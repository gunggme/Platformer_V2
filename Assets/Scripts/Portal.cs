using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : PortalInteractable
{
    public Vector3 position;
    
    protected override void Interact(GameObject obj)
    {
        obj.transform.position = position;
        base.Interact(obj);
    }
}
