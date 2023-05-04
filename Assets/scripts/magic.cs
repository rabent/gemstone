using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magic : MonoBehaviour
{
    public float damage;
    public float radius;

    public void init(int magicidx, float dam, float rad) {
        GameObject magic=gamemanager.instance.poolmng.pulling(magicidx);
        float x=Random.Range(-100,100);
        float y=Random.Range(-100, 100);
        this.damage=dam;
        this.radius=rad;
        this.transform.localScale=new Vector3(rad, rad, rad);
        magic.transform.position=new Vector3(x,y,0);
        StartCoroutine(magicfalse(magic));
    }   

    IEnumerator magicfalse(GameObject magic) {
        yield return new WaitForSeconds(1f);
        magic.SetActive(false);
    }
}
