using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiterEnemy_AI_Attack : MonoBehaviour
{
    public SphereCollider Collider;
    public AirArmour airArmour;
    public int attackDamage;
    public float attackDelay = 2.5f;
    public delegate void AttackEvent(PlayerController Target);
    public AttackEvent OnAttack;
    protected Coroutine AttackCoroutine;
    public AudioSource AttackSfx;
    public bool hasAttacked;

    // Start is called before the first frame update
    void Start()
    {
        hasAttacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasAttacked)
        {
            StopCoroutine(AttackCoroutine);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovementScript playerMovementScript;
        if (other.TryGetComponent<PlayerMovementScript>(out playerMovementScript))
        {
            AttackCoroutine = StartCoroutine(Attack(playerMovementScript));
        }

    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovementScript playerMovementScript;
        if (other.TryGetComponent<PlayerMovementScript>(out playerMovementScript))
        {
            StopCoroutine(AttackCoroutine);
            AttackCoroutine = null;
        }
    }

    private IEnumerator Attack(PlayerMovementScript playerMovementScript)
    {
        WaitForSeconds Wait = new WaitForSeconds(attackDelay);
        airArmour.RecieveArmourDamage(attackDamage);
        hasAttacked = true;
        Debug.Log("Attack");
        AttackSfx.Play();
        yield return Wait;
    }

}