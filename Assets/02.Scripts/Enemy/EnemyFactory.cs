using UnityEngine;
using System.Collections.Generic;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance;

    [Header("적 프리팹(타입별)")]
    public GameObject DirectionalEnemyPrefab; // 타입별 프리팹
    public GameObject TraceEnemyPrefab;
    public GameObject MovePauseEnemyPrefab;

    [Header("풀 사이즈")]
    public int DirectionalPoolSize = 10;
    public int TracePoolSize = 10;
    public int MovePausePoolSize = 10;

    // 각 타입별 풀을 리스트 안에 리스트로 관리
    private List<GameObject> _direcionalenemyPool = new List<GameObject>();
    private List<GameObject> _traceenemyPool = new List<GameObject>();
    private List<GameObject> _movePauseenemyPool = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
      
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        

        PoolInit();
    }

    private void PoolInit()
    {
        CreatePool(_direcionalenemyPool, DirectionalEnemyPrefab, DirectionalPoolSize);
        CreatePool(_traceenemyPool, TraceEnemyPrefab, TracePoolSize);
        CreatePool(_movePauseenemyPool, MovePauseEnemyPrefab, MovePausePoolSize);
    }

    private void CreatePool(List<GameObject> pool, GameObject prefab, int size)
    {
        if (prefab == null)
        { 
            return;
        }

        for (int i = 0; i < size; i++)
        {
            GameObject EnemyObject = Instantiate(prefab, transform);
            EnemyObject.SetActive(false);
            pool.Add(EnemyObject);
        }
    }

    // 적 꺼내기 (Get)
    public Enemy GetEnemy(List<GameObject> pool, GameObject prefab, Vector3 spawnPos)
    {
        // 비활성화된 적을 찾아서 반환
        foreach (var obj in pool)
        {
            if (!obj.activeSelf)
            {
                obj.transform.position = spawnPos;
                obj.SetActive(true);

                Enemy enemy = obj.GetComponent<Enemy>();
                enemy.ResetEnemy();

                return enemy;
            }
        }

        // 풀에 남은 적이 없으면 새로 생성
        GameObject newEnemy = Instantiate(prefab, spawnPos, Quaternion.identity, transform);
        pool.Add(newEnemy);
        return newEnemy.GetComponent<Enemy>();
    }

    public void ReturnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}
