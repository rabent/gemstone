using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermanager : MonoBehaviour
{
    public GameObject player;
    public GameObject daggerObject;
    public List<GameObject> daggerPool;
    public float angleData = 180f;
    public float speed = 100f;
    public Camera cam;
    

    void Start() {
        StartCoroutine(DaggerActionCoroutine());
    }
    
    void Update() {
        if(Input.GetKey(KeyCode.D)) {
            transform.Translate(new Vector3(speed*Time.deltaTime,0,0));
        }
        if(Input.GetKey(KeyCode.A)) {
            transform.Translate(new Vector3(-speed*Time.deltaTime,0,0));
            Debug.Log("ddd");
        }
        if(Input.GetKey(KeyCode.W)) {
            transform.Translate(new Vector3(0,speed*Time.deltaTime,0));
        }
        if(Input.GetKey(KeyCode.S)) {
            transform.Translate(new Vector3(0,-speed*Time.deltaTime,0));
        }
        cam.transform.position=new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z-10);
    }
     IEnumerator DaggerActionCoroutine()
    {
        float bigTimer = 0;
        while(true)
        {
            if(bigTimer < 1)
            {
                bigTimer += Time.deltaTime;
            }
            GameObject dagger = GameObject.Instantiate(daggerObject, null);
            dagger.transform.position = this.transform.position;
            float x=Random.Range(transform.position.x, transform.position.x+50);
            float y=Random.Range(transform.position.y+50,transform.position.y-50);
            Vector3 dir=new Vector3(x,y,0)-transform.position;
            dir=dir.normalized;
            dagger.transform.rotation=Quaternion.FromToRotation(Vector3.up,dir);
            float timer = 0;
            Rigidbody2D rigid=dagger.GetComponent<Rigidbody2D>();
            while (timer < 1)
            {
                rigid.velocity=dir*50;
                timer += Time.deltaTime;
                yield return null;
            }
            bigTimer = 0;
        }
    }
}
