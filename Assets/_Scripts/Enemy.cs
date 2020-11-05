using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float lookRadius = 20f;
    public float damage = 5f;

    Transform target;
    NavMeshAgent agent;
    public Animator anim;

    public void DamageTaken(float dmg)
    {
        health -= dmg;

        if (health <= 0f)
        {
            agent.isStopped = true;
            agent.gameObject.GetComponent<BoxCollider>().enabled = false;
            damage = 0f;
            anim.SetBool("Dead", true);
            Destroy(gameObject,6f);
            
        }
    } 

    void OnEnable()
    {
        anim.SetBool("Attack", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }
    float timer = 0;
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Attack", false);

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <=  agent.stoppingDistance)
            {
                faceTheTarget();

                if (timer > 1f || timer == 0)
                {
                    attack();
                    timer = 0;
                }

                timer += Time.deltaTime;
            }
        }
    }

    void attack()
    {

        anim.SetBool("Attack", true);

        Player theTarget = target.transform.GetComponent<Player>();

        theTarget.DamageTaken(damage);

        //anim.SetBool("attack", false);
    }

    void faceTheTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion look = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 4f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    void OnTriggerEnter(Collider other)
    {

        Debug.Log("Hitting something ");
        Debug.Log(other);
         
    }
}
