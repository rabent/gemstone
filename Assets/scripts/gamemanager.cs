using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance=null;
    public poolmanager poolmng;
    public GameObject inventory;
    public playermanager player;
    void Awake()
    {
        if(instance==null) {
            instance=this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    private void Update() {
        if(Input.GetKey(KeyCode.I)) {
            inventory.transform.position=player.transform.position;
            Debug.Log("das");
        }
        if(Input.GetKey(KeyCode.Escape)) {
            inventory.transform.position=new Vector3(500,500,0);
        }
    }




}
