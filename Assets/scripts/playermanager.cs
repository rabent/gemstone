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
        cam.transform.position=new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
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
            Vector3 myAngle = transform.localEulerAngles;
            float randomY = Random.Range(myAngle.y - angleData / 2f, myAngle.y + angleData / 2f);
            Vector3 daggerAngle = dagger.transform.localEulerAngles;
            daggerAngle.y = randomY;
            dagger.transform.localEulerAngles = daggerAngle;
            var forwardVector = dagger.transform.forward;
            float timer = 0;
            while (timer < 1)
            {
                dagger.transform.Translate(forwardVector);
                timer += Time.deltaTime;
                yield return null;
            }
            bigTimer = 0;
        }
    }
}
