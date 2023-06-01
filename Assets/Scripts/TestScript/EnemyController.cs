using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class EnemyController : MonoBehaviour
{

    [Header("Enemies Properties")]
    public Enemy_SO.EnemyData enemyData;

    [SerializeField] protected Transform target;
    private Vector3[] linearPath = new Vector3[3];
    protected Animator animator;
    protected NavMeshAgent agent;

    protected virtual void AttachAnimator()
    {
        animator = GetComponent<Animator>();
    }
    
    protected virtual void AttachNavMeshAgent()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void StraightAttack()
    {
        GameObject attack = SpawnAttackPrefab();
        attack.GetComponent<Rigidbody>().AddForce(transform.forward * enemyData.AttackSpeed * Time.deltaTime);
    }

    private GameObject SpawnAttackPrefab()
    {

        return Instantiate(enemyData.AttackPrefab, transform.position, Quaternion.identity);
    }

    protected virtual void CurvedAttack()
    {
        GameObject prefab = SpawnAttackPrefab();
        AddPath();
        prefab.transform.DOPath(linearPath, 1f ,PathType.CatmullRom);
    }
    private void AddPath()
    {
        linearPath[0] = transform.position;
        linearPath[1] = new Vector3((transform.position.x + target.transform.position.x) / 2, 4, (transform.position.z + target.transform.position.z) / 2);
        linearPath[2] = target.transform.position;
    }



}
