using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class draggedslot : MonoBehaviour
{
    static public draggedslot instance;
    public slot dragslot;
    public int change_idx=-1;
    public gemData change_gd;
    [SerializeField]
    Image itemimg;
    public bool is_change=false;
    public bool is_monolith=false;

    void Start()
    {
        instance=this;
    }

    public void dragset(Image img) 
    {
        itemimg.sprite=img.sprite;
        drag_invisible(1);
    }

    public void drag_invisible(float f) {
        Color color=itemimg.color;
        color.a=f;
        itemimg.color=color;
    }
}
