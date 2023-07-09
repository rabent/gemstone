using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class weaponmanager : MonoBehaviour
{
    public int prefabid;
    public float damage;
    public int element=0;
    public int count;
    public int penet;
    public float radius;
    public float speed;
    public int gem_color;
    public GameObject player;
    public GameObject pivot;
    public gemData[] gems;
    public slot[] mono_slots;
    public GameObject special_manager;
    Coroutine crt;
    Coroutine spcrt=null;

    void Start() {
    }


    IEnumerator magicuse(float delay) {//딜레이마다 마법을 생성해서 count만큼 사용
        while(true) {
            for(int i=0; i<count; i++) {
                GameObject mag=gamemanager.instance.poolmng.pulling(prefabid);
                mag.transform.position=this.transform.position;
                mag.GetComponent<magic>().init(this.damage,this.radius, this.element, player.transform);
            }
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator projectile(float delay)
    { //delay마다 count만큼 fire함수 발동
        while(true)
        {
            for(int i=0; i<count; i++) {
                StartCoroutine(fire());
            }
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator fire() {
        //오브젝트 생성 후 전방 180도에 랜덤으로 발사
        GameObject dagger = gamemanager.instance.poolmng.pulling(prefabid);
        dagger.transform.position = player.transform.position;
        dagger.GetComponent<projectile>().init(damage, penet,element);
        float x=Random.Range(0, 30);
        float y=Random.Range(-30,30);
        Vector3 dir=new Vector3(x,y,0);
        dir=dir.normalized;
        dagger.transform.rotation=Quaternion.FromToRotation(Vector3.up,dir);
        Rigidbody2D rigid=dagger.GetComponent<Rigidbody2D>();
        rigid.velocity=Vector2.zero;
        rigid.velocity=dir*speed;
        yield return null;
        StartCoroutine(daggerfalse(dagger));
    }

    IEnumerator daggerfalse(GameObject used_dagger) {
        yield return new WaitForSeconds(5f);
        used_dagger.SetActive(false);
    }

    IEnumerator swing(float delay) {
        //오브젝트를 pivot의 자식으로 생성 후 전방 180도로 휘두름
        while(true) {
        GameObject melee=gamemanager.instance.poolmng.pulling(prefabid);
        melee.transform.parent=pivot.transform;
        melee.transform.position=pivot.transform.position+new Vector3(0,1,0);
        melee.GetComponent<melee>().init(damage, penet, element, radius);
        pivot.transform.DORotate(new Vector3(0,0,180f),0.75f)
        .SetEase(Ease.OutQuart)
        .OnComplete(()=> {
            pivot.transform.localEulerAngles=new Vector3(0,0,0);
        });
        StartCoroutine(swing_false(melee));
        yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator swing_false(GameObject melee) {
        yield return new WaitForSeconds(0.75f);
        melee.SetActive(false);
    }

    public void skill_use() { //스킬을 gem color따라서 사용
        if(gem_color==1) {
            crt=StartCoroutine(projectile(2f));
        }
        else if(gem_color==2) {
            crt=StartCoroutine(magicuse(2f));
        }
        else if(gem_color==3) {
            crt=StartCoroutine(swing(3f));
        }
    }

    public void monolith_reset() { //인벤토리의 석판 슬롯에 젬 장착시
    //슬롯의 데이터들을 monolith empty로 넘겨받음
        Debug.Log("gem set");
        for(int i=0; i<3; i++) {
            gems[i]=mono_slots[i].g;
        }
    }

    public void monolith_clear() {
        this.damage=0;
        this.count=0;
        this.prefabid=0;
        this.gem_color=0;
        this.speed=0;
        this.radius=0;
        this.penet=0;
        this.element=0;
        if(crt!=null) StopCoroutine(crt);
        if(spcrt!=null) special_manager.GetComponent<special>().StopCoroutine(spcrt);
    }
    public void monolith_active() {
        monolith_clear();
        //인벤토리 비활성화시 작동, 젬리스트의 젬들을 검사하여 파라미터를 받아오고 스킬을 작동
        foreach(gemData gd in gems) {
            if(gd==null) continue;
            if(gd.isactive) {
                this.damage=gd.damage;
                this.count=gd.count;
                this.prefabid=gd.id;
                this.gem_color=gd.color;
                this.speed=gd.speed;
                this.radius=gd.radius;
                this.penet=gd.penet;
                this.element=gd.element;
                skill_use();
            }
            else if(gd.ispassive) {
                this.damage+=gd.damage;
                this.speed+=gd.speed;
                this.radius+=gd.radius;
                this.penet+=gd.penet;
                this.count+=gd.count;
                this.element=gd.element;
            }
            else if(gd.isspecial) {
                spcrt=special_manager.GetComponent<special>().init(this);
            }
        }
    }
}
