using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : PlayerBaseState
{
    public PlayerClimbing(CharacterController cc) : base(cc, "Player Climbing"){ }
    
    #region variables
    Rigidbody2D playerRigidbody;
    LadderController ladderController;
    const float normalSpeed = 5;
    const float normalClimbSpeed = 5;
    const float sprintClimbSpeed = 10;
    bool isSprinting = false;
    #endregion

    protected override void OnInstantiated()
    {
        base.OnInstantiated();

        playerRigidbody = characterController.GetComponent<Rigidbody2D>();
        ladderController = characterController.GetComponentInChildren<LadderController>();
    }
    public override void Enter()
    {
        base.Enter();
        playerRigidbody.gravityScale = 0;
        characterController.GetComponent<Collider2D>().isTrigger = true;
    }
    public override void Exit()
    {
        base.Exit();
        playerRigidbody.gravityScale = 1;
        characterController.GetComponent<Collider2D>().isTrigger = false;
        playerRigidbody.velocity = Vector2.zero;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(ladderController.IsDead())   //this should be an event but i have no time
        {
            characterController.ChangeState(characterController.playerStanding);
        }
    }

    public override void Move(float horizontal, float vertical)
    {
        // This way the moment we release the movement keys, the player will stop moving immediately because the velocity will be 0
        // (maybe the xSpeed should decrease gradually instead of instantly)
        float ySpeed = vertical * (isSprinting ? sprintClimbSpeed : normalClimbSpeed);
        
        playerRigidbody.velocity = Vector2.up * ySpeed;

        if(horizontal != 0)
        {
            characterController.ChangeState(characterController.playerStanding);
        }
    }

    public override void Jump()
    {
        // code here
    }

    public override void Attack()
    {
        // Start the attack animation
    }

    public override void ThrowStick()
    {
        // Start the throw stick animation (it should throw the stick at the end of the animation)
    }

    public override void Interact()
    {
        // code here
    }

    public override void StartSprinting()
    {
        isSprinting = true;
    }

    public override void StopSprinting()
    {
        isSprinting = false;
    }
}
