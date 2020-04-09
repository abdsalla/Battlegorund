using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraControls : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform target;  // Player's position
    [SerializeField] Camera theCamera;  // Player's camera
    [SerializeField] float targetRotateSpeed;

    private GameManager instance;


    void Start()
    {
        instance = GameManager.Instance;
        theCamera = GetComponent<Camera>();
       // target = instance.currentPlayer.transform;
    }

    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        transform.SetParent(target);
        Vector3 targetPosition = target.transform.position + offset; // we don't want the camera to be inside of the Player
        transform.position = target.TransformPoint(offset);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, 90.0f);
        transform.LookAt(target);
    }
}