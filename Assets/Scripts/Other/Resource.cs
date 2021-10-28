using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [HideInInspector] public int _curHp;
    [SerializeField] private int _maxHp;
    [SerializeField] private Sprite _slotSprite;
    [SerializeField] private SlotStorageObject _itemType;

    private void Start()
    {
        _curHp = _maxHp;
    }

    public void Damage(int damage)
    {
        _curHp -= damage;
        if(_curHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);

        InventorySystem.AddItem(_itemType, _slotSprite);
    }
}
