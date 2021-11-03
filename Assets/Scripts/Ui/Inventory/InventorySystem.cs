using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private Transform _inventoryPanel;
    [SerializeField] private Sprite _emptySlot;
    [SerializeField] private InputActionReference _openInventoryAction;
    private static InventorySystem _instance;
    private bool _activated;
    private List<Slot> _slots = new List<Slot>();
    private float _openedScaleY;

    private void Awake()
    {
        _instance = this;
        _openedScaleY = transform.localScale.y;

        transform.localScale = new Vector3(transform.localScale.x,
            0, transform.localScale.z);
    }

    private void OnEnable()
    {
        _openInventoryAction.action.performed += ctx => StartCoroutine(ActivateUI());
        _openInventoryAction.action.canceled += ctx => StartCoroutine(DeActivateUI());
    }

    private void Start()
    {
        foreach(Transform child in transform)
        {
            if(child.TryGetComponent(out Slot slot))
            {
                _slots.Add(slot);
            }
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

    public static bool IsItemExist(SlotStorageObject[] existObjects)
    {
        int existObjectsNum = 0;

        foreach (Slot slot in _instance._slots)
        {
            foreach(SlotStorageObject existObject in existObjects)
            {
                if (existObject == slot.storage)
                {
                    existObjectsNum++;
                    break;
                }
            }
        }

        if (existObjectsNum < existObjects.Length)
            return false;

        return true;
    }

    public static bool RemoveObject(SlotStorageObject[] removingObjects)
    {
        var removingObjectReference = removingObjects;

        if (!IsItemExist(removingObjects))
        {
            return false;
        }

        foreach (SlotStorageObject removingObject in removingObjectReference)
        {
            foreach (Slot slot in _instance._slots)
            {
                if(slot.storage == removingObject)
                {
                    slot.storage = SlotStorageObject.Empty;
                    slot.slotSprite = _instance._emptySlot;
                }
            }
        }

        return true;
    }

    public IEnumerator ActivateUI()
    {
        _activated = true;

        while (_activated)
        {
            var scaleY = transform.localScale.y;
            scaleY = Mathf.Lerp(scaleY, _openedScaleY, Time.deltaTime * 12);

            transform.localScale = new Vector3(transform.localScale.x,
    scaleY, transform.localScale.z);

            yield return null;
        }
    }

    public IEnumerator DeActivateUI()
    {
        _activated = false;

        while (!_activated)
        {
            var scaleY = transform.localScale.y;
            scaleY = Mathf.Lerp(scaleY, 0, Time.deltaTime * 12);

            transform.localScale = new Vector3(transform.localScale.x,
    scaleY, transform.localScale.z);

            yield return null;
        }
    }
}
