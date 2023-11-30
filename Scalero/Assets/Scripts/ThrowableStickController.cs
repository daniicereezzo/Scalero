using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableStickController : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyStick", 5);
    }
    [SerializeField] const int STICK_DAMAGE = 10;
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.gameObject.CompareTag("Enemy"))
        { return; }

        other.gameObject.GetComponent<EnemyHealthManager>().TakeDamage(STICK_DAMAGE);
    }

    void DestroyStick()
    {
        Destroy(gameObject);
    }
}
