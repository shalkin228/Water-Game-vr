using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangerMenu : UiInteractBase
{
    [SerializeField] private InputActionReference _inputAction;

    private void OnEnable()
    {
        print(_inputAction);
        _inputAction.action.performed += ctx =>
        {
            _isActive = true;
            StartCoroutine(ActivateUICorrutine());
            print(1);
        };
        _inputAction.action.canceled += ctx =>
        {
            _isActive = false;
            StartCoroutine(DeActivateUI());
            print(2);
        };
    }
}
