using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PortalInteractable : MonoBehaviour
{
    //message displayed to player when looking at an interactable.
        
    public string promptMessage;
    
    //this function will be called from our player
    public void BaseInteract(GameObject obj)
    {
        Interact(obj);
    }
    
    protected virtual void Interact(GameObject obj)
    {
        //we wont have any cod written in this function
        //this is a template function to be overridden by our subclasses 
    }
}
