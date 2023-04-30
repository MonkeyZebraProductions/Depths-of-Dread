using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBad_AI_AttackRadius : MonoBehaviour
{
    public BigBadEnemy_AI_Controller controller;
    public Collider AttackRadiusCollider;
    public AirArmour airArmour;
    public bool Attacking;
    public float nextAttack;
    public float attackRate;
    public GameObject player;
    public int Damage = 10;
    public AudioSource AttackSfx;

    // Start is called before the first frame update
    void Start()
    {
        Attacking = false;
        airArmour = GameObject.FindGameObjectWithTag("Player").GetComponent<AirArmour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Attacking && Time.time > nextAttack + attackRate)
        {
            player.GetComponent<AirArmour>().RecieveArmourDamage(Damage);
            Debug.Log("Attack Works");
            nextAttack = Time.time;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.gameObject;
            Attacking = true;
            controller.SwitchState(controller.attackState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Attacking = false;
            controller.SwitchState(controller.chaseState);
        }
    }
}
