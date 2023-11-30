using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StakeController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit: " + other.gameObject.name);

        if (other.gameObject.GetComponent<HealthManager>() != null)
        {
            other.gameObject.GetComponent<HealthManager>().TakeDamage(EnemyController.ATTACK_DAMAGE);
        }
    }

}
