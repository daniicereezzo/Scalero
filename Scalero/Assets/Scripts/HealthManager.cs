using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] protected int health;
    int maxHealth = 100;
    [SerializeField] bool isDead = false;
    bool canBeHit = true;
    Collider2D myPlatform = null;
    public GameObject leafParticlePrefab;

    Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int damage, GameObject damager = null)
    {
        if(isDead) { return; }
        if(!canBeHit) { return; }
        canBeHit = false;

        GameObject.Instantiate(leafParticlePrefab, transform.position+Vector3.up*0.3f, Quaternion.identity);
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        else
        {
            //animator.SetTrigger("onHit");
            Invoke("CanBeHitAgain", 0.5f);
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
    void CanBeHitAgain()
    {
        canBeHit = true;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.name);
        if(other.collider.CompareTag("Platform"))
        {
            myPlatform = other.collider;
        }
    }
    protected abstract IEnumerator ChangeFace();
}
