using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolmanager : MonoBehaviour
{
    public GameObject[] prefabs;
    public List<GameObject>[] pools; 
    private void Awake() {
        pools=new List<GameObject>[prefabs.Length];
        for(int i=0; i<prefabs.Length; i++) {
            pools[i]=new List<GameObject>();
        }
    }

    public GameObject pulling(int num) {
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

    internal void Get(int v)
    {
        throw new NotImplementedException();
    }
}


