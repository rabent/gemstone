using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermanager : MonoBehaviour
{
    public invenmanager inv;
    Rigidbody2D rigid;
    public Vector2 inputvec;
    public GameObject pivot;
    SpriteRenderer spriter;
    Animator anim;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    
    void Update() { //플레이어 이동
        inputvec.x=Input.GetAxis("Horizontal")*0.1f;
        inputvec.y=Input.GetAxis("Vertical")*0.1f;
    }

    private void FixedUpdate() { //플레이어 위치 갱신
        rigid.MovePosition(rigid.position + inputvec);
    }


    private void OnTriggerEnter2D(Collider2D collision) { //젬 획득
        if(collision.gameObject.tag == "gem") {
            Debug.Log("gem");
            gemData gd = collision.gameObject.GetComponent<gem>().GemData;
            inv.add_gem(gd);
            collision.gameObject.SetActive(false);
        }
    }
    void LateUpdate(){ //이동 애니메이션
        anim.SetFloat("Speed", inputvec.magnitude);

        if (inputvec.x != 0) {
            spriter.flipX = inputvec.x < 0;


        }
    }
    
}
