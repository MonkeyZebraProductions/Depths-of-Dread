using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public EnemyController enemeyController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsHeadingForCollision())
        {
            Vector3 collisionAvoidDir = ObstacleRays();
            Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * enemeyController.avoidCollisionWeight;
            enemeyController.steeringForce += collisionAvoidForce;
        }

        enemeyController.steeringForce = Vector3.ClampMagnitude(enemeyController.steeringForce, enemeyController.maxSteerForce);
        enemeyController.velocity = Vector3.ClampMagnitude(enemeyController.velocity, enemeyController.maxSpeed);
        enemeyController.velocity += enemeyController.steeringForce * Time.deltaTime;

        enemeyController.position += enemeyController.velocity * Time.deltaTime;
        enemeyController.forward = enemeyController.velocity.normalized;
        transform.position = enemeyController.position;
        transform.forward = enemeyController.forward;

        Debug.DrawLine(enemeyController.position, enemeyController.position + enemeyController.velocity, Color.red);
        Debug.DrawLine(enemeyController.position, enemeyController.position + enemeyController.steeringForce, Color.green);
    }

    bool IsHeadingForCollision()
    {
        RaycastHit hit;
        if (Physics.SphereCast(enemeyController.position, enemeyController.boundsRadius, enemeyController.forward, out hit, enemeyController.collisionAvoidDst, enemeyController.obstacleMask))
        {
            return true;
        }
        return false;
    }

    Vector3 ObstacleRays()
    {
        Vector3[] rayDirections = ObstacleAvoidanceManager.directions;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Vector3 dir = transform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(enemeyController.position, dir);
            if (!Physics.SphereCast(ray, enemeyController.boundsRadius, enemeyController.collisionAvoidDst, enemeyController.obstacleMask))
            {
                return dir;
            }
        }

        return enemeyController.forward;
    }

    public Vector3 SteerTowards(Vector3 vector)
    {
        Vector3 v = vector.normalized * enemeyController.maxSpeed - enemeyController.velocity;
        return Vector3.ClampMagnitude(v, enemeyController.maxSteerForce);
    }


}
