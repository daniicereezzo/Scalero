using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaiting : EnemyBaseState
{
    public EnemyWaiting(EnemyController ec) : base(ec, "EnemyWaiting"){}
    
    #region variables
    GameObject possibleTarget;
    #endregion

    protected override void OnInstantiated()
    {
        base.OnInstantiated();
        //código aquí
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        possibleTarget = CheckIfTargetInRange();
        if (possibleTarget != null)
        {
            sm.enemyAttacking.SetTarget(possibleTarget);
            Debug.Log("Target found:" + possibleTarget.name);
            sm.ChangeState(sm.enemyAttacking);
        }
    }
}
