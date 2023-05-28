using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterpoolmanager : MonoBehaviour
{
    public GameObject[] prefabs;
    public List<GameObject>[] pools; 

    void Awake() {
        pools=new List<GameObject>[prefabs.Length];
        for(int i=0; i<prefabs.Length; i++) {
            pools[i]=new List<GameObject>();
        }
        Debug.Log(pools.Length);
    }
}
