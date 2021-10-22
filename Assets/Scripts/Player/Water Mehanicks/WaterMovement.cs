using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Crest;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] private InputActionProperty _leftHand, _rightHand;
    [SerializeField] private Transform _leftHandTransform, _rightHandTransform;
    [SerializeField] private float _gliderSpeed, _normalSpeed;
    private CharacterController _characterController;
    private ActionBasedContinuousMoveProvider _moveProvider;
    private bool _isLeftMove, _isRightMove, _isUnderWater;
    private float _standartMoveSpeedOnGround;

    private void OnEnable()
    {
        _moveProvider = GetComponentInChildren<ActionBasedContinuousMoveProvider>();
        _standartMoveSpeedOnGround = _moveProvider.moveSpeed;

        _characterController = GetComponent<CharacterController>();

        _leftHand.action.performed += ctx => HandMove(_leftHandTransform.forward, _leftHandTransform.right,
            ctx.ReadValue<Vector2>());
    }

    private void Update()
    {
        if(OceanRenderer.Instance.ViewerHeightAboveWater < -0.2f && !_isUnderWater)
        {
            _isUnderWater = true;
            _moveProvider.moveSpeed = 0;
            _moveProvider.useGravity = false;
        }
        else if (OceanRenderer.Instance.ViewerHeightAboveWater > 0.2f && _isUnderWater)
        {
            _isUnderWater = false;
            _moveProvider.moveSpeed = _standartMoveSpeedOnGround;
            _moveProvider.useGravity = true;
        }
        _moveProvider.useGravity = !_isUnderWater;

        if (_isLeftMove && _isUnderWater)
        {
            // HandMove(_leftHand.transform.forward);
        }
        if (_isRightMove && _isUnderWater)
        {
            //HandMove(_rightHand.transform.forward);
        }
    }

    private void HandMove(Vector3 handForward, Vector3 handRight, Vector2 input)
    {
        if (!_isUnderWater)
            return;

        print(input);
        input = input * _normalSpeed;
        _characterController.Move(handForward * input.y + handRight * input.x);
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
