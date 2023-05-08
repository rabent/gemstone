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
        inputvec.x=Input.GetAxis("Horizontal");
        inputvec.y=Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        rigid.MovePosition(rigid.position + inputvec);
    }
    
}
