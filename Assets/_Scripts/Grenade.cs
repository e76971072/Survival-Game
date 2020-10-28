using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public GameObject explosionEff;
    public float blastRadius = 5f;
    public float explosionForce = 700f;
    public GameObject explosionSound;

    float countDown;
    bool Exploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f && Exploded == false)
        {
            Explode();
            Exploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEff, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider hits in colliders)
        {
            Rigidbody rb = hits.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);

                if (rb.transform.name.Equals("Enemy") || rb.transform.name.Equals("Enemy(Clone)"))
                {
                    Debug.Log("I hit the enemy!");
                    rb.transform.GetComponent<Enemy>().DamageTaken(100f);
                }
                if (rb.transform.name.Equals("Player"))
                {
                    Debug.Log("I hit myself!");
                    rb.transform.GetComponent<Player>().DamageTaken(75f);
                }
            }
        }

        explosionSound.GetComponent<AudioSource>().Play();

        Destroy(gameObject);
        

    }
}
