using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : StateMachine
{
    #region states
    public EnemyWaiting enemyWaiting; //Maybe its easier if the enemy always knows where the player is
    public EnemyAttacking enemyAttacking;
    public Animator animator;
    public const float ATTACK_RANGE = 1f;
    public const int ATTACK_DAMAGE = 1;
    public const float FOLLOW_RANGE = 2f;

    #endregion

    #region variables

    HealthManager healthManager;
    Rigidbody2D rb;

    public float speed = 5;
    #endregion

    protected virtual void Awake()
    {
        enemyWaiting = new EnemyWaiting(this);
        enemyAttacking = new EnemyAttacking(this);

        healthManager = GetComponent<HealthManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override BaseState GetInitialState()
    {
        return enemyWaiting;
    }
    public new EnemyBaseState GetCurrentState()
    {
        return (EnemyBaseState)currentState;
    }
}

