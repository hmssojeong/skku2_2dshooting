using UnityEngine;
using System.Collections.Generic;

public class EnemyFactory : MonoBehaviour
{
    // 적 프리팹
    public GameObject EnemyPrefab;

    // 풀 사이즈
    public int PoolSize;

    // 오브젝트풀 배열
    private List<GameObject> _enemyObjectPool;

    private void Awake()
    {
        _enemyObjectPool = new List<GameObject>();

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject Enemy = Instantiate(EnemyPrefab);

            _enemyObjectPool.Add(Enemy);
            Enemy.SetActive(false);
        }
    }

}
