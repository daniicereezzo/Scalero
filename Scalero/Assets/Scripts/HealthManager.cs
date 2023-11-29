using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] int health;
    int maxHealth = 100;
    [SerializeField] bool isDead = false;
    Collider2D myPlatform = null;

    Animator animator;

    protected virtual void Start()
    {
        RefillHealth();
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int damage)
    {
        if(isDead) { return; }

        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Heal(int heal)
    {
        if(isDead) { return; }

        health += heal;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void KillSelf()
    {
        if(isDead) { return; }

        health = 0;
        Die();
    }

    public void Revive()
    {
        if(!isDead) { return; }

        health = maxHealth;
        isDead = false;
    }
    
    public void RefillHealth()
    {
        if(isDead) { return; }

        health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public Collider2D GetPlatform()
    {
        return myPlatform;
    }

    protected virtual void Die()
    {
        isDead = true;
        animator.SetTrigger("onDie");
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.name);
        if(other.collider.CompareTag("Platform"))
        {
            myPlatform = other.collider;
        }
    }
}
