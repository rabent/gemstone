using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    public float damage;
    public int penet;

    public void init(float dam, int pen) {
        this.damage=dam;
        this.penet=pen;
    }
   // private void Update() {
   //     this.transform.position=this.transform.parent.position+new Vector3(0,1,0);
   // }
}
