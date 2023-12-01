using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(-0.6f, -3f, 0);
        GameObject.FindGameObjectWithTag("Ladder").transform.root.gameObject.GetComponent<LadderController>().SetWeapon(player.transform.right.x);
    }
    
}
