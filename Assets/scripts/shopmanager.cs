using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopmanager : MonoBehaviour
{
   public invenmanager inv;
   public GameObject merchant_pannel;
   public Button slot_button;
   public Button link_button;
   public Button[] monolith_arrows;
   GameObject[] monoliths;
   bool in_slot=false;
   bool in_link=false;

   void Start() {
      this.monoliths=inv.monoliths;
   }

   public void slot_lock() { //상점페이즈 내에서 인벤토리와 석판의 슬롯에 락을 걸어줌
      foreach(GameObject obj in inv.slots) {
         slot s=obj.GetComponent<slot>();
         s.islock=true;
      }
      foreach(GameObject obj in monoliths) {
         weaponmanager wpn=obj.GetComponent<weaponmanager>();
         foreach(slot s in wpn.mono_slots) {
            if(s.gameObject.activeSelf==true) {
               s.islock=true;
            }
         }
      }
   }

   public void slot_unlock() { //상점페이즈에서 나갈때 걸려있던 락을 모두 풀어줌
      foreach(GameObject obj in inv.slots) {
         slot s=obj.GetComponent<slot>();
         s.islock=false;
      }
      foreach(GameObject obj in monoliths) {
         weaponmanager wpn=obj.GetComponent<weaponmanager>();
         foreach(slot s in wpn.mono_slots) {
            if(s.gameObject.activeSelf==true) {
               s.islock=false;
            }
         }
      }
   }

   public void slot_open() { //슬롯 개방 메뉴에 들어갈 시 필요한 패널과 버튼을 띄워줌
      in_slot=true;
      slot_button.gameObject.SetActive(false);
      link_button.gameObject.SetActive(false);
      inv.inv_pannel.SetActive(true);
      inv.slot_refresh();
      slot_lock();
      foreach(Button btn in monolith_arrows) {
         btn.gameObject.SetActive(true);
      }
   }

   public void open_monolith0() { //각 석판의 개방 함수
      weaponmanager wpn=monoliths[0].GetComponent<weaponmanager>();
      wpn.slot_expand();
   }

   public void open_monolith1() {
      weaponmanager wpn=monoliths[1].GetComponent<weaponmanager>();
      wpn.slot_expand();
   }

   public void open_monolith2() {
      weaponmanager wpn=monoliths[2].GetComponent<weaponmanager>();
      wpn.slot_expand();
   }

   public void open_monolith3() {
      weaponmanager wpn=monoliths[3].GetComponent<weaponmanager>();
      wpn.slot_expand();
   }

   public void return_button() { //슬롯개방 화면일땐 원래 상점으로, 상점 메인화면에선 스테이지로 돌아감
      if(in_slot==true) {
         in_slot=false;
         foreach(Button btn in monolith_arrows) {
            btn.gameObject.SetActive(false);
         }
         slot_button.gameObject.SetActive(true);
         link_button.gameObject.SetActive(true);
         slot_unlock();
         inv.inv_pannel.SetActive(false);
      }
      else if(in_link==true) {

      }
      else {
         Time.timeScale=1;
         Debug.Log("merchant close");
         merchant_pannel.SetActive(false);
      }
   }
}
