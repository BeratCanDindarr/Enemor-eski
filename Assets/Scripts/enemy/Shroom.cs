using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shroom : MonoBehaviour
{
    public bool canMove;
    public GameObject AttackAbility;
    public Transform spawnPos;
    public Transform PlayerObj;

    public Image EnemyHealthBar;

    public Health Health_This;

    public EnemyBehaviour EB;
    Rigidbody rb;

    public GameObject CurrentEnemyTarget;
    public Transform nearestEnemy, secondNearestEnemy;
    public float DetectSize;
    public bool enemyInRadius;
    public Collider[] hitColliders;
    public LayerMask EnemyLayer;

    public Animator anim;

    GameManager GM;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        Health_This = transform.GetComponent<Health>();
        PlayerObj = GameObject.FindGameObjectWithTag("Player").transform;
        EB = transform.GetComponent<EnemyBehaviour>();
        rb = transform.GetComponent<Rigidbody>();
        StartCoroutine(AA_cd());
    }

    // Update is called once per frame
    void Update()
    {

        EnemyHealthBar.fillAmount = Health_This.CurrentHealth / Health_This.maxHealth;
    }
    private void FixedUpdate()
    {
        transform.LookAt(PlayerObj.position);
        if (canMove && !GM.gameOver)
        {
            rb.position = Vector3.MoveTowards(rb.position, PlayerObj.position, 2.8f * Time.deltaTime);
        }
    }
    public GameObject SMOKE;
    public IEnumerator AA_cd()
    {
        while (true)
        {
            SMOKE.SetActive(false);
            canMove = true;
            yield return new WaitForSeconds(3.5f);
            canMove = true;
            SMOKE.SetActive(true);
            //anim.SetBool("isAttack", true);
            //anim.SetBool("idle", false);
            yield return new WaitForSeconds(3.25f);
        }
    }
}
