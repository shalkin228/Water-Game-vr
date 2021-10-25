using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandInteractor : MonoBehaviour
{
    public ControllerType controllerType;

    [SerializeField] private float _interactionSphereRadius;
    [SerializeField] private Transform _itemHoldPos, _interactionSpherePos;
    private ActionBasedController _controller;
    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();

        _controller = GetComponent<ActionBasedController>();

        _controller.selectAction.action.performed += ctx => InteractionTake();
        _controller.selectAction.action.canceled += ctx => InteractionDrop();
    }

    private void InteractionTake()
    {
        if (_itemHoldPos.childCount > 0)
            return;

        InteractionItem interactionItem = null;

        Collider[] circleItems = Physics.OverlapSphere
            (_interactionSpherePos.position, _interactionSphereRadius);
        foreach(var circleItem in circleItems)
        {
            if(circleItem.TryGetComponent<InteractionItem>(out InteractionItem item))
            {
                interactionItem = item;
                break;
            }
        }

        if (interactionItem == null || interactionItem.transform.parent != null)
            return;

        if (_animator != null)
            _animator.SetBool("MultiTool", true);

        interactionItem.transform.position = _itemHoldPos.position;
        interactionItem.transform.rotation = _itemHoldPos.rotation;
        interactionItem.transform.parent = _itemHoldPos;
        interactionItem.isTaked = true;
    }

    private void InteractionDrop()
    {
        Transform interactionItem = null;
        foreach(Transform child in transform.GetChild(0))
        {
            if (child.TryGetComponent(out InteractionItem item))
            {
                interactionItem = child;
                break;
            }
        }
        if(interactionItem == null)
        {
            print(2);
            return;
        }

        if (_animator != null)
            _animator.SetBool("MultiTool", false);

        interactionItem.parent = null;
        interactionItem.GetComponent<InteractionItem>().isTaked = false;
    }
}
public enum ControllerType { Right, Left}
