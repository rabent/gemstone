using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invenmanager : MonoBehaviour
{
    // Start is called before the first frame update
    public static invenmanager inventory;
    public GameObject inv_pannel;
    public gemData[] gemlist;
    public GameObject[] monoliths;
    public GameObject[] slots;
    public int gemcount=0;

    void Start() {
        inventory=this;
    }
    
    public void slot_refresh() { // 인벤토리 슬롯을 젬 리스트와 동기화시켜줌
        for(int i=0; i<slots.Length; i++) {
            if(gemlist[i]!=null){
                slots[i].GetComponent<slot>().g=gemlist[i];
            }
            Debug.Log("slot refresh");  
        }
    }

    public void gemlist_refresh() { //인벤토리 내 젬의 위치변경 등이 있을때 리스트에도 반영해줌
        gemcount=0;
        for(int i=0; i<slots.Length; i++) {
            gemlist[i]=slots[i].GetComponent<slot>().g;
            if(gemlist[i]!=null) gemcount++;
        }
        Debug.Log("gemlist refresh");  
    }

    public void add_gem(gemData gd) { //슬롯에 여유가 있다면 젬리스트에 젬 데이터를 넣어줌
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
