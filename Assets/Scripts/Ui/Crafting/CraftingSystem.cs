using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour, UIShow
{
    [SerializeField] private float _uiActiveTime;
    private bool _activated;
    private int _curWatching;
    private List<Slot> _slots = new List<Slot>();
    private List<UIElement> _elements = new List<UIElement>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out Slot slot))
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
        foreach (UIElement element in _elements)
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
