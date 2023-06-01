using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public Transform Target;
    float damage = 15f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 direction = Target.position - transform.position;
        rb.AddForce(direction.normalized, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            other.transform.GetComponent<Health>().TakeDamage(damage);
            transform.gameObject.SetActive(false);
        }
    }
}
