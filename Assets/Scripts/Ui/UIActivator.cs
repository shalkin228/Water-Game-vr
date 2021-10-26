using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivator : MonoBehaviour
{
    private void Update()
    {
        var camForward = new Ray(transform.position, transform.forward);

        RaycastHit[] UIElements = Physics.RaycastAll(camForward);
        foreach(var UIElement in UIElements)
        {
            if(UIElement.collider.TryGetComponent(out UIShow element))
            {
                element.ActivateUI();
            }
        }
    }
}
