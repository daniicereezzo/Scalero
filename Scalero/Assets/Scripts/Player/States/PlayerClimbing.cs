using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : PlayerBaseState
{
    public PlayerClimbing(CharacterController cc) : base(cc, "Player Climbing"){ }
    
    #region variables
    Rigidbody2D playerRigidbody;
    const float normalSpeed = 5;
    const float normalClimbSpeed = 5;
    const float sprintClimbSpeed = 10;
    bool isSprinting = false;
    #endregion

    protected override void OnInstantiated()
    {
        base.OnInstantiated();

        playerRigidbody = characterController.GetComponent<Rigidbody2D>();
    }

    public override void Move(float horizontal, float vertical)
    {
        // This way the moment we release the movement keys, the player will stop moving immediately because the velocity will be 0
        // (maybe the xSpeed should decrease gradually instead of instantly)
        float xSpeed = horizontal * normalSpeed;

        float ySpeed = vertical * (isSprinting ? sprintClimbSpeed : normalClimbSpeed);
        
        playerRigidbody.velocity = new Vector2(xSpeed, ySpeed);

        // We have to implement ShouldStartClimbing() and ShouldStopClimbing()
        if(horizontal != 0 /*&& ShouldStopClimbing()*/)
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
