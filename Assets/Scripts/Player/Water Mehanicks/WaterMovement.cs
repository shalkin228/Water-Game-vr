using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] private ActionBasedController _leftHandMoveAction, _rightHandMoveAction;

    private void OnEnable()
    {
        _leftHandMoveAction.uiPressAction.action.performed += ctx => print(ctx.ReadValue<float>());
    }
}
