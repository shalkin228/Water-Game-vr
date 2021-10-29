using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : UIElement
{
    public SlotStorageObject storage;
    public Sprite itemSprite;

    [SerializeField] private SlotStorageObject[] _recipe;
    [SerializeField] private float _clickCooldown;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventorySystem.RemoveObject(_recipe))
            {
                InventorySystem.AddItem(storage, itemSprite);
            }
        }
    }
}
