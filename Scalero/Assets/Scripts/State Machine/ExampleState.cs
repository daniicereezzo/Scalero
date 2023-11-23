using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleState : BaseState
{
    private ExampleStateMachine sm;
    public ExampleState(ExampleStateMachine stateMachine) : base("ExampleState")
    {
        sm = stateMachine;
        OnInstantiated();
    }
    #region variables
    #endregion

    protected override void OnInstantiated()
    {
        base.OnInstantiated();
        //código aquí
    }

    //overridear aqui los metodos de BaseState
}