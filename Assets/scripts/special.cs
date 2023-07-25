using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class special : MonoBehaviour
{
    public Coroutine init(weaponmanager wmng) {
       
       Coroutine crt=StartCoroutine(make_shadow(wmng));
       return crt;
    }

    IEnumerator make_shadow(weaponmanager wmng) { //넘겨받은 weaponmanager의 스킬을 그대로 사용하는 그림자를 생성
        while(true) {
        GameObject shadow = gamemanager.instance.poolmng.pulling(3);
        shadow.transform.position = wmng.player.transform.position;
        weaponmanager shawmng=shadow.GetComponent<weaponmanager>();
        shawmng.damage=wmng.damage;
        shawmng.count=wmng.count;
        shawmng.prefabid=wmng.prefabid;
        shawmng.gem_color=wmng.gem_color;
        shawmng.speed=wmng.speed;
        shawmng.radius=wmng.radius;
        shawmng.penet=wmng.penet;
        shawmng.skill_use();
        StartCoroutine(shadow_false(shadow));
        yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator shadow_false(GameObject shadow) {
        yield return new WaitForSeconds(5f);
        shadow.SetActive(false);
    }
}
