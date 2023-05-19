using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slot : MonoBehaviour
{
    // Start is called before the first frame update
   private gemData pgem;
   public gemData g {
    get {return pgem;}
    set {
        pgem=value;
        if(pgem==null) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite=g.spr;
        }
    }
   }

}
