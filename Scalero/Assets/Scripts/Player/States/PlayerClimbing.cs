using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : PlayerBaseState
{
    public PlayerClimbing(CharacterController cc) : base(cc, "Player Climbing"){ }
    
    #region variables
    Rigidbody2D playerRigidbody;
    LadderController ladderController;
    float lowestPointY;
    const float normalClimbSpeed = 5;
    // const float sprintClimbSpeed = 10;
    // bool isSprinting = false;
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
        playerRigidbody.isKinematic = true;
        characterController.GetComponent<Collider2D>().isTrigger = true;
        lowestPointY = characterController.transform.position.y;
    }
    public override void Exit()
    {
        base.Exit();
        playerRigidbody.isKinematic = false;
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
        if(horizontal != 0)
        {
            characterController.ChangeState(characterController.playerStanding);
        }

        // float ySpeed = vertical * (isSprinting ? sprintClimbSpeed : normalClimbSpeed);
        float ySpeed = vertical * normalClimbSpeed;
        
        if(ySpeed < 0 && characterController.transform.position.y <= lowestPointY)
        {
            characterController.transform.position = new Vector3(characterController.transform.position.x, lowestPointY, characterController.transform.position.z);
            return;
        }
        characterController.transform.Translate(Vector3.up * ySpeed * Time.deltaTime);

    }

    // public override void Jump()
    // {
    //     // code here
    // }

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

    // public override void StartSprinting()
    // {
    //     isSprinting = true;
    // }

    // public override void StopSprinting()
    // {
    //     isSprinting = false;
    // }
}
