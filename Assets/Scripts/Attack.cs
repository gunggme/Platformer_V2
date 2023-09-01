using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    
    [SerializeField] private bool isHit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // See if it can be hit
        Damageable damageable = other.GetComponent<Damageable>();
        

        if (damageable != null)
        {
            Vector2 deliveredKcockback =
                transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            
            // Hit the target
            isHit = damageable.Hit(attackDamage, deliveredKcockback);
        }
    }
}
