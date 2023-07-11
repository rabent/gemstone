using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    public float damage;
    public int penet;
    public float radius;
    public List<int> curse;
    public bool fire=false;
    public bool ice=false;
    public bool lightn=false;
    public float anti_fireres=0;
    public float anti_iceres=0;
    public float anti_lightres=0;
    public void init(float dam, int pen, int elem, float rad) {
        this.damage=dam;
        this.penet=pen;
        this.radius=rad;
        if(elem==1) this.fire=true;
        else if (elem==2) this.ice=true;
        else if (elem==3) this.lightn=true;
        this.transform.localScale=new Vector3(0.5f*rad, rad, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            foreach(int i in curse) {
                curse_use(i, collision);
            }
        }
    }

    void curse_use(int index, Collider2D collision) {
        switch(index) {
            case 1: //화염 저항 감소
                collision.GetComponent<Enemy>().fireres-=0.3f;
                break;
            case 2: //빙결 저항 감소
                collision.GetComponent<Enemy>().iceres-=0.3f;
                break;
            case 3: //번개 저항 감소
                collision.GetComponent<Enemy>().lightres-=0.3f;
                break;
            case 4: //화염 저항 무시
                this.anti_fireres=0.2f;
                break;
            case 5: //빙결 저항 무시
                this.anti_iceres=0.2f;
                break;
            case 6: //번개 저항 무시
                this.anti_lightres=0.2f;
                break;
        }
    }
}
