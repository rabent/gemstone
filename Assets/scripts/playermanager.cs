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
    float char_speed=0.1f;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    
    void Update() { //캐릭터 이동
        inputvec.x=Input.GetAxis("Horizontal")*char_speed;
        inputvec.y=Input.GetAxis("Vertical")*char_speed;
    }

    public void player_dead() {
        char_speed=0;
        gamemanager.instance.game_over();
        invenmanager.inventory.inven_dead();
    }

    private void FixedUpdate() { //area와 함께 이동시켜줌
        rigid.MovePosition(rigid.position + inputvec);
    }


    private void OnTriggerEnter2D(Collider2D collision) { //젬과 충돌시 인벤의 젬리스트에 추가
        if(collision.gameObject.tag == "gem") {
            Debug.Log("gem");
            gemData gd = collision.gameObject.GetComponent<gem>().GemData;
            inv.add_gem(gd);
            collision.gameObject.SetActive(false);
        }
    }
    void LateUpdate(){ //걷는 애니메이션 재생
        anim.SetFloat("Speed", inputvec.magnitude);

        if (inputvec.x != 0) {
            spriter.flipX = inputvec.x < 0;


        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!gamemanager.instance.isLive)
            return;


        if(gamemanager.instance.health < 0)
        {
            for(int index=2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
        }
    }
}
