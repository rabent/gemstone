using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slot : MonoBehaviour
{
    // Start is called before the first frame update
   private gem pgem;
   public gem g {
    get {return pgem;}
    set {
        pgem=value;
        if(pgem==null) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite=g.gameObject.GetComponent<SpriteRenderer>().sprite;
        }
    }
   }

}
