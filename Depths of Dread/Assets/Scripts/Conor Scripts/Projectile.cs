using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    public int Damage;
    public bool GoThroughEnemies;

    private BiterEnemy_AI_Controller b_A_C;
    public string HitAnimationName;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag!="IgnoreProjectileCollisions")
        {
            //Debug.Log(other.gameObject);
            if(!GoThroughEnemies)
            {
                if (other.gameObject.tag == "Enemy")
                {
                    other.gameObject.GetComponent<EnemyUnitHealth>().TakeDamage(Damage);
                    b_A_C = other.GetComponent<BiterEnemy_AI_Controller>();
                    b_A_C.BiterAnimator.Play(HitAnimationName);
                    //other.gameObject.GetComponent<Biter_AI_Controller>().State = Biter_AI_State.Chase;
                }
                Destroy(this.gameObject);
            }
            else
            {
                if(other.gameObject.tag != "Enemy")
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    other.gameObject.GetComponent<EnemyUnitHealth>().TakeDamage(Damage);
                    b_A_C = other.GetComponent<BiterEnemy_AI_Controller>();
                    b_A_C.BiterAnimator.Play(HitAnimationName);
                }
            }
            
        }

    }
}
