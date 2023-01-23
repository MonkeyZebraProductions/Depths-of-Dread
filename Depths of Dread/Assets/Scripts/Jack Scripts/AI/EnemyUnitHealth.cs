using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitHealth : MonoBehaviour
{
    public int currentHealth;
    public int MaxHealth;
    private ParticleSystem BloodSplatter;
    private SpawnPickup Drops;

    private Biter_FOV biterFOV;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        BloodSplatter = GetComponentInChildren<ParticleSystem>();
        Drops = GetComponent<SpawnPickup>();
        biterFOV = GetComponentInParent<Biter_FOV>();
    }

    // Update is called once per frame
    void Update()
    {
       if(currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            BloodSplatter.Play();
            currentHealth -= damage;
            biterFOV.canSeePlayer = true;
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
