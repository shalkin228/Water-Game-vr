using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventorySystem : MonoBehaviour, UIShow
{
    [SerializeField] private float _uiActiveTime;
    [SerializeField] private Transform _inventoryPanel;
    private static InventorySystem _instance;
    private bool _activated;
    private int _curWatching;
    private List<Slot> _slots = new List<Slot>();
    private List<InventoryElement> _elements = new List<InventoryElement>();

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        foreach(Transform child in transform)
        {
            if(child.TryGetComponent(out Slot slot))
            {
                _slots.Add(slot);
            }

            _elements.Add(child.GetComponent<InventoryElement>());
        }
    }

    private IEnumerator ActiveTimer()
    {
        var oldWatching = _curWatching;

        yield return new WaitForSeconds(_uiActiveTime);

        if (_curWatching != oldWatching)
        {
            yield break;
        }

        _activated = false;

        Deactivating();

        _curWatching = 0;
    }

    private void Activating()
    {
        foreach(InventoryElement element in _elements)
        {
            element.StartCoroutine(element.Activate());
        }
    }

    private void Deactivating()
    {
        foreach (InventoryElement element in _elements)
        {
            element.StartCoroutine(element.DeActivate());
        }
    }

    public static void AddItem(SlotStorageObject storageObject, Sprite slotSprite)
    {
        foreach(Slot slot in _instance._slots)
        {
            if(slot.storage == SlotStorageObject.Empty)
            {
                slot.storage = storageObject;
                slot.slotSprite = slotSprite;
                break;
            }
        }
    }

    public void ActivateUI()
    {
        _curWatching++;
        if (_curWatching < 10)
        {
            return;
        }

        StartCoroutine(ActiveTimer());

        if (_activated)
            return;

        _activated = true;

        Activating();
    }
}
