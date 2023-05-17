using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermanager : MonoBehaviour
{
    Rigidbody2D rigid;
    public Vector2 inputvec;

    private void Awake() {
        rigid=GetComponent<Rigidbody2D>();
    }
    
    void Update() {
        inputvec.x=Input.GetAxis("Horizontal")*0.2f;
        inputvec.y=Input.GetAxis("Vertical")*0.2f;
    }

    private void FixedUpdate() {
        rigid.MovePosition(rigid.position + inputvec);
    }
    
}
