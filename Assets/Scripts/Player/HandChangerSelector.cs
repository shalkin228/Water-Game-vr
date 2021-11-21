using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandChangerSelector : MonoBehaviour
{
    private void Update()
    {
        var direction = new Ray(transform.position, -transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(direction, 2);

        foreach (var hit in hits)
        {
            if(hit.collider.TryGetComponent(out IWeponChangable weaponChangable))
            {
                weaponChangable.ChangeWeapon();
            }
        }
    }
}
