using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour, IDamageable
{
    [SerializeField] private int numberOfSteps = 3; // Number of steps in the ladder
    [SerializeField] private const int MIN_STEPS = 3; // Minimum number of steps in the ladder
    [SerializeField] private const int MAX_STEPS = 10; // Maximum number of steps in the ladder  
    [SerializeField] private const int LADDER_DAMAGE = 33; 
    private const float LADDER_PIVOT_HEIGHT_DIFFERENCE_UNITS = 200f/512f; // Height in pixels of the pivot of the ladder in the sprite + 10 / pixels per unit
    private GameObject activeLadder;
    private Collider2D myCollider;
    private Rigidbody2D rb;
    [SerializeField] private Transform handBone;
    private bool isDead = false;
    bool ladderFlag = false;
    bool canHurt = false;
    Collider2D myPlatform = null;
    List<GameObject> damagedEnemies;

    private void Start()
    {
        activeLadder = transform.GetChild(numberOfSteps-MIN_STEPS).gameObject;
        rb = GetComponent<Rigidbody2D>();
        damagedEnemies = new List<GameObject>();
        myCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //this will be called by the playerController
        if(Input.GetKeyDown(KeyCode.O))
        {
            IncreaseSize();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            DecreaseSize();
        }
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
        transform.position += Vector3.up * LADDER_PIVOT_HEIGHT_DIFFERENCE_UNITS;    //this is to make the ladder fall to the floor adjusting height to the minimum required
        transform.rotation = Quaternion.Euler(0, 0, 90);
        myCollider.enabled = true;
        rb.isKinematic = false;
    }
    void SetLadderLogic()   //this is called when ladder touches floor and its the one actually setting things up
    {
        myCollider.enabled = false;
        ladderFlag = false;
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
    public void EnableDamage()
    {
        canHurt = true;
        damagedEnemies.Clear();
    }
    public void DisableDamage()
    {
        canHurt = false;
    }

        

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Platform"))
        {
            myPlatform = other.collider;
        }
        else
        { return; }

        if(!ladderFlag)
        {   return;}

        SetLadderLogic();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!canHurt)
        {   return;}

        if(!other.CompareTag("Enemy"))
        {  return;}

        if(damagedEnemies.Contains(other.gameObject))
        {   return;}
        
        other.GetComponent<IDamageable>().TakeDamage(LADDER_DAMAGE);
        damagedEnemies.Add(other.gameObject);
    }


}
