using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    // Start is called before the first frame update
    [SerializeField]
   private gemData pgem;
   public Image slot_img;
   public bool isfull=false;
   public int slot_index;
   public gemData g { //슬롯에 젬데이터 input
    get {return pgem;}
    set {
        pgem=value;
        if(pgem==null) {
            slot_img.color=new Color(1,1,1,0);
            isfull=false;
        }
        else {
            isfull=true;
            slot_img.sprite=g.spr;
            slot_img.color=new Color(1,1,1,1);
        }
        
    }
   }

    public void OnBeginDrag(PointerEventData eventData)
    { //드래그 시작 시 draggedslot에 데이터 저장
        if(isfull) {
            draggedslot.instance.dragslot=this;
            draggedslot.instance.dragset(slot_img);
            draggedslot.instance.transform.position=eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    { //드래그 도중 draggedslot 위치 마우스포인터로 변경
        if(isfull) {
            draggedslot.instance.transform.position=eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    { //드래그가 끝날 시 begindrag한 슬롯에서 발동
    //석판에 드랍했는지 인벤토리에 드랍했는지에 따라 처리 진행
        if(draggedslot.instance.is_monolith==true) {
            this.g=null;
            invenmanager.inventory.gemlist[slot_index]=null;
            draggedslot.instance.is_monolith=false;
        }
        else if(draggedslot.instance.is_change==true) {
            Debug.Log(draggedslot.instance.change_gd);
            this.g=draggedslot.instance.change_gd;
            int idx=draggedslot.instance.change_idx;
            invenmanager.inventory.gemlist[idx]=this.g;
            invenmanager.inventory.gemlist[slot_index]=draggedslot.instance.change_gd;
            draggedslot.instance.change_gd=null;
            draggedslot.instance.change_idx=-1;
            draggedslot.instance.is_change=false;
        }
        draggedslot.instance.drag_invisible(0);
        draggedslot.instance.dragslot=null;
    }

    public void OnDrop(PointerEventData eventData)
    {//enddrag보다 먼저 발동, drop한 슬롯에서 발동
    //석판에 drop시 데이터 넘겨주고 석판을 refresh
    //인벤토리에 drop시 서로 데이터를 교환
        if(draggedslot.instance.dragslot!=null && this.gameObject.tag=="monoslot") {
            this.g=draggedslot.instance.dragslot.g;
            foreach(GameObject mono in invenmanager.inventory.monoliths) {
                mono.GetComponent<weaponmanager>().monolith_reset();
            }
            draggedslot.instance.is_monolith=true;
        }
        else if(draggedslot.instance.dragslot!=null && this.gameObject.tag=="slot") {
            draggedslot.instance.change_idx=this.slot_index;
            draggedslot.instance.change_gd=this.g;
            this.g=draggedslot.instance.dragslot.g;
            draggedslot.instance.is_change=true;
        }
    }
}
