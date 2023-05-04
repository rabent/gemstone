using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magic : MonoBehaviour
{
    public float damage;
    public float radius;

    public void init(float dam, float rad) {
        float x=Random.Range(this.transform.position.x-100,this.transform.position.x+100);
        float y=Random.Range(this.transform.position.y-100, this.transform.position.y+100);
        this.damage=dam;
        this.radius=rad;
        this.transform.localScale=new Vector3(rad, rad, rad);
        this.transform.position=new Vector3(x,y,0);
        StartCoroutine(magicfalse(this.gameObject));
    }   

    IEnumerator magicfalse(GameObject magic) {
        yield return new WaitForSeconds(1f);
        magic.SetActive(false);
    }
}
