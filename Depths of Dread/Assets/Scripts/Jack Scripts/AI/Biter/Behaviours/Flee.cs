using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    public EnemyController enemyController;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = enemyController.target;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 offsetToTarget = (enemyController.position - target.position);
        enemyController.steeringForce = offsetToTarget.normalized * enemyController.maxSteerForce;
    }
}
