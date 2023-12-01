using System;
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
    [NonSerialized] public LadderController ladderController;
    public GameObject throwableStickPrefab;
    bool hasLadder = true;
    [SerializeField] GameObject interactable = null;

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
        interactable = ladderController.GetActiveLadder();
    }
    public void RetrieveLadder()
    {
        if(hasLadder)
        { return; }

        ladderController.SetWeapon(transform.right.x);
        hasLadder = true;
        interactable = null;
    }
    public void AddStick()
    {
        if(GetInteractableTag() != "Step")
        { return; }

        ladderController.IncreaseSize();
        Destroy(interactable);
        interactable = null;
    }
    public string GetInteractableTag()
    {
        if(interactable == null)
        { return null; }

        return interactable.tag;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if((other.CompareTag("Ladder") && other.transform.parent.GetComponent<LadderController>().isPlanted()) || other.CompareTag("Step"))
        { interactable = other.gameObject; }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(interactable == null)
        { return; }

        if (other.gameObject == interactable)
        {
            interactable = null;
            if(GetCurrentState() == playerClimbing && other.CompareTag("Ladder"))
            {
                ChangeState(playerStanding);
            }
        }
    }
}
