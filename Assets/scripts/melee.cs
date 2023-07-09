using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    public float damage;
    public int penet;
    public float radius;
    public bool fire=false;
    public bool ice=false;
    public bool lightn=false;

    public void init(float dam, int pen, int elem, float rad) {
        this.damage=dam;
        this.penet=pen;
        this.radius=rad;
        if(elem==1) this.fire=true;
        else if (elem==2) this.ice=true;
        else if (elem==3) this.lightn=true;
        this.transform.localScale=new Vector3(0.5f*rad, rad, 1);
    }
   // private void Update() {
   //     this.transform.position=this.transform.parent.position+new Vector3(0,1,0);
   // }
}
