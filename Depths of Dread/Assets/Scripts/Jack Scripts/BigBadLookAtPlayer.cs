using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigBadLookAtPlayer : MonoBehaviour
{
    public Vector3 Target;
    public Transform Model;
    public NavMeshAgent agent;
    public float MoveUpRate = 1f;
    public float MinOffset = 4f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
      
    }

    // Update is called once per frame
    void Update()
    {
        Target = GetComponent<BigBadEnemy_AI_Controller>().CurrentDestination;
        //Model.transform.LookAt(Target);

        Quaternion lookOnLook = Quaternion.LookRotation(Target - Model.transform.position);

        Model.transform.rotation = Quaternion.Slerp(Model.transform.rotation, lookOnLook, Time.deltaTime);

        float angle = Model.transform.localEulerAngles.x;

        if (angle >= 1f && angle <= 90f && agent.baseOffset > MinOffset)
        {
            agent.baseOffset -= MoveUpRate * Time.deltaTime;
        }

        if (angle >= 270f && angle <= 360f)
        {
            agent.baseOffset += MoveUpRate * Time.deltaTime;
        }
    }

}
