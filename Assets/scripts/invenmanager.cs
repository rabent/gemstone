using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invenmanager : MonoBehaviour
{
    // Start is called before the first frame update
    private RaycastHit2D hit;
    public GameObject[] gemlist;
    public GameObject[] monoliths;

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

}
