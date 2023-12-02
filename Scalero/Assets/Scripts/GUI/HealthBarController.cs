using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarController : MonoBehaviour
{
    
    private PlayerHealthManager playerHealthManager;
    private Vector3 initialPosition;
    private Vector3 initialScale;
    private float currentHealth;
    private const float size_x = 496f;

    void Awake(){
        playerHealthManager = GameObject.FindObjectOfType<PlayerHealthManager>();
    }

    void Start(){
        initialPosition = transform.localPosition;
        initialScale = transform.localScale;
        currentHealth = playerHealthManager.GetHealth();
    }

    void Update(){
        UpdateHealthBar();
        if(currentHealth != playerHealthManager.GetHealth()){
            currentHealth = playerHealthManager.GetHealth();
        }
    }

    public void UpdateHealthBar(){
        float healthPercentage = (float)playerHealthManager.GetHealth() / (float)playerHealthManager.GetMaxHealth();
        transform.localScale = new Vector3(healthPercentage * initialScale.x, initialScale.y, initialScale.z);
        transform.localPosition = initialPosition + Vector3.right * (healthPercentage/2 - 0.5f) * size_x;
    }
}
