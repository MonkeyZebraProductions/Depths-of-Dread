using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LookAtPlayer : MonoBehaviour
{
    public Transform Target,Model;
    public NavMeshAgent agent;
    public float MoveUpRate=1f;
    public float MinOffset = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Target = GetComponent<Biter_AI>().CurrentDestination;
        //Model.transform.LookAt(Target);

        Quaternion lookOnLook = Quaternion.LookRotation(Target.transform.position - Model.transform.position);
        Model.transform.rotation =
        Quaternion.Slerp(Model.transform.rotation, lookOnLook, Time.deltaTime);

        float angle = Model.transform.localEulerAngles.x;

        if(angle >=1f && angle <= 90f && agent.baseOffset> MinOffset)
        {
            agent.baseOffset -= MoveUpRate*Time.deltaTime;
        }

        if (angle >= 270f && angle <= 360f)
        {
            agent.baseOffset += MoveUpRate * Time.deltaTime;
        }
    }
}
