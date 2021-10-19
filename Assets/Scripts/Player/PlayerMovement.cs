using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private ActionBasedController _leftHand, _rightHand;
    [SerializeField] private float _speed;
    private CharacterController _characterController;
    private Camera _mainCamera;



    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _leftHand.translateAnchorAction.action.performed
            += ctx => Move(ctx.ReadValue<Vector2>().x);

        _mainCamera = Camera.main;
    }

    private void Move(float inputY)
    {
        print(inputY);
        //_characterController.Move(_mainCamera.transform.forward * inputY * _speed
            //+ t);
    }
}
