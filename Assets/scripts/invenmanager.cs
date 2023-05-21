using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invenmanager : MonoBehaviour
{
    // Start is called before the first frame update
    private RaycastHit2D hit;
    public List<gemData> gemlist;
    public GameObject[] monoliths;
    public GameObject[] slots;

    private void Update() {
        if(Input.GetMouseButtonDown(1)) {
            Vector2 point=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(point, Vector2.zero);
            Debug.Log(hit.transform.gameObject);
            if(hit.transform.gameObject.tag == "gem") {
                monoliths[0].GetComponent<weaponmanager>().gems[0]=hit.transform.gameObject.GetComponent<gem>();
                hit.transform.gameObject.transform.position+=new Vector3(0,3,0);
                 monoliths[0].GetComponent<weaponmanager>().monolith_reset();
            }
            }
            
        }
    
    public void slot_refresh() {
        int gemcount=1;
        for(int i=0; i<gemlist.Count && i<slots.Length; i++) {
            slots[i].GetComponent<slot>().g=gemlist[i];
            gemcount++;
            Debug.Log("slot refresh");
        }
        for(int i=gemcount; i<slots.Length; i++) {
            slots[i].GetComponent<slot>().g=null;
        }
    }

    public void add_gem(gemData gd) {
        if(gemlist.Count<slots.Length) {
            gemlist.Add(gd);
            slot_refresh();
        }
        else {
            Debug.Log("slot full");
        }
    }

}
