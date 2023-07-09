using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float damage;
    public int penet;
    public bool fire=false;
    public bool ice=false;
    public bool lightn=false;

    public void init(float dam, int pen, int elem) {
        this.damage=dam;
        this.penet=pen;
        if(elem==1) this.fire=true;
        else if (elem==2) this.ice=true;
        else if (elem==3) this.lightn=true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            if(penet>0) penet--;
            else if(penet==0) this.gameObject.SetActive(false);
        }
    }
}
