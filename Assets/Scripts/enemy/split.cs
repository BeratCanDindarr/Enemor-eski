using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class split : MonoBehaviour
{
    public bool canMove;
    public GameObject AttackAbility;
    public Transform spawnPos;
    public Transform PlayerObj;

    public Image EnemyHealthBar;
    Rigidbody rb;
    public EnemyBehaviour EB;

    public GameObject CurrentEnemyTarget;
    public Transform nearestEnemy, secondNearestEnemy;
    public float DetectSize;
    public bool enemyInRadius;
    public Collider[] hitColliders;
    public LayerMask EnemyLayer;

    public Animator anim;

    GameManager GM;

    Health Health_This;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        Health_This = transform.GetComponent<Health>();
        rb = transform.GetComponent<Rigidbody>();
        EB = transform.GetComponent<EnemyBehaviour>();
        PlayerObj = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(AA_cd());
        //EnemyLayer = 3;
        DetectSize = 85;
        //anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //var adjusted = PlayerObj.position;
        //adjusted.x = 0;
        //adjusted.y = 0;

        EnemyHealthBar.fillAmount = Health_This.CurrentHealth / Health_This.maxHealth;

    }
    private void FixedUpdate()
    {


        if (EB.Charmed)
        {
            hitColliders = Physics.OverlapSphere(transform.position, DetectSize, EnemyLayer);
            Physics.IgnoreLayerCollision(gameObject.layer, EnemyLayer);

            float minimumDistance = Mathf.Infinity;
            float secondMinimumDistance = Mathf.Infinity;
            //nearestEnemy = null;
            //secondNearestEnemy = null;

            foreach (Collider collider in hitColliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (distance < minimumDistance)
                {
                    // Update both the nearest and second nearest enemy
                    secondMinimumDistance = minimumDistance;
                    secondNearestEnemy = nearestEnemy;

                    minimumDistance = distance;
                    nearestEnemy = collider.transform;
                }
                else if (distance < secondMinimumDistance)
                {
                    // Update only the second nearest enemy
                    secondMinimumDistance = distance;
                    secondNearestEnemy = collider.transform;
                }
            }

            if (nearestEnemy != null)
            {
                // Look at the second nearest enemy
                if (secondNearestEnemy != null)
                {
                    transform.LookAt(secondNearestEnemy.position);
                    //if (canMove)
                    //{
                    //    rb.position = Vector3.MoveTowards(rb.position, secondNearestEnemy.position, 1.42f * Time.deltaTime);
                    //}
                }
                else
                {
                    transform.LookAt(PlayerObj.position);
                    if (canMove && !GM.gameOver)
                    {
                        rb.position = Vector3.MoveTowards(rb.position, PlayerObj.position, 3.2f * Time.deltaTime);
                    }
                }

                enemyInRadius = true;
            }
            else
            {
                // No enemies in radius
                Debug.Log("There is no enemy in the given radius");
                enemyInRadius = false;
            }

        }
        if (!EB.Charmed)
        {
            transform.LookAt(PlayerObj.position);
            if (canMove && !GM.gameOver)
            {
                rb.position = Vector3.MoveTowards(rb.position, PlayerObj.position, 3.2f * Time.deltaTime);
            }
        }
    }
    public IEnumerator AA_cd()
    {
        while (true)
        {
            canMove = true;
            yield return new WaitForSeconds(2.5f);
            canMove = false;
            //anim.SetBool("isAttack", true);
            //anim.SetBool("idle", false);
            yield return new WaitForSeconds(.25f);
            //anim.SetBool("isAttack", false);
            //anim.SetBool("isWalk", true);
            if (!GM.startSequence && EB.PlayerInRadius)
            {
                GameObject go = Instantiate(AttackAbility, spawnPos.position, AttackAbility.transform.rotation);
                go.transform.GetComponent<AcidBall>().Target = PlayerObj;
                go.transform.GetComponent<AcidBall>().EB = EB;
                go.transform.GetComponent<AcidBall>().nearestEnemy = secondNearestEnemy;
                Destroy(go, 5);
            }
            if (!GM.startSequence && EB.Charmed)
            {
                GameObject go = Instantiate(AttackAbility, spawnPos.position, AttackAbility.transform.rotation);
                go.transform.GetComponent<AcidBall>().Target = PlayerObj;
                go.transform.GetComponent<AcidBall>().EB = EB;
                go.transform.GetComponent<AcidBall>().nearestEnemy = secondNearestEnemy;
                go.transform.GetComponent<AcidBall>().canHitEnemy = true;
                Destroy(go, 5);
            }
        }
    }
}
