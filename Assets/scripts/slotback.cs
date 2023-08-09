using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slotback : MonoBehaviour
{
    public slot slot;
    public Image img;

    public void slot_disable() {
        slot.gameObject.SetActive(false);
        img.gameObject.SetActive(true);
    }

    public void slot_active() {
        slot.gameObject.SetActive(true);
        img.gameObject.SetActive(false);
    }
}
