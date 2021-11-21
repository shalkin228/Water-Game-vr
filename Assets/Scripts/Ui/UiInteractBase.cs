using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UiInteractBase : MonoBehaviour, UIShow
{
    [SerializeField] protected float _deActivateSpeed, _activateSpeed;
    protected bool _isActive;
    protected ActivatingType _activating = ActivatingType.DeActivating;
    protected Canvas _canvas;

    private Vector3 _normalScale;

    private void Start()
    {
        _canvas = GetComponentInChildren<Canvas>();

        _normalScale = _canvas.transform.localScale;
        _canvas.transform.localScale = new Vector3(_canvas.transform.localScale.x,
            0, _canvas.transform.localScale.z);
    }

    protected virtual void LateUpdate()
    {
        if (_isActive)
        {
            _isActive = false;
        }
        else if (_activating == ActivatingType.Activating)
        {
            StartCoroutine(DeActivateUI());
        }
    }

    public void ActivateUI()
    {
        _isActive = true;

        if (_activating == ActivatingType.DeActivating)
        {
            StartCoroutine(ActivateUICorrutine());        
        }

        _activating = ActivatingType.Activating;
    }

    protected IEnumerator ActivateUICorrutine()
    {
        while (_isActive)
        {
            var currentScale = _canvas.transform.localScale;
            currentScale = Vector3.Lerp(currentScale, _normalScale,
                Time.deltaTime * _activateSpeed);
            _canvas.transform.localScale = currentScale;
            yield return null;
        }
    }

    protected IEnumerator DeActivateUI()
    {
        _isActive = false;
        _activating = ActivatingType.DeActivating;

        while (!_isActive)
        {
            var currentScale = _canvas.transform.localScale;
            currentScale = Vector3.Lerp(currentScale, new Vector3(currentScale.x, 0, currentScale.y),
                Time.deltaTime * _deActivateSpeed);
            _canvas.transform.localScale = currentScale;
            yield return null;
        }
    }

    protected enum ActivatingType { Activating, DeActivating }
}
