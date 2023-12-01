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
            characterController.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(horizontal < 0)
        {
            characterController.transform.rotation = Quaternion.Euler(0, 180, 0);
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
        if(vertical != 0 && characterController.GetInteractableTag() == "Ladder")
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
        playerAnimator.SetTrigger("onAttack");  //should implement a return if is already attacking, but not very important
    }

    public override void ThrowStick()
    {
        if (!characterController.HasLadder())
        { return; }
        if (characterController.ladderController.GetNumberOfSteps() <= LadderController.MIN_STEPS)
        { return; }
        // Start the throw stick animation (it should throw the stick at the end of the animation)
        //playerAnimator.SetTrigger("onThrowStick");
        characterController.ladderController.DecreaseSize();
        GameObject stick = GameObject.Instantiate(characterController.throwableStickPrefab, characterController.transform.position+Vector3.up*0.5f, Quaternion.identity);
        stick.GetComponent<Rigidbody2D>().velocity = characterController.transform.right * STICK_THROW_VELOCITY;
        stick.GetComponent<Rigidbody2D>().AddTorque(-Mathf.Sign(characterController.transform.right.x) * 1000f);
    }

    // We should create an Interactable component for every interactable object in the game
    public override void Interact()
    {
        if(characterController.GetInteractableTag() == null)
        {   return; }
        if(characterController.GetInteractableTag() == "Ladder")
        {
            characterController.RetrieveLadder();
            return;
        }
        if(characterController.GetInteractableTag() == "Step")
        {
            characterController.AddStick();
            return;
        }
        Debug.Log("There was a mistake");
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
