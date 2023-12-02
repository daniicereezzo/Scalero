using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface IDamageable
    {
        void TakeDamage(int damage, GameObject damager = null);
        bool IsDead();
        Collider2D GetPlatform();
    } 
