using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Crest;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] private ActionBasedController _leftHand, _rightHand;
    [SerializeField] private float _speedDelitel;
    private CharacterController _characterController;
    private ActionBasedContinuousMoveProvider _moveProvider;
    private bool _isLeftMove, _isRightMove, _isUnderWater;

    private void OnEnable()
    {
        _moveProvider = GetComponentInChildren<ActionBasedContinuousMoveProvider>();

        _characterController = GetComponent<CharacterController>();

        _leftHand.uiPressAction.action.performed += ctx => _isLeftMove = true;
        _leftHand.uiPressAction.action.canceled += ctx => _isLeftMove = false;

        _rightHand.uiPressAction.action.performed += ctx => _isRightMove = true;
        _rightHand.uiPressAction.action.canceled += ctx => _isRightMove = false;
    }

    private void Update()
    {
        if(OceanRenderer.Instance.ViewerHeightAboveWater < 0 && !_isUnderWater)
        {
            _moveProvider.useGravity = false;
        }
        else if (OceanRenderer.Instance.ViewerHeightAboveWater > 0.1f && _isUnderWater)
        {
            _moveProvider.useGravity = true;
        }
        _isUnderWater = OceanRenderer.Instance.ViewerHeightAboveWater < 0f;

        if (_isLeftMove && _isUnderWater)
        {
            HandMove(_leftHand.transform.forward);
        }
        if (_isRightMove && _isUnderWater)
        {
            HandMove(_rightHand.transform.forward);
        }
    }

    private void HandMove(Vector3 handForward)
    {
        _characterController.Move(handForward / _speedDelitel);
    }
}
