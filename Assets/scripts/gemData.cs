using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class gemData : ScriptableObject
{
   

    public bool isactive; //액티브 젬인지
    public bool ispassive; //패시브 젬인지
    public bool isspecial; //스페셜 젬인지
    public int curse; //저주 효과가 있는지
    public int color; //색깔이 어떤지
    public int id; //id 번호가 몇번인지
    public float damage; //액티브라면 피해가 몇인지
    public float force; //넉백 강도가 몇인지
    public int element; //원소 속성이 어떤 것인지
    public int count; //한번에 몇번 발동되는지
    public float speed=1; //탄속이 어느정도인지
    public Sprite spr; //젬의 이미지가 어떤지
    public float radius=1; //공격의 반경이 어떤지
    public int penet; //몇번 관통하는지
    public float delay_reduct=1;

    public string gem_name;
    [TextArea]
    public string gem_explain;
    public List<string> tags;
    public List<string> required_tag;
}
