using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : StateMachine
{
    #region states
    public PlayerClimbing playerClimbing;
    public PlayerStanding playerStanding;
    
    #endregion
    
    #region variables
    HealthManager healthManager;
    Rigidbody2D rb;
    public LadderController ladderController;
    public GameObject throwableStickPrefab;
    bool hasLadder = true;

    public float speed = 5;
    public float jumpForce = 5;
    public float climbSpeed = 5;
    #endregion

    protected virtual void Awake()
    {
        playerStanding = new PlayerStanding(this);
        playerClimbing = new PlayerClimbing(this);

        healthManager = GetComponent<HealthManager>();
        rb = GetComponent<Rigidbody2D>();
        ladderController = GetComponentInChildren<LadderController>();
    }

    protected override BaseState GetInitialState()
    {
        return playerStanding;
    }

    // We use new because we are hiding the method of the base class to return a more specific type
    public new PlayerBaseState GetCurrentState()
    {
        return (PlayerBaseState)currentState;
    }
    public void EnableLadderDamage()
    {
        ladderController.EnableDamage();
    }
    public void DisableLadderDamage()
    {
        ladderController.DisableDamage();
    }

    public bool HasLadder()
    {
        return hasLadder;
    }
    public void SetLadder()
    {
        ladderController.SetLadder();
        hasLadder = false;
    }
    public void RetrieveLadder()
    {
        if(hasLadder)
        { return; }

        ladderController.SetWeapon();
        hasLadder = true;
    }
}
