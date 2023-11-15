using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance=null;
    public poolmanager poolmng;
    public GameObject inventory;
    public invenmanager invenmanager;
    public playermanager player;
    public Spawner spawner;
    public Slider hpbar;
    public GameObject hp_red;
    public GameObject uimng;
    public uimanager ui;
    public int char_num;
    public float gameTime;
    public bool inv_active=false;
    public TMP_Text min_text;
    public TMP_Text sec_text; 
    public TMP_Text gold_text;
    public int gold=0;
    public gemData char1;
    public gemData char2;
    public gemData char3;
    public GameObject game_over_screen;
    public float maxGameTime = 2 * 10f; // 20�? / 5 * 60f >> 5�?
    public float health;
    public float maxhealth=1000;
    public bool isLive = true;

    void Awake() //게임 초기화 및 ui매니저 데이터 인계받음
    {
        if(instance==null) {
            instance=this;
        }
        else {
            Destroy(this);
        }
        min_text.text="00";
        sec_text.text="00";
        gold_text.text="000";
        uimng=GameObject.Find("UImanager");
        ui=uimng.GetComponent<uimanager>();
        char_num=ui.char_num;
        hp_refresh();
    }

    private void Start() {
        if(char_num==1) {
            GameObject mn=invenmanager.monoliths[0];
            weaponmanager wpmn=mn.GetComponent<weaponmanager>();
            wpmn.gems[0]=char1;
            wpmn.mono_slots[0].g=char1;
            wpmn.monolith_active();
        }
        else if(char_num==2) {
            GameObject mn=invenmanager.monoliths[0];
            weaponmanager wpmn=mn.GetComponent<weaponmanager>();
            wpmn.gems[0]=char2;
            wpmn.mono_slots[0].g=char2;
            wpmn.monolith_active();
            player.char_select2();
        }
        else if(char_num==3) {
            GameObject mn=invenmanager.monoliths[0];
            weaponmanager wpmn=mn.GetComponent<weaponmanager>();
            wpmn.gems[0]=char3;
            wpmn.mono_slots[0].g=char3;
            wpmn.monolith_active();
            player.char_select3();
        }
    }

    public void hp_refresh() {
        hpbar.value=health/maxhealth;
        if(hpbar.value==0) hp_red.SetActive(false);
    }
    

    private void Update()
    {
        gameTime += Time.deltaTime;

        //if(gameTime > maxGameTime){
           // gameTime = maxGameTime;
        //}

        int min=(int)gameTime / 60;
        int sec=((int)gameTime - min*60) % 60;

        if(sec>=60) {
            sec-=60;
        }
        if(min<10) min_text.text="0"+min.ToString();
        else min_text.text=min.ToString();

        if(sec<10) sec_text.text="0"+sec.ToString();
        else sec_text.text=sec.ToString();

        if(gold<10) gold_text.text="00"+gold.ToString();
        else if(gold<100) gold_text.text="0"+gold.ToString();
        else gold_text.text=gold.ToString();

        if(Input.GetKeyDown(KeyCode.I)) { //인벤토리 오픈 및 초기화
            inventory.SetActive(true);
            invenmanager.slot_refresh();
            hpbar.gameObject.SetActive(false);
            Time.timeScale=0;
            inv_active=true;
        }

        if(inv_active==true && Input.GetKeyDown(KeyCode.Escape)) {//인벤토리 켜져있을시 닫고 인벤이 꺼져있으면 설정창을 on
            GameObject[] monoliths=invenmanager.monoliths;
            foreach(GameObject mono in monoliths) {
                mono.GetComponent<weaponmanager>().monolith_active();
            }
            inventory.SetActive(false);
            hpbar.gameObject.SetActive(true);
            inv_active=false;
            Time.timeScale=1;
        }
    }

    public void merchant_phase() { //게임 시작 후 일정시간이 지나면 상점 페이즈를 오픈, 시간을 정지함
    //그와 동시에 현재 스테이지에 있던 모든 오브젝트를 비활성화함으로써 초기화
        foreach(List<GameObject> pool in poolmng.pools) {
            foreach(GameObject obj in pool) {
                obj.SetActive(false);
            }
        }
        spawner.stage++;
        spawner.boss_spawned=false;
        gameTime=0;
        Time.timeScale=0;
        ui.merchant_on();
    }

    public void game_over() {
        game_over_screen.SetActive(true);
        StartCoroutine(game_over_back());
    }

    IEnumerator game_over_back() {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("starting scene");
    }

}
