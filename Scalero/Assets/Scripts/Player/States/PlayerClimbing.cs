using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : PlayerBaseState
{
    public PlayerClimbing(CharacterController cc) : base(cc, "Player Climbing"){ }
    
    #region variables
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    LadderController ladderController;
    float lowestPointY;
    float normalClimbSpeed;
    // const float sprintClimbSpeed = 10;
    // bool isSprinting = false;
    #endregion

    protected override void OnInstantiated()
    {
        base.OnInstantiated();

        playerRigidbody = characterController.GetComponent<Rigidbody2D>();
        playerAnimator = characterController.GetComponent<Animator>();
        ladderController = characterController.GetComponentInChildren<LadderController>();
        normalClimbSpeed = characterController.climbSpeed;
    }
    public override void Enter()
    {
        base.Enter();
        playerAnimator.SetBool("isClimbing", true);
        playerAnimator.SetBool("isWalking", false);
        playerAnimator.SetBool("isFalling", false);
        FreezeRigidbody(true);
        characterController.GetComponent<Collider2D>().isTrigger = true;
        lowestPointY = ladderController.transform.position.y-0.4f;
    }
    public override void Exit()
    {
        base.Exit();
        playerAnimator.SetBool("isClimbing", false);
        FreezeRigidbody(false);
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
            playerAnimator.SetFloat("climbSpeedParameter", 0);
            return;
        }
        characterController.transform.Translate(Vector3.up * ySpeed * Time.deltaTime);
        playerAnimator.SetFloat("climbSpeedParameter", Mathf.Abs(vertical));
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

    void FreezeRigidbody(bool freeze)
    {
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.angularVelocity = 0;
        playerRigidbody.isKinematic = freeze;
    }
}
