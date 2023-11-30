using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStanding : PlayerBaseState
{
    public PlayerStanding(CharacterController cc) : base(cc, "Player Standing"){ }

    #region variables
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    const float normalSpeed = 5;
    const float sprintSpeed = 10;
    const float jumpForce = 5;
    const float MIN_ANIMATION_RATE = 0.6f;
    bool isSprinting = false;
    
    #endregion

    protected override void OnInstantiated()
    {
        base.OnInstantiated();

        playerRigidbody = characterController.GetComponent<Rigidbody2D>();
        playerAnimator = characterController.GetComponent<Animator>();
    }

    public override void Move(float horizontal, float vertical)
    {
        float xSpeed = horizontal * (isSprinting ? sprintSpeed : normalSpeed);

        // Should we set the ySpeed to the current ySpeed? Or should we set it to 0?
        playerRigidbody.velocity = new Vector2(xSpeed, playerRigidbody.velocity.y);

        if(horizontal > 0)
        {
            characterController.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(horizontal < 0)
        {
            characterController.transform.localScale = new Vector3(-1, 1, 1);
        }

        if(horizontal == 0)
        {
            playerAnimator.SetBool("walking", false);
        }
        else
        {
            playerAnimator.SetBool("walking", true);
        }

        // We have to implement ShouldStartClimbing() and ShouldStopClimbing()
        if(vertical != 0 /*&& ShouldStartClimbing()*/)
        {
            characterController.ChangeState(characterController.playerClimbing);
        }
    }

    public override void Jump()
    {
        // code here
    }

    public override void Attack()
    {
        if (!characterController.HasLadder())   //implement here secondary attack without ladder
        { return; }

        int steps = characterController.ladderController.GetNumberOfSteps();
        int interpolationParameter = (steps - LadderController.MIN_STEPS) / (LadderController.MAX_STEPS - LadderController.MIN_STEPS);

        playerAnimator.SetFloat("attackSpeedParameter", Mathf.Lerp(1, MIN_ANIMATION_RATE, interpolationParameter));
        playerAnimator.SetTrigger("onAttack");
    }

    public override void ThrowStick()
    {
        // Start the throw stick animation (it should throw the stick at the end of the animation)
    }

    // We should create an Interactable component for every interactable object in the game
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
