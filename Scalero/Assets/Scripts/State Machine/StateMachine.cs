using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected BaseState currentState;
    public string currentStateName; //esto deberia ser una propiedad con set, public get

    protected virtual void Start() //esto ocurre despues del awake del hijo, por lo que GetInitialState ya devolverá el estado predeterminado de esa SM y no null (aunque puede que dé igual porque imagino que el override sucede en compile time pero que me diga dani)
    {  
        ManualStart();
    }

    public virtual void ManualStart()
    {
        currentState = GetInitialState();
        currentStateName = currentState.name;
        currentState.Enter();
    }

    protected virtual void Update()
    {
        currentState.UpdateLogic();
    }

    protected virtual void LateUpdate() //usamos esto en vez de fixed update porque priorizamos que la SM no se pierda en una transicion por ir asincrona al update.
    {
        currentState.UpdatePhysics();
    }

    public virtual void ChangeState(BaseState newState)
    {
        StopAllCoroutines();
        currentState.Exit();
        currentState = newState;
        currentStateName = newState.name;
        currentState.Enter();
    }

    protected abstract BaseState GetInitialState(); //esto será overrideado por el estado que será el default en cada máquina particular.

    public BaseState GetCurrentState()
    {
        return currentState;
    }
}
