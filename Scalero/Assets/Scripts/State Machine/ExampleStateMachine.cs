using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleStateMachine : StateMachine
{
    #region states
    public ExampleState exampleState;
    //listar de este modo todos los estados
    
    #endregion
    
    #region variables
    #endregion

    protected virtual void Awake()
    {
        exampleState = new ExampleState(this);
    }

    protected override BaseState GetInitialState()
    {   return exampleState;}
    //importante cambiar esto por el estado inicial de la SM
}