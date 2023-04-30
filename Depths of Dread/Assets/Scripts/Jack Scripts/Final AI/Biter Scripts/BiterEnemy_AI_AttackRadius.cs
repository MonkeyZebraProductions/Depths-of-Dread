using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiterEnemy_AI_AttackRadius : MonoBehaviour
{
    public BiterEnemy_AI_Controller controller;
    public Collider AttackRadiusCollider;
    public AirArmour airArmour;
    public bool Attacking;
    public float nextAttack;
    public float attackRate = 5;
    public GameObject player;
    public int Damage = 10;
    public AudioSource AttackSfx;
    public float TimerToRetreat = 10f;


    // Start is called before the first frame update
    void Start()
    {
        Attacking = false;
        controller = GetComponentInParent<BiterEnemy_AI_Controller>();
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


        /*
        if(hasAttacked)
        {
            StopCoroutine(AttackCoroutine);
        }
        */

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            player = other.gameObject;
            Attacking = true;
            controller.SwitchState(controller.attackState);
        }

        /*
        PlayerMovementScript playerMovementScript;
        if (other.TryGetComponent<PlayerMovementScript>(out playerMovementScript))
        {
            AttackCoroutine = StartCoroutine(Attack(playerMovementScript));
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Attacking = false;
            controller.SwitchState(controller.chaseState);
        }

        /*
        PlayerMovementScript playerMovementScript;
        if (other.TryGetComponent<PlayerMovementScript>(out playerMovementScript))
        {
            StopCoroutine(AttackCoroutine);
            AttackCoroutine = null;
        }
        */
    }

    /*

    private IEnumerator Attack(PlayerMovementScript playerMovementScript)
    {
        WaitForSeconds Wait = new WaitForSeconds(attackDelay);
        b_A_C.BiterAnimator.Play("Attack");
        airArmour.RecieveArmourDamage(attackDamage);
        hasAttacked = true;
        Debug.Log("Attack");
        AttackSfx.Play();
        yield return Wait;
    }

    */
}
