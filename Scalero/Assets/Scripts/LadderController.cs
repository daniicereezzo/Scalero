using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour, IDamageable
{
    [SerializeField] private int numberOfSteps = 3; // Number of steps in the ladder
    [SerializeField] private const int MIN_STEPS = 3; // Minimum number of steps in the ladder
    [SerializeField] private const int MAX_STEPS = 10; // Maximum number of steps in the ladder  
    private GameObject activeLadder;
    private Rigidbody2D rb;
    [SerializeField] private Transform handBone;
    private bool isDead = false;
    bool ladderFlag = false;
    Collider2D myPlatform = null;

    private void Start()
    {
        activeLadder = transform.GetChild(numberOfSteps-MIN_STEPS).gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //this will be called by the characterController
        // if(Input.GetKeyDown(KeyCode.O))
        // {
        //     IncreaseSize();
        // }
        // if(Input.GetKeyDown(KeyCode.P))
        // {
        //     DecreaseSize();
        // }
        // if(Input.GetKeyDown(KeyCode.I))
        // {
        //     SetFloor();
        // }
        if(Input.GetKeyDown(KeyCode.U))
        {
            SetLadder();
        }   
    }

    void IncreaseSize()
    {
        // if(transform.parent == null)
        // {   return;}

        if(numberOfSteps >= MAX_STEPS)
        {   return;}

        activeLadder.SetActive(false);
        numberOfSteps++;
        activeLadder = transform.GetChild(numberOfSteps-MIN_STEPS).gameObject;
        activeLadder.SetActive(true);
    }

    void DecreaseSize()
    {
        // if(transform.parent == null)
        // {   return;}

        if(numberOfSteps <= MIN_STEPS)
        {   return;}

        activeLadder.SetActive(false);
        numberOfSteps--;
        activeLadder = transform.GetChild(numberOfSteps-MIN_STEPS).gameObject;
        activeLadder.SetActive(true);
    }

    public int GetNumberOfSteps()
    {   return numberOfSteps;}

    public bool IsPlanted()
    {
        if (transform.parent == null)
        {   return true;}
        else
        {   return false;}
    }

    public void SetFloor()
    {
        activeLadder.GetComponent<Collider2D>().isTrigger = false;
        transform.SetParent(null);
        rb.isKinematic = true;
    }

    public void SetLadder() //this is the one you call from outside and it makes the ladder start falling to the floor
    {
        ladderFlag = true;
        transform.SetParent(null);
        activeLadder.GetComponent<Collider2D>().isTrigger = false;
        rb.isKinematic = false;
    }
    void SetLadderLogic()   //this is called when ladder touches floor and its the one actually setting things up
    {
        ladderFlag = false;
        activeLadder.GetComponent<Collider2D>().isTrigger = true;
        rb.isKinematic = true;
        isDead = false;
    }

    public void SetWeapon()
    {
        activeLadder.GetComponent<Collider2D>().isTrigger = true;
        transform.position = handBone.position;
        transform.SetParent(handBone);
        rb.isKinematic = true;
    }

    public bool IsDead()
    {
        // if(Mathf.Approximately(transform.rotation.eulerAngles.z, 0))
        // {   return false;}
        // else
        // {   return true;}
        return isDead;
    }

    public void TakeDamage(int damage)
    {
        isDead = true;
        rb.isKinematic = false;
        activeLadder.GetComponent<Collider2D>().isTrigger = false;
        rb.AddForce(new Vector2(-damage, damage)*100);
        rb.AddTorque(damage*100);
    }

    public Collider2D GetPlatform()
    {
        return myPlatform;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Platform"))
        {
            myPlatform = other.collider;
        }
        
        if(!ladderFlag)
        {   return;}

        SetLadderLogic();
    }


}
