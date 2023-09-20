using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class weaponmanager : MonoBehaviour
{
    public int prefabid; //할당된 액티브 젬의 id
    public float damage; //피해
    public int element=0; //속성
    public float force=3; //넉백 강도
    public int count; //발사 수
    public int penet; //관통
    public float radius; //반경
    public float speed; //탄속
    public int gem_color; //젬 색깔
    public List<int> curse; //저주 목록
    public GameObject player; //플레이어 위치
    public GameObject pivot; //플레이어의 회전축
    public gemData[] gems; //석판의 젬 목록
    public int slot_index=0; //새로 개방된 슬롯 숫자
    public slotback[] expand_slots; //아직 열리지 않은 슬롯 목록
    public slot[] mono_slots; //석판의 슬롯 목록
    public GameObject special_manager;
    Coroutine crt;
    Coroutine spcrt=null;

    void Start() {
    }

    public void slot_expand() { //새로 열린 슬롯 개수가 3이 될때까지 개방 가능
        if(slot_index<3) {
            expand_slots[slot_index].slot_active();
            slot_index++;
        }
        else Debug.Log("all slot expanded");
    }


    IEnumerator magicuse(float delay) {//count만큼 마법을 pulling하여 발동시킴
    //wave 마법의 경우 중첩되면 밸런스가 무너지므로 count를 1로 고정
        while(true) {
            for(int i=0; i<count; i++) {
                GameObject mag=gamemanager.instance.poolmng.pulling(prefabid);
                if(prefabid==5) count=1;
                mag.transform.position=this.transform.position;
                mag.GetComponent<magic>().init(this.prefabid,this.damage,this.radius, this.element, player.transform);
            }
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator projectile(float delay)
    { //delay마다 count만큼 fire 함수를 발동
        while(true)
        {
            for(int i=0; i<count; i++) {
                StartCoroutine(fire());
            }
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator fire() {
        //캐릭터 전방 180도 범위중 랜덤으로 투사체를 발사하는 함수
        GameObject dagger = gamemanager.instance.poolmng.pulling(prefabid);
        dagger.transform.position = player.transform.position;
        dagger.GetComponent<projectile>().init(damage, penet,element, curse, force);
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
        //캐릭터 전방 180도만큼 근접무기를 휘두르는 함수
        while(true) {
        GameObject melee=gamemanager.instance.poolmng.pulling(prefabid);
        melee.transform.parent=pivot.transform;
        melee.transform.position=pivot.transform.position+new Vector3(0,1,0);
        melee.GetComponent<melee>().init(damage, penet, element, radius, force);
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

    public void skill_use() { //gem color에 따라 종류에 맞는 함수를 발동시킴
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

    public void monolith_reset() { //인벤토리에서 monolith에 젬을 장착시켰을 때
    //슬롯의 젬 데이터를 monolith로 가져오는 함수
        Debug.Log("gem set");
        for(int i=0; i<3; i++) { //향후 3을 열린 슬롯 개수로 수정
            if(mono_slots[i].gameObject.activeSelf==true) {
                gems[i]=mono_slots[i].g;
            }
        }
    }

    public void monolith_clear() { //공격의 중복발동을 방지하기 위해 공격 발동 전에 초기화해주는 함수
        this.damage=0;
        this.count=0;
        this.prefabid=0;
        this.gem_color=0;
        this.speed=0;
        this.radius=0;
        this.penet=0;
        this.element=0;
        this.force=3;
        curse.Clear();
        if(crt!=null) StopCoroutine(crt);
        if(spcrt!=null) special_manager.GetComponent<special>().StopCoroutine(spcrt);
    }
    public void monolith_active() {
        monolith_clear();
        //인벤토리를 끌 때 monolith가 가진 젬들을 계산하여 weaponmanager가 최종적으로 스킬을 발동함
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
                this.force=gd.force;
                skill_use();
            }
            else if(gd.ispassive) {
                if(gd.curse!=0) curse.Add(gd.curse);
                this.damage+=gd.damage;
                this.speed+=gd.speed;
                this.radius+=gd.radius;
                this.penet+=gd.penet;
                this.count+=gd.count;
                this.element=gd.element;
                this.force=gd.force;
            }
            else if(gd.isspecial) {
                spcrt=special_manager.GetComponent<special>().init(this);
            }
        }
    }
}
