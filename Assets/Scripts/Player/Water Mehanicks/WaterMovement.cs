using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Crest;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] private ActionBasedController _leftHand, _rightHand;
    [SerializeField] private float _gliderDivider, _normalSpeedDivider;
    private CharacterController _characterController;
    private ActionBasedContinuousMoveProvider _moveProvider;
    private bool _isLeftMove, _isRightMove, _isUnderWater;

    private void OnEnable()
    {
        _moveProvider = GetComponentInChildren<ActionBasedContinuousMoveProvider>();

        _characterController = GetComponent<CharacterController>();

        _leftHand.uiPressAction.action.performed += ctx => _isLeftMove = true;
        //_leftHand.uiPressAction.action.canceled += ctx =>
        //StartCoroutine(StopMove(false)); 

        _rightHand.uiPressAction.action.performed += ctx => _isRightMove = true;
        //_rightHand.uiPressAction.action.canceled += ctx => 
        //StartCoroutine(StopMove(true));
    }

    private void Update()
    {
        if(OceanRenderer.Instance.ViewerHeightAboveWater < -0.2f && !_isUnderWater)
        {
            _isUnderWater = true;
            _moveProvider.forwardSource = Camera.main.transform;
        }
        else if (OceanRenderer.Instance.ViewerHeightAboveWater > 0.2f && _isUnderWater)
        {
            _isUnderWater = false;
            _moveProvider.forwardSource = null;
        }
        _moveProvider.useGravity = !_isUnderWater;

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
        _characterController.Move(handForward / _gliderDivider);
    }

    private IEnumerator StopMove(bool isRightHand)
    {
        if (isRightHand)
        {
            _isRightMove = false;
        }
        else
        {
            _isLeftMove = false;
        }

        if (_isRightMove || _isLeftMove)
            yield break;

        while (_characterController.velocity.x == 0 && _characterController.velocity.y == 0 &&
            _characterController.velocity.z == 0 || _isRightMove || _isLeftMove)
        {
            _characterController.Move(new Vector3(
                _characterController.velocity.x - Time.deltaTime / 10,
                _characterController.velocity.y - Time.deltaTime / 10,
                _characterController.velocity.z - Time.deltaTime / 10));
        }
    }
}
