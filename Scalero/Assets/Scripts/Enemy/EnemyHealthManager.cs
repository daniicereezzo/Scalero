using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{
    private Renderer[] enemyRenderers;
    private float fadeSpeed = 0.7f; // Adjust the fade speed as desired

    public GameObject normalFace;
    public GameObject hurtFace;
    public GameObject stepPrefab;
    public GameObject enemyStake;

    protected override void Start()
    {
        base.Start();
        enemyRenderers = GetComponentsInChildren<Renderer>();
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
    }

    public override void TakeDamage(int damage, GameObject damager = null)
    {
        base.TakeDamage(damage);
        if(IsDead())
        {
            EnemyController enemyController = GetComponent<EnemyController>();
            enemyController.enabled = false;
            enemyController.StopAllCoroutines();
            enemyStake.SetActive(false);
            
            GameObject.Instantiate(stepPrefab, transform.position+Vector3.up*0.5f, Quaternion.identity);
        }

        StartCoroutine(ChangeFace());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        // Gradually decrease the alpha value of the enemy's materials
        float currentAlpha = 1f;
        while (currentAlpha > 0)
        {
            foreach (Renderer renderer in enemyRenderers)
            {
                foreach (Material material in renderer.materials)
                {
                    Color currentColor = material.color;
                    float newAlpha = currentColor.a - fadeSpeed * Time.deltaTime;
                    material.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
                }
            }

            currentAlpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator ChangeFace()
    {
        Debug.Log("ChangeFace");
        normalFace.SetActive(false);
        hurtFace.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        normalFace.SetActive(true);
        hurtFace.SetActive(false);
    }
    
    protected override void Die()
    {
        base.Die();
        StartCoroutine(FadeOutAndDestroy());
    }
}


