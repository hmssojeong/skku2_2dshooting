using System.Collections.Generic;
using UnityEngine;


public class BulletFactory : MonoBehaviour
{
    private static BulletFactory _instance = null;
    public static BulletFactory Instance => _instance;

    // 필요 속성
    [Header("총알 프리팹")] // 복사해올 총알 프리팹 게임 오브젝트
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;
    public GameObject StraightBulletPrefab;
    public GameObject TripleBulletPrefab;

    [Header("풀링")]
    public int PoolSize = 30;
    public int SubPoolSize = 30;
    public int StraightPoolSize = 30;
    public int TriplePoolSize = 30;

    private List<GameObject> _bulletObjectPool = new List<GameObject>(); // 게임 총알을 담아둘 풀: 탄창
    private List<GameObject> _subBulletPool = new List<GameObject>();
    private List<GameObject> _straightBulletPool = new List<GameObject>();
    private List<GameObject> _tripleBulletPool = new List<GameObject>();

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        PoolInit();
    }

    private void PoolInit()
    {
        CreatePool(_bulletObjectPool, BulletPrefab, PoolSize);
        CreatePool(_subBulletPool, SubBulletPrefab, SubPoolSize);
        CreatePool(_straightBulletPool, StraightBulletPrefab, StraightPoolSize);
        CreatePool(_tripleBulletPool, TripleBulletPrefab, TriplePoolSize);
    }

    private void CreatePool(List<GameObject> pool, GameObject prefab, int size)
    {
        if (prefab == null)
        {
            return;
        }

        for (int i = 0; i < size; i++)
        {
            GameObject BulletObject = Instantiate(prefab, transform);
            BulletObject.SetActive(false);
            pool.Add(BulletObject);
        }
    }

    private GameObject GetBulletFromPool(List<GameObject> pool, GameObject prefab, Vector3 pos)
    {
        foreach (var bullet in pool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = pos;
                bullet.SetActive(true);
                return bullet;
            }
        }

        // 풀 부족 → 자동 확장
        GameObject newBullet = Instantiate(prefab, pos, Quaternion.identity, transform);
        pool.Add(newBullet);
        return newBullet;
    }

    // 총알 생성
    public GameObject MakeBullet(Vector3 position)
    {
        return GetBulletFromPool(_bulletObjectPool, BulletPrefab, position);
    }

    public GameObject MakeSubBullet(Vector3 position)
    {
        return GetBulletFromPool(_subBulletPool, SubBulletPrefab, position);
    }

    public GameObject MakeStraightBullet(Vector3 position)
    {
        return GetBulletFromPool(_straightBulletPool, StraightBulletPrefab, position);
    }
    public GameObject MakeTripleBullet(Vector3 position)
    {
        return GetBulletFromPool(_tripleBulletPool, TripleBulletPrefab, position);
    }

}