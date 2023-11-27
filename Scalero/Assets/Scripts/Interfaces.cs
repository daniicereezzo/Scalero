using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface IDamageable
    {
        void TakeDamage(int damage);
        bool IsDead();
        Collider2D GetPlatform();
    } 
