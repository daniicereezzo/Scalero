using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : BaseState
{
    protected EnemyController sm;
    public EnemyBaseState(EnemyController ec, string stateName) : base(stateName)
    {
        sm = ec;
        OnInstantiated();   //This calls the method OnInstantiated() of the child class
    }

    #region variables
    protected GameObject player;
    protected GameObject ladder;
    protected Rigidbody2D enemyRb;      //Maybe remove this, cause its not used
    protected const float HEIGHT_ATTACK_RANGE = 2f; //Over this height difference, the enemy will stop tracking the player
    #endregion

    protected override void OnInstantiated()
    {
        base.OnInstantiated();
        player = GameObject.FindGameObjectWithTag("Player");
        ladder = GameObject.FindGameObjectWithTag("Ladder");
        enemyRb = sm.GetComponent<Rigidbody2D>();
    }

    protected virtual GameObject CheckIfTargetInRange()
    {
        if(player.GetComponent<HealthManager>() != null && player.GetComponent<HealthManager>().IsDead())
        {
            return null;
        }
        //Debug.Log("Player in range: " + player.transform.position.y < sm.transform.position.y + HEIGHT_ATTACK_RANGE);
        if(Mathf.Abs(player.transform.position.y - sm.transform.position.y) < HEIGHT_ATTACK_RANGE)
        {
            if(IsItSafeFromPitfall(player.GetComponent<HealthManager>()))
            {
                return player;
            }
        }
        
        if(ladder == null)  //remove this one once the ladder is implemented
        {
            //Debug.Log("Ladder not found");
            return null;
        }
        if(ladder.GetComponent<LadderController>().IsDead())
        {
            return null;
        }
            
        if(ladder.transform.position.y < sm.transform.position.y + HEIGHT_ATTACK_RANGE) //be wary that the ladder has a custom pivot, check it is placed on floor
        {
            if(IsItSafeFromPitfall(ladder.GetComponent<LadderController>()))
            {
                return ladder;
            }
        }
        
        return null;
    }
    bool IsItSafeFromPitfall(IDamageable target)
    {
        Collider2D myPlatform = sm.GetComponent<HealthManager>().GetPlatform();
        
        if(myPlatform == null)
        { return true; }

        if(myPlatform == target.GetPlatform())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
