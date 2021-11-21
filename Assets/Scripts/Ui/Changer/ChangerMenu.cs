using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ChangerMenu : UiInteractBase
{
    [HideInInspector] public Action OnDeactivate;
    public Transform _holdPos;

    [SerializeField] private InputActionReference _inputAction;
    [SerializeField] private Transform _spawnPoint;

    private void OnEnable()
    {
        transform.parent = null;

        _inputAction.action.performed += ctx =>
        {
            transform.position = _spawnPoint.position;
            transform.rotation = _spawnPoint.rotation;

            _isActive = true;
            StartCoroutine(ActivateUICorrutine());
        };
        _inputAction.action.canceled += ctx =>
        {
            OnDeactivate.Invoke();
            _isActive = false;
            StartCoroutine(DeActivateUI());
        };
    }

    protected override void LateUpdate()
    { }
}
