using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    public float damage;
    public int penet;
    public float radius;

    public void init(float dam, int pen, float rad) {
        this.damage=dam;
        this.penet=pen;
        this.radius=rad;
        this.transform.localScale=new Vector3(0.5f*rad, rad, 1);
    }
   // private void Update() {
   //     this.transform.position=this.transform.parent.position+new Vector3(0,1,0);
   // }
}
