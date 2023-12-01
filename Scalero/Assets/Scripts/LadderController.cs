using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour, IDamageable
{
    [SerializeField] private int numberOfSteps = 3; // Number of steps in the ladder
    public const int MIN_STEPS = 3; // Minimum number of steps in the ladder
    public const int MAX_STEPS = 10; // Maximum number of steps in the ladder  
    [SerializeField] private const int LADDER_DAMAGE = 30;
    [SerializeField] private const int DAMAGE_BONUS_PER_STEP = 5; 
    private const float LADDER_DROP_HEIGHT = 0.2f; // Enough Height so it starts falling from above the floor 
    private GameObject activeLadder;
    BoxCollider2D climbCollider;
    private Rigidbody2D rb;
    [SerializeField] private Transform handBone;
    private bool isDead = true;
    bool ladderFlag = false;
    bool canHurt = false;
    float DEFAULT_ROTATION_Z;
    BoxCollider2D myPlatform = null;
    List<GameObject> damagedEnemies;

    private void Start()
    {
        activeLadder = transform.GetChild(numberOfSteps-MIN_STEPS).gameObject;
        rb = GetComponent<Rigidbody2D>();
        damagedEnemies = new List<GameObject>();
        climbCollider = GetComponent<BoxCollider2D>();
        DEFAULT_ROTATION_Z = transform.rotation.z;
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
        // if(Input.GetKeyDown(KeyCode.U))
        // {
        //     SetLadder();
        // }   
    }

    public void IncreaseSize()
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

    public void DecreaseSize()
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

    public void SetFloor()  //probably not gonna implement this one
    {
        // activeLadder.GetComponent<BoxCollider2D>().isTrigger = false;
        // transform.SetParent(null);
        // rb.isKinematic = true;
    }

    public void SetLadder() //this is the one you call from outside and it makes the ladder start falling to the floor
    {
        ladderFlag = true;
        transform.SetParent(null);
        transform.rotation = Quaternion.Euler(0, 0, 90);
        transform.localScale = new Vector3(1, 1, 1);
        transform.position += Vector3.up * LADDER_DROP_HEIGHT;    //this is to make the ladder fall to the floor adjusting height to the minimum required
        climbCollider.enabled = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        rb.isKinematic = false;
    }
    void SetLadderLogic()   //this is called when ladder touches floor and its the one actually setting things up
    {
        climbCollider.enabled = false;
        ladderFlag = false;
        rb.isKinematic = true;
        isDead = false;
    }

    public void SetWeapon(float playerRotationY)
    {
        isDead = true;
        climbCollider.enabled = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.SetParent(handBone);
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, DEFAULT_ROTATION_Z);
        transform.localScale = new Vector3(Mathf.Sign(playerRotationY), 1, 1);
        rb.isKinematic = true;
    }

    public bool isPlanted()
    {
        return transform.parent == null;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(int damage, GameObject damager)
    {
        isDead = true;
        rb.isKinematic = false;
        // SetMyCollider();
        climbCollider.enabled = true;
        rb.constraints = RigidbodyConstraints2D.None;
        float directionSign = Mathf.Sign(transform.position.x - damager.transform.position.x);
        rb.AddForce(new Vector2(directionSign * damage, damage)*100f, ForceMode2D.Impulse);
        rb.AddTorque(-directionSign * damage*1200f);
    }

    public Collider2D GetPlatform()
    {
        return myPlatform;
    }
    public GameObject GetActiveLadder()
    {
        return activeLadder;
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
            myPlatform = (BoxCollider2D)other.collider;
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
        
        other.GetComponent<IDamageable>().TakeDamage(LADDER_DAMAGE+(numberOfSteps-MIN_STEPS)*DAMAGE_BONUS_PER_STEP);
        damagedEnemies.Add(other.gameObject);
    }


}
