using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick JOYSTICK;

    public GameObject CurrentEnemyTarget;
    public Transform nearestEnemy;

    public Image HealthBar;

    public float SPEED, DetectSize;
    public bool enemyInRadius;

    public Health Health_This;

    public LayerMask EnemyLayer;
    GameManager GM;
    UIController UI;
    Rigidbody rb;
    private IEnumerator AA_loop;
    void Start()
    {
        UI = FindObjectOfType<UIController>();
        GM = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        AA_loop = AutoAttackLoop();
        StartCoroutine(AA_loop);
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        //isCooldown = false;
    }
    float angleA;

    [Header("Ability 1")]
    public GameObject AbilityPrefab;
    public Image abilityImage1;
    public float cooldown1 = 5;
    bool isCooldown = false;
    public KeyCode ability1;

    //Ability 1 Input Variables
    Vector3 position;
    public Canvas ability1Canvas;
    public Image skillshot;
    public Transform player;

    [Header("Ability 2")]
    public GameObject AbilityPrefab_CHARM;
    public Image abilityImage2;
    public float cooldown2 = 10;
    bool isCooldown2 = false;
    public KeyCode ability2;

    //Ability 2 Input Variables
    public Image targetCircle;
    public Image indicatorRangeCircle;
    public Canvas ability2Canvas;
    private Vector3 posUp;
    public float maxAbility2Distance;

    [Header("Ability 3")]
    public Image abilityImage3;
    public float cooldown3 = 7;
    bool isCooldown3 = false;
    public KeyCode ability3;

    Quaternion Indicator_Direction;
    void Update()
    {
        Ability1();
        //Ability2();
        //Ability3();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Ability 1 Inputs
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //position = new Vector3(hit.point.x, ability1Canvas.transform.position.y, hit.point.z);
        }

        ////Ability 2 Inputs
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //{
        //    if (hit.collider.gameObject != this.gameObject)
        //    {
        //        posUp = new Vector3(hit.point.x, 10f, hit.point.z);
        //        position = hit.point;
        //    }
        //}


        //Ability 1 Canvas Inputs
        Quaternion Indicator_Direction = Quaternion.LookRotation(position - player.transform.position);
        ability1Canvas.transform.rotation = Quaternion.Lerp(Indicator_Direction, ability1Canvas.transform.rotation, 0f);

        ////Ability 2 Canvas Inputs
        //var hitPosDir = (hit.point - transform.position).normalized;
        //float distance = Vector3.Distance(hit.point, transform.position);
        //distance = Mathf.Min(distance, maxAbility2Distance);

        //var newHitPos = transform.position + hitPosDir * distance;
        //ability2Canvas.transform.position = (newHitPos);    
        for (var i = 0; i < Input.touchCount; ++i)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    GM.isMoving = true;

                    break;
                case TouchPhase.Moved:
                    GM.isMoving = true;

                    ////abilityIndicator.GetComponent<Image>().enabled = true;

                    break;
                case TouchPhase.Ended:
                    GM.isMoving = false;
                    //if (enemyInRadius)
                    //{
                    //    InitAutoAttack();
                    //}

                    break;
            }


            //MoveToTarget(touch.position);
        }
        HealthBar.fillAmount = Health_This.CurrentHealth / Health_This.maxHealth;
    }
    public void A_1Cooldown()
    {
        if (enemyInRadius)
        {
            abilityImage1.gameObject.SetActive(true);
            GameObject go = Instantiate(AbilityPrefab, transform.position, AbilityPrefab.transform.rotation);
            go.transform.GetComponent<Fireball>().Direction = position - player.transform.position;
            skillshot.GetComponent<Image>().enabled = false;
            isCooldown = true;
            abilityImage1.fillAmount = 1;
            if (PlayerPrefs.GetInt("Level", 1) == 1)
            {
                GM.TutMoveDialog2();
            }
        }

    }
    public void A_1enable()
    {
        if (isCooldown == false)
        {
            skillshot.GetComponent<Image>().enabled = true;
        }
    }
    public void A_1cancel()
    {
        if (isCooldown == false)
        {
            skillshot.GetComponent<Image>().enabled = false;
        }
    }
    public void A_2Cooldown()
    {
        if (enemyInRadius)
        {
            abilityImage2.gameObject.SetActive(true);
            GameObject go = Instantiate(AbilityPrefab_CHARM, transform.position, AbilityPrefab_CHARM.transform.rotation);
            go.transform.GetComponent<Charm>().Direction = position - player.transform.position;
            skillshot.GetComponent<Image>().enabled = false;
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
            if (PlayerPrefs.GetInt("Level", 1) == 1)
            {
                GM.TutMoveDialog2();
            }
        }

    }
    public void A_2enable()
    {
        if (isCooldown2 == false)
        {
            skillshot.GetComponent<Image>().enabled = true;
        }
    }
    public void A_2cancel()
    {
        if (isCooldown2 == false)
        {
            skillshot.GetComponent<Image>().enabled = false;
        }
    }
    void Ability1()
    {
        if (Input.GetKey(ability1) && isCooldown == false)
        {
            skillshot.GetComponent<Image>().enabled = true;

            ////Disable Other UI
            //indicatorRangeCircle.GetComponent<Image>().enabled = false;
            //targetCircle.GetComponent<Image>().enabled = false;
        }

        //if (skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        //{
        //    isCooldown = true;
        //    abilityImage1.fillAmount = 1;
        //}

        if (isCooldown)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            //skillshot.GetComponent<Image>().enabled = false;

            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                abilityImage1.gameObject.SetActive(false);
                isCooldown = false;
            }
        }
        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            //skillshot.GetComponent<Image>().enabled = false;

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                abilityImage2.gameObject.SetActive(false);
                isCooldown2 = false;
            }
        }
        if (isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

            //skillshot.GetComponent<Image>().enabled = false;

            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                abilityImage3.gameObject.SetActive(false);
                isCooldown3 = false;
            }
        }
    }
    public void AbilityPassive3(float damage)
    {
        if (!isCooldown3)
        {
            //float randomFloat = Random.Range(0f, 1f); // Generates a random float between 0 and 1 (inclusive)

            //if (randomFloat < 0.5f)
            //{

            //}
            //else
            //{

            //}
            transform.GetComponent<Health>().Miss();
            abilityImage3.fillAmount = 1;
            abilityImage3.gameObject.SetActive(true);
            isCooldown3 = true;
        }
        else
        {
            transform.GetComponent<Health>().TakeDamage(damage);
        }
    }

    void Ability2()
    {
        if (Input.GetKey(ability2) && isCooldown2 == false)
        {
            indicatorRangeCircle.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;

            //Disable Skillshot UI
            skillshot.GetComponent<Image>().enabled = false;
        }

        if (targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
        }

        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }

    void Ability3()
    {
        if (Input.GetKey(ability3) && isCooldown3 == false)
        {
            isCooldown3 = true;
            abilityImage3.fillAmount = 1;
        }

        if (isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }

    public Collider[] hitColliders;
    Vector3 movement;
    private void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * JOYSTICK.Vertical + Vector3.right * JOYSTICK.Horizontal;
        //rb.AddForce(direction * SPEED * Time.fixedDeltaTime, ForceMode.Impulse);
        //Vector3 targetMovement = direction;
        //Vector3 newMovement = Vector3.Lerp(movement, direction, 0.5f);
        //movement = newMovement * Time.deltaTime;
        rb.velocity = direction * SPEED;
        //rb.rotation = Quaternion.Euler(0f, angleA, 0f);

        hitColliders = Physics.OverlapSphere(transform.position, DetectSize, EnemyLayer);
        float minimumDistance = Mathf.Infinity;
        foreach (Collider collider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                nearestEnemy = collider.transform;
                //nearestEnemy.GetChild(0).transform.gameObject.SetActive(true);
            }
            //else
            //{
            //    collider.transform.GetChild(0).transform.gameObject.SetActive(false);
            //}
        }
        if (nearestEnemy != null)
        {
            //nearestEnemy.GetComponent<MeshRenderer>().material.color = Color.red;
            //Debug.Log("Nearest Enemy: " + nearestEnemy + "; Distance: " + minimumDistance);
            enemyInRadius = true;
            position = new Vector3(nearestEnemy.position.x, ability1Canvas.transform.position.y, nearestEnemy.position.z);
        }
        if (hitColliders.Length == 0)
        {
            //Debug.Log("There is no enemy in the given radius");
            enemyInRadius = false;
        }
        //for (int i = 0; i < hitColliders.Length; i++)
        //{
        //    if (i == 1)
        //    {
        //        hitColliders[0].transform.GetChild(0).transform.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        hitColliders[i-1].transform.GetChild(0).transform.gameObject.SetActive(false);
        //    }

        //}
        if (GM.isMoving)
        {
            if (Mathf.Atan2(-JOYSTICK.Horizontal, JOYSTICK.Vertical) * Mathf.Rad2Deg != 0)
            { angleA = Mathf.Atan2(JOYSTICK.Horizontal, JOYSTICK.Vertical) * Mathf.Rad2Deg; }
            rb.transform.eulerAngles = new Vector3(0f, angleA, 0);
        }
        //if (!GM.isMoving)
        //{
        //    Vector3 enPos = nearestEnemy.position - transform.position;
        //    rb.transform.eulerAngles = new Vector3(0f, enPos.y, 0);
        //}
    }


    void MoveToTarget(Vector2 posOnScreen)
    {
        //Debug.Log(XsideMovement);
        Vector3 direction = (Vector3.forward * JOYSTICK.Vertical + Vector3.right * JOYSTICK.Horizontal);

        //Vector3 direction = (Vector3.forward * -JOYSTICK.Vertical / 4 + Vector3.forward * JOYSTICK.Horizontal / 4) * SPEED;
        //transform.eulerAngles = new Vector3(0, Mathf.Atan2(JOYSTICK.Horizontal, JOYSTICK.Vertical) * 180 / Mathf.PI, 0);
        //if (!isGrounded)
        //{
        //	rb.AddForce(new Vector3(direction.x * Xsensivity, -1 /** speed / 15*/, 1/* * speed / 15*/) * speed / 15, ForceMode.VelocityChange);
        //}
        //if (isGrounded)
        //{
        //	rb.AddForce(/*direction*/ new Vector3(direction.x * Xsensivity, 0, 1) * speed/15, ForceMode.VelocityChange);
        //}
        rb.AddForce(direction, ForceMode.VelocityChange);
    }

    public GameObject AABullet;
    public Transform BulletPos;
    public void InitAutoAttack()
    {
        //if (!GM.startSequence)
        //{
        //    var nearestCurrentObj = nearestEnemy;
        //    GameObject go = Instantiate(AABullet, BulletPos.position, AABullet.transform.rotation);
        //    go.transform.GetComponent<AutoAttack>().Target = nearestCurrentObj;
        //    Destroy(go, 2);
        //}
        var nearestCurrentObj = nearestEnemy;
        GameObject go = Instantiate(AABullet, BulletPos.position, AABullet.transform.rotation);
        go.transform.GetComponent<AutoAttack>().Target = nearestCurrentObj;
        Destroy(go, 2);
        if (PlayerPrefs.GetInt("Level", 1) == 1)
        {
            GM.TutMoveDialog1();
        }
    }
    public float AutoAttackInterval;
    public IEnumerator AutoAttackLoop()
    {
        while (true)
        {
            if (enemyInRadius && !GM.isMoving)
            {
                InitAutoAttack();
            }

            yield return new WaitForSeconds(AutoAttackInterval);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("FinishDoor"))
        {
            UI.LevelCompleted();
        }
    }
}
