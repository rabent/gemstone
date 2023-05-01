using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermanager : MonoBehaviour
{
    public GameObject player;
    public float speed = 100f;
    public Camera cam;
    
    void Update() {
        if(Input.GetKey(KeyCode.D)) {
            transform.Translate(new Vector3(speed*Time.deltaTime,0,0));
        }
        if(Input.GetKey(KeyCode.A)) {
            transform.Translate(new Vector3(-speed*Time.deltaTime,0,0));
            Debug.Log("ddd");
        }
        if(Input.GetKey(KeyCode.W)) {
            transform.Translate(new Vector3(0,speed*Time.deltaTime,0));
        }
        if(Input.GetKey(KeyCode.S)) {
            transform.Translate(new Vector3(0,-speed*Time.deltaTime,0));
        }
        cam.transform.position=new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z-10);
    }
    
}
