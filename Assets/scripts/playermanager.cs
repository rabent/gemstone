using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermanager : MonoBehaviour
{
    Rigidbody2D rigid;
    public Vector2 inputvec;
    SpriteRenderer spriter;
    Animator anim;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    
    void Update() {
        inputvec.x=Input.GetAxis("Horizontal")*0.1f;
        inputvec.y=Input.GetAxis("Vertical")*0.1f;
    }

    private void FixedUpdate() {
        rigid.MovePosition(rigid.position + inputvec);
    }

    void LateUpdate(){
        anim.SetFloat("Speed", inputvec.magnitude);

        if (inputvec.x != 0) {
            spriter.flipX = inputvec.x < 0;

        }
    }
    
}
