using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magic : MonoBehaviour
{
    public float damage;
    public float radius;

    public void init(float dam, float rad, Transform player) {
        float x=Random.Range(player.position.x-4,player.transform.position.x+4);
        float y=Random.Range(player.position.y-4, player.position.y+4);
        this.damage=dam;
        this.radius=rad;
        this.transform.localScale=new Vector3(rad, rad, rad);
        this.transform.position=new Vector3(x,y,0);
        StartCoroutine(magicfalse(this.gameObject));
    }   

    IEnumerator magicfalse(GameObject magic) {
        yield return new WaitForSeconds(0.5f);
        magic.SetActive(false);
    }
}
