using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour
{
    public bool isActive;

    protected Image _image;

    protected virtual void Start()
    {
        _image = GetComponent<Image>();
    }

    public virtual IEnumerator Activate()
    {
        isActive = true;
        while (isActive)
        {
            Color newColor = _image.color;
            newColor.a += Time.deltaTime;
            _image.color = newColor;

            yield return new WaitForSeconds(.02f);
        }
    }

    public virtual IEnumerator DeActivate()
    {
        isActive = false;
        while (!isActive)
        {
            Color newColor = _image.color;
            newColor.a -= Time.deltaTime;
            _image.color = newColor;

            yield return new WaitForSeconds(.02f);
        }
    }
}
