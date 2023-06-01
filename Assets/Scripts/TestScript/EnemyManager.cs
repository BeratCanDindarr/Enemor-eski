using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    public static EnemyManager instance;

    public EnemyManager() 
    { 
        if(instance == null)
            instance = this;
    }
    #endregion

    [Header("Enemy Data")]
    public Enemy_SO enemiesData;

    [Header("Active Enemies")]
    public List<GameObject> enemies;
}
