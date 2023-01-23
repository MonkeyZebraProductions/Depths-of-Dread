using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 position;
    public Vector3 forward;
    public Vector3 velocity;

    public Vector3 steeringForce;

    public Transform target;

    public float maxSpeed = 5f;
    public float maxSteerForce = 5f;
    public float arriveSlowRadius = 5f;
    public float pathArriveDistance = 2f;

    public float targetWeight = 1;

    public LayerMask obstacleMask;
    public float boundsRadius = .27f;
    public float avoidCollisionWeight = 10;
    public float collisionAvoidDst = 5;

    void Awake()
    {
        position = transform.position;
    }

  
    // Update is called once per frame
    void Update()
    {
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxSteerForce);
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        velocity += steeringForce * Time.deltaTime;

        position += velocity * Time.deltaTime;
        forward = velocity.normalized;
        transform.position = position;
        transform.forward = forward;

        Debug.DrawLine(position, position + velocity, Color.red);
        Debug.DrawLine(position, position + steeringForce, Color.green);
    }
}
