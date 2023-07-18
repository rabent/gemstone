using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class magic : MonoBehaviour
{
    public float damage;
    public float radius;
    public int id;
    public List<int> curse;
    public Transform player;
    public Animator anim;
    public bool fire=false;
    public bool ice=false;
    public bool lightn=false;
    public float anti_fireres=0;
    public float anti_iceres=0;
    public float anti_lightres=0;

    private void Start() {
        anim=this.GetComponent<Animator>();
    }
    public void init(int id, float dam, float rad, int elem, Transform player) {
        this.damage=dam;
        this.radius=rad;
        this.id=id;
        this.player=player;
        if(elem==1) this.fire=true;
        else if (elem==2) this.ice=true;
        else if (elem==3) this.lightn=true;
        switch(id) {
            case 1:
                float x=Random.Range(player.position.x-4,player.transform.position.x+4);
                float y=Random.Range(player.position.y-4, player.position.y+4);
                this.transform.localScale=new Vector3(rad, rad, rad);
                this.transform.position=new Vector3(x,y,0);
                break;
            case 5:
                anim=null;
                this.transform.position=player.transform.position;
                this.transform.DOScale(new Vector3(radius,radius*0.8f,radius),1f)
                .SetEase(Ease.InQuad)
                .OnComplete(()=> {
                    this.transform.localScale=new Vector3(0.6f, 0.5f,1);
                    this.gameObject.SetActive(false);
                });
                break;
            case 6:
                anim=null;
                this.transform.position=player.transform.position;
                this.transform.localScale=new Vector3(rad, rad, rad);
                x=Random.Range(-1.5f,1.5f);
                y=Random.Range(-30, 30);
                Vector3 dir=new Vector3(x,y,0);
                dir=dir.normalized;
                this.transform.rotation=Quaternion.FromToRotation(Vector3.up,dir);
                Rigidbody2D rigid=this.GetComponent<Rigidbody2D>();
                rigid.velocity=Vector2.zero;
                rigid.velocity=dir*5;
                break;
            case 7:
                this.transform.localScale=new Vector3(rad, rad, rad);
                break;

        }
       
    }   

    private void Update() {
        if(this.id==1 || this.id==7){
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) this.gameObject.SetActive(false);
        }
        else if(this.id==5) {
            this.transform.position=player.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
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
            case 1: //화염 저항 감소
                collision.GetComponent<Enemy>().fireres-=0.3f;
                break;
            case 2: //빙결 저항 감소
                collision.GetComponent<Enemy>().iceres-=0.3f;
                break;
            case 3: //번개 저항 감소
                collision.GetComponent<Enemy>().lightres-=0.3f;
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
        }
    }

}
