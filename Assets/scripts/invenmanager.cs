using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invenmanager : MonoBehaviour
{
    // Start is called before the first frame update
    public static invenmanager inventory;
    private RaycastHit2D hit;
    public gemData[] gemlist;
    public GameObject[] monoliths;
    public GameObject[] slots;
    public int gemcount;

    void Start() {
        inventory=this;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(1)) {
            Vector2 point=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(point, Vector2.zero);
            Debug.Log(hit.transform.gameObject);
            if(hit.transform.gameObject.tag == "slot" && hit.transform.gameObject.GetComponent<slot>().isfull) {
                //monoliths[0].GetComponent<weaponmanager>().gems[0]=hit.transform.gameObject.GetComponent<gemData>();
                hit.transform.gameObject.transform.position+=new Vector3(0,3,0);
                 monoliths[0].GetComponent<weaponmanager>().monolith_reset();
            }
            }
            
        }
    
    public void slot_refresh() {
        gemcount=0;
        for(int i=0; i<slots.Length; i++) {
            if(gemlist[i]!=null){
                slots[i].GetComponent<slot>().g=gemlist[i];
                gemcount++;
            }
            else slots[i].GetComponent<slot>().g=gemlist[i];
            Debug.Log("slot refresh");  
        }
        for(int i=gemcount; i<slots.Length; i++) {
            slots[i].GetComponent<slot>().g=null;
        }
    }

    public void add_gem(gemData gd) {
        if(gemcount<slots.Length) {
            for(int i=0; i<slots.Length; i++) {
                if(gemlist[i]==null) {
                    gemlist[i]=gd;
                    break;
                }
            }
        }
        else {
            Debug.Log("slot full");
        }
    }

}
