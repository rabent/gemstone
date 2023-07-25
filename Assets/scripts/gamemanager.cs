using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance=null;
    public poolmanager pool;
    public poolmanager poolmng;
    public GameObject inventory;
    public invenmanager invenmanager;
    public playermanager player;
    public GameObject uimng;
    public uimanager ui;
    public int char_num;
    public float gameTime;
    public bool inv_active=false;
    public float maxGameTime = 2 * 10f; // 20�? / 5 * 60f >> 5�?

    void Awake() //게임 초기화
    {
        if(instance==null) {
            instance=this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);

        uimng=GameObject.Find("UImanager");
        ui=uimng.GetComponent<uimanager>();
        char_num=ui.char_num;
    }
    

    private void Update()
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime){
            gameTime = maxGameTime;
        }

        if(Input.GetKeyDown(KeyCode.I)) { //인벤토리 오픈 및 초기화
            inventory.SetActive(true);
            invenmanager.slot_refresh();
            Time.timeScale=0;
            inv_active=true;
        }

        if(inv_active==true && Input.GetKeyDown(KeyCode.Escape)) {//인벤토리 켜져있을시 닫고 인벤이 꺼져있으면 설정창을 on
            GameObject[] monoliths=invenmanager.monoliths;
            foreach(GameObject mono in monoliths) {
                mono.GetComponent<weaponmanager>().monolith_active();
            }
            inventory.SetActive(false);
            inv_active=false;
            Time.timeScale=1;
        }
    }




}
