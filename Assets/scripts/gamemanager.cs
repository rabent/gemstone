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
    public float gameTime;
    public float maxGameTime = 2 * 10f; // 20초 / 5 * 60f >> 5분
    void Awake()
    {
        if(instance==null) {
            instance=this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime){
            gameTime = maxGameTime;
        }

        if(Input.GetKeyDown(KeyCode.I)) {
            inventory.SetActive(true);
            invenmanager.slot_refresh();
            Time.timeScale=0;
            Debug.Log("das");
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            GameObject[] monoliths=invenmanager.monoliths;
            foreach(GameObject mono in monoliths) {
                mono.GetComponent<weaponmanager>().monolith_active();
            }
            inventory.SetActive(false);
            Time.timeScale=1;
        }
    }




}
