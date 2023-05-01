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

    void Start() {
        StartCoroutine(projectile());
    }

    IEnumerator projectile()
    {
        while(true)
        {
            for(int i=0; i<count; i++) {
                StartCoroutine(fire());
            }
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator fire() {
        GameObject dagger = gamemanager.instance.poolmng.pulling(prefabid);
        dagger.transform.position = this.transform.position;
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

}
