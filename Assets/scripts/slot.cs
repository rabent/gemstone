using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slot : MonoBehaviour
{
    // Start is called before the first frame update
   private gemData pgem;
   [SerializeField]
   public gemData g {
    get {return pgem;}
    set {
        pgem=value;
        this.gameObject.GetComponent<SpriteRenderer>().sprite=g.spr;
        this.gameObject.GetComponent<SpriteRenderer>().color=new Color(1,1,1,1);
    }
   }

}
