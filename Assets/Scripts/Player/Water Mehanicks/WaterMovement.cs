using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Crest;

public class WaterMovement : MonoBehaviour
{
    public bool isUnderWater, canChangeGravity = true;

    [SerializeField] private InputActionProperty _leftHand, _rightHand;
    [SerializeField] private Transform _leftHandTransform, _rightHandTransform;
    [SerializeField] private float _gliderSpeed, _normalSpeed;
    private CharacterController _characterController;
    private ActionBasedContinuousMoveProvider _moveProvider;
    private Vector2 _input;
    private float _standartMoveSpeedOnGround;

    private void OnEnable()
    {
        _moveProvider = GetComponentInChildren<ActionBasedContinuousMoveProvider>();
        _standartMoveSpeedOnGround = _moveProvider.moveSpeed;

        _characterController = GetComponent<CharacterController>();

        _leftHand.action.performed += ctx => _input = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        UpdateUndewater();

        if (canChangeGravity)
        {
            _moveProvider.useGravity = !isUnderWater;
        }
    }

    private void FixedUpdate()
    {
        if(isUnderWater && _input.x != 0 && _input.y != 0)
        {
            _characterController.Move
                ((_leftHandTransform.forward * _input.y + _leftHandTransform.right * _input.x) * _normalSpeed);
        }
    }

    private void HandMove(Vector2 input)
    {
        _input = input;
    }

    public void UpdateUndewater()
    {
        float heightAboveWater = transform.position.y - OceanRenderer.Instance.SeaLevel;

        if (heightAboveWater < -.5f && !isUnderWater)
        {
            isUnderWater = true;
            if (canChangeGravity)
            {
                _moveProvider.moveSpeed = 0;
                _moveProvider.useGravity = false;
            }
        }
        else if (heightAboveWater > -.5f && isUnderWater)
        {
            isUnderWater = false;
            if (canChangeGravity)
            {
                _moveProvider.moveSpeed = _standartMoveSpeedOnGround;
                _moveProvider.useGravity = true;
            }
        }
    }
}
