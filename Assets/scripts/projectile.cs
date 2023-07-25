using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float damage;
    public float force;
    public int penet;
    public List<int> curse;
    public bool fire=false;
    public bool ice=false;
    public bool lightn=false;
    public float anti_fireres=0;
    public float anti_iceres=0;
    public float anti_lightres=0;

    public void init(float dam, int pen, int elem, List<int> curse, float force) {
        //투사체의 변수를 할당하고 속성이 있다면 부여
        this.damage=dam;
        this.penet=pen;
        this.curse=curse;
        this.force=force;
        if(elem==1) this.fire=true;
        else if (elem==2) this.ice=true;
        else if (elem==3) this.lightn=true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //적과 충돌시 가지고있는 저주 발동, 관통만큼 적을 관통한 뒤 inactive
        if(collision.gameObject.tag == "Enemy") {
            foreach(int i in curse) {
                curse_use(i, collision);
            }

            if(penet>0) penet--;
            else if(penet==0) this.gameObject.SetActive(false);
        }
    }

    void curse_use(int index, Collider2D collision) {
        switch(index) {
            case 1: //화염 저항
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
            case 7:
            // 폭발 보조가 달려있을 시 투사체가 충돌할때 데미지에 비례한 반경을 가진 폭발 생성
                GameObject exp=gamemanager.instance.poolmng.pulling(7);
                exp.transform.position=this.transform.position;
                exp.GetComponent<magic>().init(7,this.damage*0.3f,this.damage*0.125f, 0, transform);
                this.gameObject.SetActive(false);
                break;
        }
    }
}
