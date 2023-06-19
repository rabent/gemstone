using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float damage;
    public int penet;

    public void init(float dam, int pen) {
        this.damage=dam;
        this.penet=pen;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            if(penet>0) penet--;
            else if(penet==0) this.gameObject.SetActive(false);
        }
    }
}
