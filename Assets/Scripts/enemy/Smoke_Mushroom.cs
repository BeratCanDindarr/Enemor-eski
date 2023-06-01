using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke_Mushroom : MonoBehaviour
{
    float damage = 5f;
    public GameObject PlayerObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerObj = other.transform.gameObject;
            StartCoroutine(PoisonDamage());
        }
    }
    IEnumerator PoisonDamage()
    {
        PlayerObj.transform.GetComponent<Health>().TakeDamage(damage);
        yield return new WaitForSeconds(1f);
        PlayerObj.transform.GetComponent<Health>().TakeDamage(damage);
        yield return new WaitForSeconds(1f);
        PlayerObj.transform.GetComponent<Health>().TakeDamage(damage);
    }
}
