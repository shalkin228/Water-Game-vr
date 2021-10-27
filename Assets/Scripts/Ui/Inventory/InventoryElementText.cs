using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryElementText : InventoryElement
{
    private TextMeshProUGUI _text;

    protected override void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public override IEnumerator Activate()
    {
        isActive = true;
        while (isActive)
        {
            Color newColor = _text.color;
            newColor.a += Time.deltaTime;
            _text.color = newColor;

            yield return null;
        }
    }

    public override IEnumerator DeActivate()
    {
        isActive = false;
        while (!isActive)
        {
            Color newColor = _text.color;
            newColor.a -= Time.deltaTime;
            _text.color = newColor;

            yield return null;
        }
    }
}
