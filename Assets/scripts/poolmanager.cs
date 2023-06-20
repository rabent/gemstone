using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolmanager : MonoBehaviour
{
    public GameObject[] prefabs;
    public List<GameObject>[] pools; 
    private void Awake() { //프리팹 리스트 받아오기
        pools=new List<GameObject>[prefabs.Length];
        for(int i=0; i<prefabs.Length; i++) {
            pools[i]=new List<GameObject>();
        }
    }

    public GameObject pulling(int num) { //없으면 생성, 있으면 재생성
        GameObject prefab=null;
        foreach(GameObject obj in pools[num]) {
            if(!obj.activeSelf) {
                prefab=obj; obj.SetActive(true); break;
            }
        }
        if(prefab==null) {
            prefab=Instantiate(prefabs[num],new Vector3(0,0,0),Quaternion.identity);
            pools[num].Add(prefab);
        }
        return prefab;
    }


}


