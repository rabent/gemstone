using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;

public class slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
   private gemData pgem;
   public Image slot_img;
   public bool islock=false;
   public bool isfull=false;
   public bool begin_mono=false;
   public int slot_index;
   public GameObject pannel;
   public TMP_Text title;
   public TMP_Text explain;
   public TMP_Text tags;

   public gemData g { //젬 데이터가 있다면 투명화를 해제
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

   void OnDisable() {
    pannel.SetActive(false);
   }

    public void OnPointerClick(PointerEventData eventData) {
        if(eventData.button==PointerEventData.InputButton.Right) {
            if(this.g!=null) {
                g=null;
                gamemanager.instance.gold+=10;
                invenmanager.inventory.gemlist_refresh();
            }
        }
    }

   public void OnPointerEnter(PointerEventData eventData) {
    //마우스 올리면 젬의 정보 패널을 띄움
        if(this.isfull) {
            pannel.SetActive(true);
            title.text=g.gem_name;
            explain.text=g.gem_explain;
            string str="";
            foreach(string s in g.tags) {
                str+=s + ",";
            }
            if(g.ispassive) {
                foreach(string s in g.required_tag) {
                    str+="<color=#800000ff><b>" + s + "</b></color>" + ",";
                }
            }
            str=str.Remove(str.Length - 1, 1);
            this.tags.text=str;
            Debug.Log("mouse enter");
        }
   }

    public void OnPointerExit(PointerEventData eventData) {
        //마우스 뗐을 때 창 사라짐
        if(pannel.activeSelf==true) {
            pannel.SetActive(false);
            Debug.Log("mouse exit");
        }
    }

   
    public void OnBeginDrag(PointerEventData eventData)
    { //슬롯에 젬이 있을시 슬롯을 클릭하면 draggedslot에 그 슬롯의 데이터를 복사해서 넘겨줌
        pannel.SetActive(false);
        if(isfull && !islock) {
            if(this.gameObject.tag=="monoslot") begin_mono=true;
            draggedslot.instance.dragslot=this;
            draggedslot.instance.dragset(slot_img);
            draggedslot.instance.transform.position=eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    { //마우스 이동에 따라 draggedslot이 이동
        if(isfull && !islock) {
            draggedslot.instance.transform.position=eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    { //드래그가 끝났을 시 처음에 클릭했던 슬롯에서 발동하는 함수
    //드래그의 종착점이 monolith인지, 다른 슬롯인지에 따라서 필요한 절차를 진행
        if(draggedslot.instance.is_monolith==true && draggedslot.instance.is_change==false && !islock) {
            this.g=null;
            invenmanager.inventory.gemlist[slot_index]=null;
            draggedslot.instance.is_monolith=false;
        }
        else if(draggedslot.instance.is_change==true && !islock) {
            Debug.Log(draggedslot.instance.change_gd);
            this.g=draggedslot.instance.change_gd;
            int idx=draggedslot.instance.change_idx;
            if(draggedslot.instance.is_monolith) {
                invenmanager.inventory.gemlist[slot_index]=draggedslot.instance.change_gd;
                draggedslot.instance.is_monolith=false;
            }
            else {
                invenmanager.inventory.gemlist[idx]=this.g;
                if(!begin_mono) invenmanager.inventory.gemlist[slot_index]=draggedslot.instance.change_gd;
            }
            draggedslot.instance.change_gd=null;
            draggedslot.instance.change_idx=-1;
            draggedslot.instance.is_change=false;
        }
        if(begin_mono && !islock) {
             foreach(GameObject mono in invenmanager.inventory.monoliths) {
                mono.GetComponent<weaponmanager>().monolith_reset();
            }
        }
        draggedslot.instance.drag_invisible(0);
        draggedslot.instance.dragslot=null;
        begin_mono=false;
        invenmanager.inventory.gemlist_refresh();
    }

    public void OnDrop(PointerEventData eventData)
    { //enddrag보다 먼저 발동하는 함수로 드래그가 끝난 위치에 있는 슬롯에서 발동
    //드래그가 끝난 위치가 monolith라면 젬데이터를 monolith로 넘겨주고 refresh
    //드래그가 끝난 위치가 다른 슬롯이라면 그 슬롯에 draggedslot의 데이터를 넘기고 슬롯의 데이터를 받아옴
        if(draggedslot.instance.dragslot!=null && this.gameObject.tag=="monoslot" && !islock) {
            if(this.g!=null) {
                draggedslot.instance.change_idx=this.slot_index;
                draggedslot.instance.change_gd=this.g;
                draggedslot.instance.is_change=true;
            }
            this.g=draggedslot.instance.dragslot.g;
            foreach(GameObject mono in invenmanager.inventory.monoliths) {
                mono.GetComponent<weaponmanager>().monolith_reset();
            }
            draggedslot.instance.is_monolith=true;
        }
        else if(draggedslot.instance.dragslot!=null && this.gameObject.tag=="slot" && !islock) {
            draggedslot.instance.change_idx=this.slot_index;
            draggedslot.instance.change_gd=this.g;
            this.g=draggedslot.instance.dragslot.g;
            draggedslot.instance.is_change=true;
        }
    }
}
