using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private CharacterController playerController;
    private HealthManager healthManager;

    void Awake()
    {
        playerController = GetComponent<CharacterController>();
        healthManager = GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthManager.IsDead()) { return; }

        playerController.GetCurrentState().Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // We have to decide if we want the player to be able to jump
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     playerController.GetCurrentState().Jump();
        // }


        if(Input.GetKeyDown(KeyCode.E))
        {
            playerController.GetCurrentState().Interact();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            playerController.GetCurrentState().Attack();
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            playerController.GetCurrentState().ThrowStick();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            if(!playerController.HasLadder())
            { return; }

            playerController.SetLadder();
        }
        // if(Input.GetKeyDown(KeyCode.LeftShift))
        // {
        //     playerController.GetCurrentState().StartSprinting();
        // }

        // if(Input.GetKeyUp(KeyCode.LeftShift))
        // {
        //     playerController.GetCurrentState().StopSprinting();
        // }
    }
}
