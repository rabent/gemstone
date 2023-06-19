using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class weaponmanager : MonoBehaviour
{
    public int id;
    public int prefabid;
    public float damage;
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

    void Start() {
    }


    IEnumerator magicuse(float delay) {
        while(true) {
            for(int i=0; i<count; i++) {
                GameObject mag=gamemanager.instance.poolmng.pulling(prefabid);
                mag.transform.position=this.transform.position;
                mag.GetComponent<magic>().init(this.damage,this.radius, player.transform);
            }
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator projectile(float delay)
    {
        while(true)
        {
            for(int i=0; i<count; i++) {
                StartCoroutine(fire());
            }
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator fire() {
        GameObject dagger = gamemanager.instance.poolmng.pulling(prefabid);
        dagger.transform.position = player.transform.position;
        dagger.GetComponent<projectile>().init(damage, penet);
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
        while(true) {
        GameObject melee=gamemanager.instance.poolmng.pulling(prefabid);
        melee.transform.parent=pivot.transform;
        melee.transform.position=pivot.transform.position+new Vector3(0,1,0);
        melee.GetComponent<melee>().init(damage, penet, radius);
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

    public void skill_use() {
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

    public void monolith_reset() {
        Debug.Log("gem set");
        for(int i=0; i<3; i++) {
            if(mono_slots[i].g!=null) gems[i]=mono_slots[i].g;
        }
    }
    public void monolith_active() {
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
                skill_use();
            }
            else if(gd.ispassive) {
                this.count+=gd.count;
            }
            else if(gd.isspecial) {
                special_manager.GetComponent<special>().init(this);
            }
        }
    }
}
