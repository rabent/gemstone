using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magic : MonoBehaviour
{
    public float damage;
    public float radius;
    public Animator anim;
    public bool fire=false;
    public bool ice=false;
    public bool lightn=false;

    private void Start() {
        anim=this.GetComponent<Animator>();
    }
    public void init(float dam, float rad, int elem, Transform player) {
        float x=Random.Range(player.position.x-4,player.transform.position.x+4);
        float y=Random.Range(player.position.y-4, player.position.y+4);
        this.damage=dam;
        this.radius=rad;
        if(elem==1) this.fire=true;
        else if (elem==2) this.ice=true;
        else if (elem==3) this.lightn=true;
        this.transform.localScale=new Vector3(rad, rad, rad);
        this.transform.position=new Vector3(x,y,0);
    }   

    private void Update() {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) this.gameObject.SetActive(false);
    }
}
