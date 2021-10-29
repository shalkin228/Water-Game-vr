using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : UIElement
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

    [SerializeField] private Transform _holdPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(storage == SlotStorageObject.Multitool)
            {
                var multiTool = Resources.Load<GameObject>("Multi Tool");
                Instantiate(multiTool, _holdPos.position, Quaternion.identity);
            }
        }
    }
}
public enum SlotStorageObject
{
    Empty, Multitool, Coral, Smazka, Processor, Kalii, Gold, Olov, Pharfor
}
