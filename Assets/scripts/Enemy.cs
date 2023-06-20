using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();

    }
    void FixedUpdate()
    {
        if(!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    } //�� ��ġ ����
    void LateUpdate() 
    {
        if(!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }//���� ĳ���͸� ���ϵ��� ��ġ���� 
    void OnEnable() 
    {
        target = gamemanager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    } //�� �ʱ�ȭ

    public void Init(SpawnData data)
    {
        Debug.Log(data.spriteType);
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    } //�����ʹ�� �� �Ķ���͸� ����

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (!collision.CompareTag("Bullet") && !collision.CompareTag("melee") && !collision.CompareTag("magic"))
            return;

        if(collision.CompareTag("Bullet")) {
            health -= collision.GetComponent<projectile>().damage;
            StartCoroutine(KonckBack());
        }
        else if(collision.CompareTag("melee")) {
            health -= collision.GetComponent<melee>().damage;
            StartCoroutine(KonckBack());
        }
        else if(collision.CompareTag("magic")) {
            health -= collision.GetComponent<magic>().damage;
            StartCoroutine(KonckBack());
        }

        if(health > 0){ //��������� �ǰ� �ִϸ��̼� �۵�
            anim.SetTrigger("Hit");
        }
        else {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
        } //��� �� �Ķ���� ���� �� �ִϸ��̼� Ȱ��ȭ
    }

    IEnumerator KonckBack()
    {
        yield return wait; 
        Vector3 playerPos = gamemanager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    } //�ǰ� �� �� �˹�

    void Dead(){
        gameObject.SetActive(false);
    } //������Ʈ ��Ȱ��ȭ
}
