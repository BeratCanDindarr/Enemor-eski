using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("STATUS")]
    public bool Charmed;
    public bool Awaken;
    public bool Alive;

    public float CharmCooldown;
    void Start()
    {
        DetectSize = 18f;
    }

    public Collider[] hitColliders;
    public float DetectSize;
    public bool PlayerInRadius;
    public LayerMask PlayerLayer;
    public Transform nearestPlayer;
    void Update()
    {
        hitColliders = Physics.OverlapSphere(transform.position, DetectSize, PlayerLayer);
        float minimumDistance = Mathf.Infinity;
        foreach (Collider collider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                nearestPlayer = collider.transform;
                //nearestEnemy.GetChild(0).transform.gameObject.SetActive(true);
            }
            //else
            //{
            //    collider.transform.GetChild(0).transform.gameObject.SetActive(false);
            //}
        }
        if (nearestPlayer != null)
        {
            //nearestEnemy.GetComponent<MeshRenderer>().material.color = Color.red;
            //Debug.Log("Nearest Enemy: " + nearestPlayer + "; Distance: " + minimumDistance);
            PlayerInRadius = true;
        }
        if (hitColliders.Length == 0)
        {
            //Debug.Log("There is no enemy in the given radius");
            PlayerInRadius = false;
        }
    }

    public void CharmCD()
    {
        StartCoroutine(SetBoolAfterDelay());
    }

    IEnumerator SetBoolAfterDelay()
    {
        yield return new WaitForSeconds(8f);
        Charmed = false;
    }
}
