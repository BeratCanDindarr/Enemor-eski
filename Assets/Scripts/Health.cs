using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth, CurrentHealth;

    public GameObject DamagePopUp, DeathParticle;
    GameManager GM;
    UIController UI;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        UI = FindObjectOfType<UIController>();
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (CurrentHealth >= 0f)
        {
            CurrentHealth -= amount;
            GameObject go = Instantiate(DamagePopUp, new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), transform.position.y + 3, transform.position.z), DamagePopUp.transform.rotation);
            go.transform.GetComponent<DamageText>().DamageAmount = amount;
            if (transform.CompareTag("Dummy"))
            {
                transform.GetComponent<HitFlash_MeshRenderer>().TriggerHitFlash();
            }
            else
            {
                transform.GetComponent<HitFlash>().TriggerHitFlash();
            }

            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }
    }
    public void Miss()
    {
        GameObject go = Instantiate(DamagePopUp, new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), transform.position.y + 3, transform.position.z), DamagePopUp.transform.rotation);
        go.transform.GetComponent<DamageText>().isText = true;
        go.transform.GetComponent<DamageText>().Text_Input = "MISS";
        transform.GetComponent<HitFlash>().TriggerHitFlash();

    }
    public void Charmed()
    {

        //CurrentHealth -= amount;
        GameObject go = Instantiate(DamagePopUp, new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), transform.position.y + 3, transform.position.z), DamagePopUp.transform.rotation);
        go.transform.GetComponent<DamageText>().isText = true;
        go.transform.GetComponent<DamageText>().TextColorInit(Color.magenta);
        go.transform.GetComponent<DamageText>().TextColor = Color.magenta;
        go.transform.GetComponent<DamageText>().Text_Input = "CHARMED";
        if (transform.CompareTag("Dummy"))
        {
            transform.GetComponent<HitFlash_MeshRenderer>().TriggerHitFlash();
        }
        else
        {
            transform.GetComponent<HitFlash>().TriggerHitFlash();
        }


    }
    private void Die()
    {
        if(transform.gameObject.layer == 3)
        {
            GM.DeadEnemies++;
            if(GM.DeadEnemies >= GM.Enemies.Count)
            {
                GM.OpenFinalDoor();
            }
            GameObject go = Instantiate(DeathParticle, new Vector3(transform.position.x, 2.25f, transform.position.z), DeathParticle.transform.rotation);
            Destroy(go, 4f);
            transform.gameObject.SetActive(false);
        }
        else
        {
            GameObject go = Instantiate(DeathParticle, new Vector3(transform.position.x, 2.25f, transform.position.z), DeathParticle.transform.rotation);
            Destroy(go, 4f);
            transform.gameObject.SetActive(false);
            GM.gameOver = true;
            UI.OverInvoke();
        }
    }
}
