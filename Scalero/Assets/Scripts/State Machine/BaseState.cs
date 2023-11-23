using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public string name;
    public BaseState(string name)   //hay que añadir en el constructor particular que pida una instancia de sm del tipo de SM de la que el estado particular sea estado
    {
        this.name = name;
    }

    protected virtual void OnInstantiated() { }     //esto se llama en el constructor de cada estado particular
    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}
