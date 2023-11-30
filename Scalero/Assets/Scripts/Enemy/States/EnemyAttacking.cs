using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : EnemyBaseState
{
    public EnemyAttacking(EnemyController ec) : base(ec, "EnemyAttacking"){ }
    #region variables
    protected GameObject target;
    protected StakeController stakeController;
    bool attacking;


    #endregion
    public override void Enter()
    {
        base.Enter();
        sm.animator.SetBool("following", true);
    }

    protected override void OnInstantiated()
    {
        base.OnInstantiated();
        stakeController = sm.GetComponentInChildren<StakeController>();
        //código aquí
    }

    protected virtual void MoveTowardsTarget()
    {
        if (Mathf.Abs(sm.transform.position.x - target.transform.position.x) < EnemyController.ATTACK_RANGE)
        {
            if (attacking)
            {
                return;
            }
            sm.StartCoroutine(Attack(target));
            return;
        }

        if (target.transform.position.x < sm.transform.position.x)
        {
            sm.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            sm.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        sm.transform.Translate(Vector2.right * sm.speed * Time.deltaTime);

        //i need to implement a way to stop if theres a pitfall in front of the enemy
    }

    protected virtual IEnumerator Attack(GameObject target)
    {
        SetAttacking(true);
        while (Mathf.Abs(sm.transform.position.x - target.transform.position.x) < EnemyController.FOLLOW_RANGE)
        {
            if(target.GetComponent<IDamageable>().IsDead())
            {
                Debug.Log("Target is dead");
                SetAttacking(false);
                sm.ChangeState(sm.enemyWaiting);
                
                yield break;    //should be unreachable but just in case
            }

            yield return null;
        }
        SetAttacking(false);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        MoveTowardsTarget();

        //it should not change constantly between targets because new target is only assigned from waitingState, and this is only entered if there are no targets in range.
        if (CheckIfTargetInRange() != target)
        {
            attacking = false;
            sm.ChangeState(sm.enemyWaiting);
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
    void SetAttacking(bool isAttacking)
    {
        this.attacking = isAttacking;
        sm.animator.SetBool("attacking", isAttacking);
        stakeController.enabled = isAttacking;
    }

}
