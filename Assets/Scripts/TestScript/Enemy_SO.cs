using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Create/Enemy/new Enemy", fileName ="New Enemy Type")]
public class Enemy_SO : ScriptableObject
{
    [Serializable]
   public class EnemyData
    {
        public string Name;
        public float Health;
        public float Speed;
        public float FireRate;
        public float AttackRange;
        public float AttackSpeed;
        public GameObject AttackPrefab;
    }

   public List<EnemyData> Enemies;
}
