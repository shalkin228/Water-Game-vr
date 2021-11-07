using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UiInteractBase : MonoBehaviour, UIShow
{
    [SerializeField] protected Canvas _canvas;
    protected bool _isActive;

    private void Start()
    {
        _canvas.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (_isActive)
        {
            _canvas.gameObject.active = true;

            _isActive = false;
        }
        else
        {
            _canvas.gameObject.active = false;
        }
    }

    public void ActivateUI()
    {
        _isActive = true;
    }

    
}
