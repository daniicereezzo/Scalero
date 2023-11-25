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
}
