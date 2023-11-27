using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : EnemyBaseState
{
    public EnemyAttacking(EnemyController ec) : base(ec, "EnemyAttacking"){ }
    #region variables
    protected const float ATTACK_RANGE = 1.5f;
    protected const int ATTACK_DAMAGE = 1;
    protected const float FOLLOW_RANGE = 2f;
    protected const float ATTACK_COOLDOWN = 1.5f;
    protected GameObject target;
    bool attacking;


    #endregion

    protected override void OnInstantiated()
    {
        base.OnInstantiated();
        //código aquí
    }

    protected virtual void MoveTowardsTarget()
    {
        if (Mathf.Abs(sm.transform.position.x - target.transform.position.x) < ATTACK_RANGE)
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
        attacking = true;
        while (Mathf.Abs(sm.transform.position.x - target.transform.position.x) < FOLLOW_RANGE)
        {
            if(target.GetComponent<IDamageable>().IsDead())
            {
                sm.ChangeState(sm.enemyWaiting);
                attacking = false;
                Debug.Log("Target is dead");
                yield break;
            }

            Debug.Log("Attacking");
            target.GetComponent<IDamageable>().TakeDamage(ATTACK_DAMAGE);

            yield return new WaitForSeconds(ATTACK_COOLDOWN);
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        MoveTowardsTarget();

        //it should not change constantly between targets because new target is only assigned from waitingState, and this is only entered if there are no targets in range.
        if (CheckIfTargetInRange() == null)
        {
            attacking = false;
            sm.ChangeState(sm.enemyWaiting);
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

}
