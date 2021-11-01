using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : UIElement
{
    private Sprite _slotSprite;

    public SlotStorageObject storage;
    public Sprite slotSprite
    {
        get
        {
            return _slotSprite;
        }
        set
        {
            _slotSprite = value;

            _image.sprite = _slotSprite;
        }
    }

    [SerializeField] private Transform _holdPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(storage == SlotStorageObject.Multitool && isActive)
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
