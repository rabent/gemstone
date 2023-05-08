using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gem : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isactive;
    public bool ispassive;
    public int color;
    public int id;
    public float damage;
    public int count;
    public int speed;

    private void Update() {
        if(Input.GetMouseButtonDown(1)) {
            Debug.Log("mouse");
            transform.parent.GetComponent<invenmanager>().monoliths[0].GetComponent<weaponmanager>().gems[0]=this.GetComponent<gem>();
            this.transform.position+=new Vector3(0,6,0);
            transform.parent.GetComponent<invenmanager>().monoliths[0].GetComponent<weaponmanager>().monolith_reset();
        }
    }
}
