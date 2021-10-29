using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private float _uiActiveTime;
    [SerializeField] private Transform _inventoryPanel;
    [SerializeField] private Sprite _emptySlot;
    [SerializeField] private InputActionReference _openInventoryAction;
    private static InventorySystem _instance;
    private bool _activated;
    private int _curWatching;
    private List<Slot> _slots = new List<Slot>();
    private List<UIElement> _elements = new List<UIElement>();

    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        _openInventoryAction.action.performed += ctx => ActivateUI();
    }

    private void Start()
    {
        foreach(Transform child in transform)
        {
            if(child.TryGetComponent(out Slot slot))
            {
                _slots.Add(slot);
            }

            _elements.Add(child.GetComponent<UIElement>());
        }
    }

    private IEnumerator ActiveTimer()
    {
        var oldWatching = _curWatching;

        yield return new WaitForSeconds(_uiActiveTime);

        _activated = false;

        Deactivating();

        _curWatching = 0;
    }

    private void Activating()
    {
        foreach(UIElement element in _elements)
        {
            element.StartCoroutine(element.Activate());
        }
    }

    private void Deactivating()
    {
        foreach (UIElement element in _elements)
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

    public static bool IsItemExist(SlotStorageObject[] existObjects)
    {
        int currentIteration = 0;
        foreach(SlotStorageObject existObject in existObjects)
        {
            foreach(Slot slot in _instance._slots)
            {
                if(slot.storage == existObject)
                {
                    existObjects[currentIteration] = SlotStorageObject.Empty;
                    break;
                }
            }
            currentIteration++;
        }

        foreach(SlotStorageObject existObject in existObjects)
        {
            if (!(existObject == SlotStorageObject.Empty))
            {
                print(1);
                return false;
            }
        }
        return true;
    }

    public static bool RemoveObject(SlotStorageObject[] removingObjects)
    {

        foreach(SlotStorageObject removingObject in removingObjects)
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

    public void ActivateUI()
    {
        StartCoroutine(ActiveTimer());

        if (_activated)
            return;

        _activated = true;

        Activating();
    }
}
