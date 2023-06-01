using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charm : MonoBehaviour
{
    public Transform Target;
    float damage = 1;
    public Vector3 Direction;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(transform.gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //Vector3 direction = Target.position - transform.position;
        rb.AddForce(Direction.normalized, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            other.transform.GetComponent<Health>().TakeDamage(damage);
            other.transform.GetComponent<Health>().Charmed();
            other.transform.GetComponent<EnemyBehaviour>().Charmed = true;
            other.transform.GetComponent<EnemyBehaviour>().CharmCD();
            ParticleSystem[] ParticleSystems = GetComponentsInChildren<ParticleSystem>();

            foreach(ParticleSystem psq in ParticleSystems)
            {
                var main = psq.main;
                main.maxParticles = 0;
            }
            Destroy(transform.gameObject, 2);

        }
    }
}
