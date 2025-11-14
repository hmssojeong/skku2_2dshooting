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
    public GameObject ZigzagBulletPrefab;

    [Header("풀링")]
    public int PoolSize = 30;
    public int SubPoolSize = 30;
    public int ZigzagPoolSize = 30;

    private List<GameObject> _bulletObjectPool = new List<GameObject>(); // 게임 총알을 담아둘 풀: 탄창
    private List<GameObject> _subBulletPool = new List<GameObject>();
    private List<GameObject> _zigzagBulletPool = new List<GameObject>();

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
        CreatePool(_zigzagBulletPool, ZigzagBulletPrefab, ZigzagPoolSize);
    }

    private void CreatePool(List<GameObject> pool, GameObject prefab, int size)
    {
        if (prefab == null)
        {
            Debug.LogError("프리팹이 연결되지 않았습니다: " + prefab);
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

    public GameObject MakeZigZagBullet(Vector3 position, Vector3 targetPosition,
        float speed = 10f, float amplitude = 2f, float frequency = 5f)
    {
        GameObject bullet = GetBulletFromPool(_zigzagBulletPool, ZigzagBulletPrefab, position);

        ZigZagBullet zigzag = bullet.GetComponent<ZigZagBullet>();
        if (zigzag != null)
        {
            zigzag.Initialize(targetPosition, speed, amplitude, frequency);
        }

        return bullet;
    }
}