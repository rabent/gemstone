using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class magic : MonoBehaviour
{
    public float damage; //피해
    public float radius; //반경
    public int id; //id번호
    public List<int> curse; //발동되는 저주 목록 
    public Transform player; //플레이어 위치
    public Animator anim; //마법의 애니메이션
    public bool fire=false; //화염 속성인가?
    public bool ice=false; //얼음 속성인가?
    public bool lightn=false; //번개 속성인가?
    public float anti_fireres=0; //화염저항 무시 수치
    public float anti_iceres=0; //얼음저항 무시 수치
    public float anti_lightres=0; //번개저항 무시 수치

    private void Start() {
        anim=this.GetComponent<Animator>();
    }
    public void init(int id, float dam, float rad, int elem, Transform player) {
        //투사체의 변수를 할당하고 id에 따라 다른 마법을 발동시킴
        this.damage=dam;
        this.radius=rad;
        this.id=id;
        this.player=player;
        if(elem==1) this.fire=true;
        else if (elem==2) this.ice=true;
        else if (elem==3) this.lightn=true;
        switch(id) {
            case 1: //spark 젬
                float x=Random.Range(player.position.x-4,player.transform.position.x+4);
                float y=Random.Range(player.position.y-4, player.position.y+4);
                this.transform.localScale=new Vector3(rad, rad, rad);
                this.transform.position=new Vector3(x,y,0);
                break;
            case 5: //wave 젬
                anim=null;
                this.transform.position=player.transform.position;
                this.transform.localScale=new Vector3(radius,radius*0.8f,radius);
                StartCoroutine(inactive(0.3f));
                break;
            case 6: //fireball 젬
                anim=null;
                this.transform.position=player.transform.position;
                this.transform.localScale=new Vector3(rad, rad, rad);
                x=Random.Range(-1f,1f);
                y=Random.Range(-2f, 2f);
                Vector3 dir=new Vector3(x,y,0);
                dir=dir.normalized;
                this.transform.rotation=Quaternion.FromToRotation(Vector3.up,dir);
                Rigidbody2D rigid=this.GetComponent<Rigidbody2D>();
                rigid.velocity=Vector2.zero;
                rigid.velocity=dir*5;
                StartCoroutine(inactive(5f));
                break;
            case 7: //폭발 효과
                this.transform.localScale=new Vector3(rad, rad, rad);
                break;
            case 9: //icicle 젬
                anim=null;
                this.transform.position=player.transform.position;
                this.transform.localScale=new Vector3(rad, rad, rad);
                x=Random.Range(-1.5f,-0.1f);
                y=Random.Range(-1f, 1f);
                dir=new Vector3(x,y,0);
                dir=dir.normalized;
                this.transform.rotation=Quaternion.FromToRotation(Vector3.up,dir);
                rigid=this.GetComponent<Rigidbody2D>();
                rigid.velocity=Vector2.zero;
                rigid.velocity=dir*12;
                StartCoroutine(inactive(5f));
                break;
            case 11:
                anim=null;
                this.transform.position=player.transform.position;
                StartCoroutine(fire_totem());
                StartCoroutine(inactive(7f));
                break;
            case 12:
                this.transform.localScale=new Vector3(rad,rad,rad);
                break;
        }
       
    } 

    IEnumerator fire_totem() {
        GameObject exp=gamemanager.instance.poolmng.pulling(12);
        exp.transform.position=this.transform.position;
        exp.GetComponent<magic>().init(12, this.damage, this.radius, 1, player);
        yield return new WaitForSeconds(3f);
        StartCoroutine(fire_totem());
    }

    IEnumerator inactive(float tm)  {
        yield return new WaitForSeconds(tm);
        this.gameObject.SetActive(false);
    }

    private void Update() {
        //애니메이션이 있는 마법의 경우 애니메이션이 끝나면 inactive해줌
        if(this.id==1 || this.id==7 || this.id==12){
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) this.gameObject.SetActive(false);
        }
        else if(this.id==5) {
            this.transform.position=player.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //적과 충돌시 가지고있는 저주를 발동시키고 파이어볼은 충돌시 폭발 생성
        if(collision.gameObject.tag == "Enemy") {
            foreach(int i in curse) {
                curse_use(i, collision);
            }
            if(id==6) {
                GameObject exp=gamemanager.instance.poolmng.pulling(7);
                exp.transform.position=this.transform.position;
                exp.GetComponent<magic>().init(7,this.damage*0.3f,this.damage*0.125f, 0, player.transform);
                this.gameObject.SetActive(false);
            }
        }
    }

    void curse_use(int index, Collider2D collision) {
        switch(index) {
           case 1: //화염 저항
                if(!collision.GetComponent<Enemy>().cursed[index]){
                    collision.GetComponent<Enemy>().fireres-=0.3f;
                    collision.GetComponent<Enemy>().cursed[index]=true;
                }
                break;
            case 2: //빙결 저항 감소
                if(!collision.GetComponent<Enemy>().cursed[index]){
                    collision.GetComponent<Enemy>().iceres-=0.3f;
                    collision.GetComponent<Enemy>().cursed[index]=true;
                }
                break;
            case 3: //번개 저항 감소
                if(!collision.GetComponent<Enemy>().cursed[index]){
                    collision.GetComponent<Enemy>().lightres-=0.3f;
                    collision.GetComponent<Enemy>().cursed[index]=true;
                }
                break;
            case 4: //화염 저항 무시
                this.anti_fireres=0.2f;
                break;
            case 5: //빙결 저항 무시
                this.anti_iceres=0.2f;
                break;
            case 6: //번개 저항 무시
                this.anti_lightres=0.2f;
                break;
            case 8:
                if(!collision.GetComponent<Enemy>().cursed[index]){
                    collision.GetComponent<Enemy>().speed*=0.85f;
                    collision.GetComponent<Enemy>().cursed[index]=true;
                }
                break;
        }
    }
}
