using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uimanager : MonoBehaviour
{
    public static uimanager ui_instance=null;
    public GameObject startpannnel;
    public GameObject characterpannel;
    public GameObject charremindpannel;
    public GameObject quitremindpannel;
    public GameObject ingame_option;
    public GameObject merchant_pannel;
    public GameObject tuto_pannel;
    public GameObject main_option;
    bool in_game=false;
    public bool pause=false;
    Button fsbtn;
    Button wsbtn;
    Button backbtn;
    public int char_num;
   
   private void Awake() {
    if(ui_instance==null) {
            ui_instance=this;
            DontDestroyOnLoad(this.gameObject);
        }
    else {
        Destroy(ui_instance.gameObject);
        ui_instance=this;
        DontDestroyOnLoad(this.gameObject);
    }
   }
   public void startbutton() {
    startpannnel.SetActive(false);
    characterpannel.SetActive(true);
   }

   public void main_option_button() {
    main_option.SetActive(true);
   }
   public void main_option_back() {
    main_option.SetActive(false);
   }

   public void tuto_button() {
    tuto_pannel.SetActive(true);
   }
   public void tuto_back_button() {
    tuto_pannel.SetActive(false);
   }
   public void characterselect1() {
    char_num=1;
    charremindpannel.SetActive(true);
   }

   public void characterselect2() {
    char_num=2;
    charremindpannel.SetActive(true);
   }

   public void characterselect3() {
    char_num=3;
    charremindpannel.SetActive(true);
   }

   public void characteryes() {
    characterpannel.SetActive(false);
    startpannnel=null;
    characterpannel=null;
    charremindpannel=null;
    quitremindpannel=null;
    SceneManager.LoadScene("in-game");
    in_game=true;
   }

   public void back_to_main() {
    Time.timeScale=1;
    SceneManager.LoadScene("starting scene");
   }

   public void characterno() {
    char_num=0;
    charremindpannel.SetActive(false);
   }

   public void backtomain() {
    characterpannel.SetActive(false);
    startpannnel.SetActive(true);
   }

   public void exit_button() {
    quitremindpannel.SetActive(true);
   }

   public void exit_yes() {
    Debug.Log("exited");
    Application.Quit();
   }

   public void exit_no() {
    quitremindpannel.SetActive(false);
   }

   public void full_screen() {
    Screen.sleepTimeout = SleepTimeout.NeverSleep;
    Screen.SetResolution(1920, 1080, true);
    Debug.Log("full screen");
   }

   public void window_screen() {
    Screen.sleepTimeout = SleepTimeout.NeverSleep;
    Screen.SetResolution(1280, 720, false);
    Debug.Log("windowed");
   }

   public void merchant_on() {
    merchant_pannel=GameObject.Find("Canvas").transform.Find("merchant pannel").gameObject;
    merchant_pannel.SetActive(true);
    Time.timeScale=0;
   }

   private void Update() {
    if(in_game==true && gamemanager.instance!=null) {
        //인벤토리가 꺼져있을때 esc를 누르면 인게임 옵션 패널을 띄워줌
        if(pause==true && Input.GetKeyDown(KeyCode.Escape)) {
            ingame_option.SetActive(false);
            pause=false;
            Time.timeScale=1;
        }
        else if(gamemanager.instance.inv_active==false && Input.GetKeyDown(KeyCode.Escape)) {
            ingame_option=GameObject.Find("Canvas").transform.Find("ingame_option pannel").gameObject;
            ingame_option.SetActive(true);
            Time.timeScale=0;
            pause=true;
            fsbtn=GameObject.Find("full screen button").GetComponent<Button>();
            fsbtn.onClick.AddListener(full_screen);
            wsbtn=GameObject.Find("window screen button").GetComponent<Button>();
            wsbtn.onClick.AddListener(window_screen);
            backbtn=GameObject.Find("back button").GetComponent<Button>();
            backbtn.onClick.AddListener(back_to_main);
        }
        
    }
   }
}
