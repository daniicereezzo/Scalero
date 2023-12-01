using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StakeController : MonoBehaviour
{
    GameObject targetObject;
    private void Start()
    {
        //I need a Start to disable the script from inspector
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(this.enabled == false)
        { return; }

        if(other.gameObject.CompareTag("Player"))
        {
            targetObject = other.gameObject;
        }
        else if(other.gameObject.CompareTag("Ladder"))
        {
            targetObject = other.transform.parent.gameObject;
        }
        else
        { return; }

        if (targetObject.GetComponent<IDamageable>().IsDead())
        { return; }

        Debug.Log("hit: " + targetObject.name);

        targetObject.GetComponent<IDamageable>().TakeDamage(EnemyController.ATTACK_DAMAGE, gameObject);
    }

}
