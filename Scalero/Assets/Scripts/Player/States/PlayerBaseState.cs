using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : BaseState
{
    protected CharacterController characterController;
    public PlayerBaseState(CharacterController cc, string stateName) : base(stateName)
    {
        characterController = cc;
        OnInstantiated();   //This calls the method OnInstantiated() of the child class
    }

    #region variables
    protected const float STICK_THROW_VELOCITY = 10;
    #endregion

    //overridear aqui los metodos de BaseState

    public abstract void Move(float horizontal, float vertical);
    public abstract void Jump();
    public abstract void Attack();
    public abstract void ThrowStick();
    public abstract void Interact();
    public abstract void StartSprinting();
    public abstract void StopSprinting();
}
