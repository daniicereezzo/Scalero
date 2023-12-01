using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;
    //camera position limits
    const float minX = -4.6f;
    const float maxX = 4.45f;
    const float minY = -1.54f;
    const float maxY = 2.95f;
    const float smoothSpeed = 1f;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(Mathf.Clamp(playerTransform.position.x, minX, maxX), Mathf.Clamp(playerTransform.position.y, minY, maxY), transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
