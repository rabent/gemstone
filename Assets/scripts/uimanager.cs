using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uimanager : MonoBehaviour
{
    public GameObject startpannnel;
    public GameObject characterpannel;
    public GameObject gamepannel;
    public GameObject remindpannel;
    // Start is called before the first frame update
   public void startbutton() {
    startpannnel.SetActive(false);
    characterpannel.SetActive(true);
   }
   
   public void characterselect() {
    remindpannel.SetActive(true);
   }

   public void characteryes() {
    characterpannel.SetActive(false);
    gamepannel.SetActive(true);
   }

   public void characterno() {
    remindpannel.SetActive(false);
   }

   public void backtomain() {
    characterpannel.SetActive(false);
    startpannnel.SetActive(true);
   }
}
