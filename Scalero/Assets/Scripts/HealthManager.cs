using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    int health;
    int maxHealth = 100;
    bool isDead = false;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            isDead = true;
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
        isDead = true;
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
}
