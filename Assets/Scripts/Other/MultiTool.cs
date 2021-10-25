using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiTool : InteractionItem
{
    [SerializeField] private InputActionReference _rightHandAction, _leftHandAction;
    [SerializeField] private Transform _itemSocket, _sphere;
    [SerializeField] private float _maxDistance, _hitCooldown, _maxRotationSpeed;
    

    private void OnEnable()
    {
        _rightHandAction.action.performed += ctx => HitItem(ControllerType.Right);
        _leftHandAction.action.performed += ctx => HitItem(ControllerType.Left);
    }

    private void HitItem(ControllerType type)
     {
        if (transform.parent == null)
            return;
        if (transform.parent.parent.GetComponent<HandInteractor>().controllerType != type)
            return;

        RaycastHit[] raycastHits = Physics.RaycastAll(_itemSocket.position, _itemSocket.forward);
        foreach(var raycastHit in raycastHits)
        {
            if(raycastHit.collider.TryGetComponent(out Resource resource) && raycastHit.distance < _maxDistance)
            {
                StartCoroutine(Hitting(resource));
                break;
            }
        }
     }

    private void StopHit()
    {
        StopCoroutine(Hitting(new Resource()));
    }

    private IEnumerator Hitting(Resource resource)
    {
        while(!(resource._curHp <= 0))
        {
            resource.Damage(1);

            print(resource._curHp);

            yield return new WaitForSeconds(_hitCooldown);
        }
    }
}
