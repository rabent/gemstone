using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float fireres;
    public float iceres;
    public float lightres;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    int spriteType;


    bool isLive = true;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;


    void Awake() //변수 할당
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();

    }
    void FixedUpdate()
    { //몬스터 이동
        if(!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
    void LateUpdate() 
    {
        if(!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }
    void OnEnable() 
    {
        target = gamemanager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    { //몬스터 스탯 초기화
        Debug.Log(data.spriteType);
        spriteType = data.spriteType;
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
        fireres=data.fireres;
        iceres=data.iceres;
        lightres=data.lightres;
        if(spriteType == 3){
            transform.localScale = new Vector3(2, 2 ,1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) 
    { //공격을 받으면 저항을 계산하여 데미지를 입음
        if (!collision.CompareTag("Bullet") && !collision.CompareTag("melee") && !collision.CompareTag("magic"))
            return;

        if(collision.CompareTag("Bullet")) {
            float dam=collision.GetComponent<projectile>().damage;
            if(collision.GetComponent<projectile>().fire) dam-=dam*(this.fireres-collision.GetComponent<projectile>().anti_fireres);
            else if(collision.GetComponent<projectile>().ice) dam-=dam*(this.iceres-collision.GetComponent<projectile>().anti_iceres);
            else if(collision.GetComponent<projectile>().lightn) dam-=dam*(this.lightres-collision.GetComponent<projectile>().anti_lightres);
            health -= dam;
            StartCoroutine(KonckBack());
            audiomanager.instance.PlaySfx(audiomanager.Sfx.Range);
        }
        else if(collision.CompareTag("melee")) {
            float dam=collision.GetComponent<melee>().damage;
            if(collision.GetComponent<melee>().fire) dam-=dam*(this.fireres-collision.GetComponent<melee>().anti_fireres);
            else if(collision.GetComponent<melee>().ice) dam-=dam*(this.iceres-collision.GetComponent<melee>().anti_iceres);
            else if(collision.GetComponent<melee>().lightn) dam-=dam*(this.lightres-collision.GetComponent<melee>().anti_lightres);
            health -= dam;
            StartCoroutine(KonckBack());
            audiomanager.instance.PlaySfx(audiomanager.Sfx.Melee);
        }
        else if(collision.CompareTag("magic")) {
            float dam=collision.GetComponent<magic>().damage;
            if(collision.GetComponent<magic>().fire) dam-=dam*(this.fireres-collision.GetComponent<magic>().anti_fireres);
            else if(collision.GetComponent<magic>().ice) dam-=dam*(this.iceres-collision.GetComponent<magic>().anti_iceres);
            else if(collision.GetComponent<magic>().lightn) dam-=dam*(this.lightres-collision.GetComponent<magic>().anti_lightres);
            health -= dam;
            StartCoroutine(KonckBack());
        }

        if(health > 0){
            //체력이 남아있을시 hit
            anim.SetTrigger("Hit");
            audiomanager.instance.PlaySfx(audiomanager.Sfx.Hit);
        }
        else {
            //사망 과정 진행
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            audiomanager.instance.PlaySfx(audiomanager.Sfx.Dead);
        }
    }

    IEnumerator KonckBack()
    {
        yield return wait; //애니메이션 중첩을 막기위해 유예를 줌
        Vector3 playerPos = gamemanager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead(){
        gameObject.SetActive(false);
    }
}