using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;

    private Animator animator;
    
    
    [SerializeField] private int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;

    public int Heath
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, _maxHealth);
            
            // If heath drops below 0, character is no longer alive
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    
    [SerializeField]
    private bool _isAlive = false;
    
    [SerializeField]
    private bool isInvincible = false;

    public bool IsHit
    {
        get
        {
            return animator.GetBool(AnimationString.isHit);
        }
        set
        {
            animator.SetBool(AnimationString.isHit, value);
        }
    }

    private float timeSinceHit = 0; 
    public float invicibilityTimer = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            //animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);

            if (value == false)
            {
                damageableDeath.Invoke();
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Heath = MaxHealth;
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invicibilityTimer)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }
    
    public bool Hit(int damages, Vector2 knockback)
    {

        if (IsAlive && !isInvincible)
        {
            Heath -= damages;
            isInvincible = true;
            
            // Notify other subscribed components that the damageable was hit to handle the knockback and such
           // animator.SetTrigger(AnimationStrings.hitTrigger);

            damageableHit?.Invoke(damages, knockback);
            CharacterEvent.characterDamaged?.Invoke(this.gameObject, damages);
            
            return true;
        }
        
        // Unable to  be hit
        return false;
    }


    public bool Heal(int healthRestore)
    {
        if (IsAlive && Heath < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Heath, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);

            Heath += actualHeal;

            CharacterEvent.characterHealed(gameObject, actualHeal);
            return true;
        }

        return false;
    }
}
