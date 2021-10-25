using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiTool : InteractionItem
{
    [SerializeField] private InputActionReference _rightHandAction, _leftHandAction;
    [SerializeField] private Transform _itemSocket;

    private void OnEnable()
    {
        _rightHandAction.action.performed += ctx => TakeItem(ControllerType.Right);
        _leftHandAction.action.performed += ctx => TakeItem(ControllerType.Left);
    }

     private void TakeItem(ControllerType type)
     {
         
     }

}
