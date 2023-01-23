using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    public EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offsetToTarget = (enemyController.target.position - enemyController.position);
        float distance = offsetToTarget.magnitude;
        enemyController.steeringForce = offsetToTarget.normalized * enemyController.maxSteerForce;

        if (distance < enemyController.arriveSlowRadius)
        {
            enemyController.velocity = new Vector3(0, 0, 0);
        }
    }
}
