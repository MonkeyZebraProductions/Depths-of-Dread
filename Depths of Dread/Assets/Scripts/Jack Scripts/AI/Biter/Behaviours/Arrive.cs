using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour
{
    public EnemyController enemeyController;
    public Transform arriveTarget;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        arriveTarget = enemeyController.target;

        Vector3 desiredVelocity = (arriveTarget.position - enemeyController.position);
        float distance = desiredVelocity.magnitude;

        if (distance > enemeyController.arriveSlowRadius)
        {
            desiredVelocity = desiredVelocity.normalized * enemeyController.maxSpeed;
        }

        if (distance < enemeyController.arriveSlowRadius)
        {
            enemeyController.velocity = new Vector3(0, 0, 0);
            enemeyController.steeringForce = new Vector3(0, 0, 0);
        }

        else
        {
            desiredVelocity = desiredVelocity.normalized * enemeyController.maxSpeed * (distance / enemeyController.arriveSlowRadius);
        }

        enemeyController.steeringForce = desiredVelocity - enemeyController.velocity;

        Debug.DrawLine(enemeyController.position, enemeyController.position + enemeyController.steeringForce, Color.magenta);

        Debug.DrawLine(enemeyController.position, enemeyController.target.position, Color.green);

        Debug.DrawLine(enemeyController.position, enemeyController.position + enemeyController.velocity, Color.red);

    }
}
