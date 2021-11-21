using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangerSlot : MonoBehaviour, IWeponChangable
{
    public bool isSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            _isSelected = value;

            _selectedSlotInstance = this;
        }
    }
    public GameObject storage;

    private static ChangerSlot _selectedSlotInstance;
    private ChangerMenu _menu;
    private bool _isSelected;

    private void Start()
    {
        _menu = GetComponentInParent<ChangerMenu>();

        _menu.OnDeactivate += () =>
        {
            if (_selectedSlotInstance == this)
            {
                InteractionItem.RemoveInteractionItem();
                isSelected = false;
                if (storage != null)
                {
                    Instantiate(storage, _menu._holdPos.position, _menu._holdPos.rotation);
                }
            }
        };
    }

    public void ChangeWeapon()
    {
        isSelected = true;
    }
}
