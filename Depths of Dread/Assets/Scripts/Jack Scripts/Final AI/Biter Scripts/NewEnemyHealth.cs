using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public float MaxHealth;
    public bool IsBiter, IsBanshee, IsPersuer, IsBigBad;
    private ParticleSystem BloodSplatter;
    private SpawnPickup Drops;

    private BiterEnemy_AI_Controller biterAI;
    private Banshee_AI_Controller bansheeAI;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        BloodSplatter = GetComponentInChildren<ParticleSystem>();
        Drops = GetComponent<SpawnPickup>();
        if (IsBiter)
        {
            biterAI = GetComponentInParent<BiterEnemy_AI_Controller>();
        }
        if (IsBanshee)
        {
            bansheeAI = GetComponentInParent<Banshee_AI_Controller>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            BloodSplatter.Play();
            currentHealth -= damage;
            if (IsBiter)
            {
                biterAI.justGotHit = true;
            }
            if (IsBanshee)
            {
                bansheeAI.justGotHit = true;
            }

        }
    }

    public void Heal(int heal)
    {
        if (currentHealth < MaxHealth)
        {
            currentHealth += heal;

        }

        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
    }

    public void Death()
    {
        Drops.DropPickup(transform);
        Destroy(transform.parent.gameObject);
    }


}
