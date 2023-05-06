using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public gem[] gems;

    void Start() {
        gems=new gem[1];
        
    }


    IEnumerator magicuse(float delay) {
        while(true) {
            for(int i=0; i<count; i++) {
                GameObject mag=gamemanager.instance.poolmng.pulling(prefabid);
                mag.transform.position=this.transform.position;
                mag.GetComponent<magic>().init(this.damage,this.radius);
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

    public void monolith_reset() {
        foreach(gem g in gems) {
            if(g.isactive) {
                this.damage=g.damage;
                this.count=g.count;
                this.prefabid=g.id;
                this.gem_color=g.color;
                this.speed=g.speed;
                if(gem_color==1) {
                    StartCoroutine(projectile(2f));
                }
                else if(gem_color==2) {
                    StartCoroutine(magicuse(2f));
                }
            }
            else if(g.ispassive) {

            }
        }
    }

}
