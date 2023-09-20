using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolmanager : MonoBehaviour
{
    public GameObject[] prefabs;
    public List<GameObject>[] pools; 
    private void Awake() { //리스트에 담긴 프리팹들을 가져옴
        pools=new List<GameObject>[prefabs.Length];
        for(int i=0; i<prefabs.Length; i++) {
            pools[i]=new List<GameObject>();
        }
    }

    public GameObject pulling(int num) { //풀에 담긴 원하는 오브젝트를 생성하고 inactive인 오브젝트가 있으면 active하여 재활용함
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


