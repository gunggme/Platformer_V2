using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private Transform Interact;

    [SerializeField] private float distance;
    [SerializeField] private LayerMask mask;

    void Update()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(Interact.position, Vector2.right * gameObject.transform.localScale.x, distance, mask);
        //debug용으로 레이 그림으로 그리기
        //Debug.DrawRay(Interact.position, Vector2.right * gameObject.transform.localScale.x);
        if (!rayHit.collider)
        {
            return;
        }

        if (rayHit.collider.TryGetComponent(out PortalInteractable inter))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inter.BaseInteract(this.gameObject);
            }
        }
    }
}
