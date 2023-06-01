using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBall : MonoBehaviour
{
    public bool canHitEnemy;
    public Transform Target, nearestEnemy;
    Rigidbody rb;
    Vector3 direction;
    float damage = 10f;
    public EnemyBehaviour EB;
    //split SPL;

    void Start()
    {
        //SPL = transform.GetComponent<split>();
        //EB = transform.GetComponent<EnemyBehaviour>();
        rb = GetComponent<Rigidbody>();
        if (EB.Charmed)
        {
            direction = nearestEnemy.position - transform.position;
        }
        else
        {
            direction = Target.position - transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void FixedUpdate()
    {
        if (EB.Charmed)
        {
            rb.AddForce(direction.normalized *2, ForceMode.VelocityChange);
        }
        else
        {
            rb.AddForce(direction.normalized *2, ForceMode.VelocityChange);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<Player>().AbilityPassive3(damage);
            //other.transform.GetComponent<Health>().TakeDamage(damage);
            transform.gameObject.SetActive(false);
        }
        if (other.gameObject.layer == 3)
        {
            if (canHitEnemy)
            {
                other.transform.GetComponent<Health>().TakeDamage(damage);

                ParticleSystem[] ParticleSystems = GetComponentsInChildren<ParticleSystem>();

                foreach (ParticleSystem psq in ParticleSystems)
                {
                    var main = psq.main;
                    main.maxParticles = 0;
                }
                Destroy(transform.gameObject, 2);
            }           
        }
    }
}
