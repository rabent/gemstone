using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    // Start is called before the first frame update
   private gemData pgem;
   public Image slot_img;
   public bool isfull=false;
   [SerializeField]
   public gemData g {
    get {return pgem;}
    set {
        pgem=value;
        isfull=true;
        slot_img.sprite=g.spr;
        slot_img.color=new Color(1,1,1,1);
    }
   }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(isfull) {
            draggedslot.instance.dragslot=this;
            draggedslot.instance.dragset(slot_img);
            draggedslot.instance.transform.position=eventData.position;

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isfull) {
            draggedslot.instance.transform.position=eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggedslot.instance.drag_invisible(0);
        draggedslot.instance.dragslot=null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(draggedslot.instance.dragslot!=null) {
            this.g=draggedslot.instance.dragslot.g;
            Debug.Log("drop");
            this.gameObject.transform.GetComponentInParent<weaponmanager>().monolith_reset();
        }
    }
}
