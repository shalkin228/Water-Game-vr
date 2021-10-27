using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Crest;

public class OceanHeightShower : MonoBehaviour, UIShow
{
    [SerializeField] private float _uiActiveTime;
    private TextMeshProUGUI _text;
    private bool _activated;
    private int _curWatching;

    private void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        int depth = (int)Mathf.Min(Camera.main.transform.position.y - OceanRenderer.Instance.SeaLevel, 0);
        _text.text = "Depth: " + Mathf.Abs(depth);
    }

    private IEnumerator ActiveTimer()
    {
        var oldWatching = _curWatching;

        yield return new WaitForSeconds(_uiActiveTime);

        if(_curWatching != oldWatching)
        {
            yield break;
        }

        _activated = false;

        StartCoroutine(Deactivating());

        _curWatching = 0;
    }

    private IEnumerator Activating()
    {
        while (_activated)
        {
            Color newTextColor = _text.color;
            newTextColor.a += Time.deltaTime;
            _text.color = newTextColor;

            yield return null;
        }
    }

    private IEnumerator Deactivating()
    {
        while (_text.color.a > 0)
        {
            Color newTextColor = _text.color;
            newTextColor.a -= Time.deltaTime;
            _text.color = newTextColor;

            yield return null;
        }
    }

    public void ActivateUI()
    {
        _curWatching++;
        if(_curWatching < 10)
        {
            return;
        }

        StopCoroutine(Deactivating());

        StartCoroutine(ActiveTimer());

        if (_activated)
            return;

        _activated = true;

        StartCoroutine(Activating());
    }
}
