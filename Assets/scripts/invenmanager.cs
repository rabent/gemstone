using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invenmanager : MonoBehaviour
{
    // Start is called before the first frame update
    public static invenmanager inventory;
    public gemData[] gemlist;
    public GameObject[] monoliths;
    public GameObject[] slots;
    public int gemcount=0;

    void Start() {
        inventory=this;
    }

    
    public void slot_refresh() { // 인벤토리 활성화시 슬롯 새로고침
        for(int i=0; i<slots.Length; i++) {
            if(gemlist[i]!=null){
                slots[i].GetComponent<slot>().g=gemlist[i];
            }
            Debug.Log("slot refresh");  
        }
        //for(int i=gemcount; i<slots.Length; i++) {
           // slots[i].GetComponent<slot>().g=null;
        //}
    }

    public void gemlist_refresh() {
        for(int i=0; i<slots.Length; i++) {
            gemlist[i]=slots[i].GetComponent<slot>().g;
        }
        Debug.Log("gemlist refresh");  
    }

    public void add_gem(gemData gd) {
        if(gemcount<slots.Length) {
            for(int i=0; i<slots.Length; i++) {
                if(gemlist[i]==null) {
                    gemlist[i]=gd;
                    gemcount++;
                    break;
                }
            }
        }
        else {
            Debug.Log("slot full");
        }
    }

}
