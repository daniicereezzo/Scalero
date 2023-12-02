using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    public GameObject normalFace;
    public GameObject hurtFace;
    public GameObject lowHealthFace;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        normalFace.SetActive(true);
        hurtFace.SetActive(false);
        lowHealthFace.SetActive(false);
    }

    public override void TakeDamage(int damage, GameObject damager = null)
    {
        base.TakeDamage(damage);
        if(IsDead())
        {
            GetComponent<CharacterController>().enabled = false;
            GetComponent<CharacterController>().StopAllCoroutines();
        }

        StartCoroutine(ChangeFace());
    }

    protected override IEnumerator ChangeFace()
    {
        normalFace.SetActive(false);
        lowHealthFace.SetActive(false);
        hurtFace.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hurtFace.SetActive(false);
        if(GetHealth() <= 20)
        {
            lowHealthFace.SetActive(true);
        }
        else
        {
            normalFace.SetActive(true);
        }
    }
    public void SetHealth(int health)
    {
        this.health = health;
    }
}
