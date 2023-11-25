using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    [SerializeField] private int numberOfSteps = 3; // Number of steps in the ladder
    [SerializeField] private const int MIN_STEPS = 3; // Minimum number of steps in the ladder
    [SerializeField] private const int MAX_STEPS = 5; // Maximum number of steps in the ladder  
    private GameObject activeLadder;
    [SerializeField] private Transform handBone;

    private void Start()
    {
        activeLadder = transform.GetChild(numberOfSteps-MIN_STEPS).gameObject;
    }

    private void Update()
    {
        //this will be called by the characterController
        if(Input.GetKeyDown(KeyCode.O))
        {
            IncreaseSize();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            DecreaseSize();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            SetFloor();
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            SetWeapon();
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
    }

    public void SetWeapon()
    {
        activeLadder.GetComponent<Collider2D>().isTrigger = true;
        transform.position = handBone.position;
        transform.SetParent(handBone);
    }


}
