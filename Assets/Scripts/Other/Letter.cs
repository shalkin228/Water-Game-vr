using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Letter : UiInteractBase
{
    [SerializeField] private Transform _teleportPos;
    [SerializeField] private InputActionReference _actionReference;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;

        _actionReference.action.performed += ctx => RiseUp();
    }

    private void RiseUp()
    {
        if (_activating == ActivatingType.DeActivating)
            return;

        var player = FindObjectOfType<WaterMovement>().gameObject;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = _teleportPos.position;
        player.GetComponent<CharacterController>().enabled = true;
    }

    private void Update()
    {
        if(_activating == ActivatingType.Activating)
        {
            _canvas.transform.LookAt(_mainCamera.transform);
        }
    }
}
