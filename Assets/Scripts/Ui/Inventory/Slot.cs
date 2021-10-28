using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : InventoryElement
{
    public SlotStorageObject storage;
    public Sprite slotSprite
    {
        get
        {
            return slotSprite;
        }
        set
        {
            _image.sprite = value;

            slotSprite = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print(1);
        }
    }
}
public enum SlotStorageObject
{
    Empty, Multitool, Coral, Smazka, Processor, Kalii, Gold, Olov, Porcelain
}
