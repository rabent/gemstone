using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake() 
    {
        coll = GetComponent<Collider2D>();   
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area")) //area와 충돌시 맵을 이동시킴
            return;

        Vector3 playerPos = gamemanager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        float difx=playerPos.x-myPos.x;
        float dify=playerPos.y-myPos.y;
        float dirx = difx < 0 ? -1 : 1;
        float diry = dify < 0 ? -1 : 1;
        difx = Mathf.Abs(difx);
        dify = Mathf.Abs(dify);

        Vector3 playerDir = gamemanager.instance.player.inputvec;
        

        switch(transform.tag) {
            case "Ground": //맵을 이동
                if (Mathf.Abs(difx - dify) <= 0.1f) {
                    transform.Translate(Vector3.up * diry * 40);
                    transform.Translate(Vector3.right * dirx * 40);
                }
                else if (difx > dify){
                    transform.Translate(Vector3.right * dirx * 40);
                }
                else if (difx < dify){
                    transform.Translate(Vector3.up * diry * 40);
                }
                break;
            case "Enemy": //맵밖의 몬스터를 맵과 함께 근처로 이동
                if (coll.enabled){
                    transform.Translate(playerDir * 40 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }
}
