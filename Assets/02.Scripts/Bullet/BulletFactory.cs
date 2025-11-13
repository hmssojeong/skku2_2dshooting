using UnityEngine;
using UnityEngine.UIElements;

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory _instance = null;

    public static BulletFactory Instance => _instance;
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
        // Awake vs Start vs Lazy
        // 1. 탄창을 총알을 담을 수 있는 크기로 만들어준다.
        _bulletObjectPool = new GameObject[PoolSize];

        // 2. 탄창 크기만큼 반복해서
        for (int i = 0; i < PoolSize; i++)
        {
            // 3. 총알을 생성한다.
            GameObject bulletObject = Instantiate(BulletPrefab, transform);

            // 4. 생성한 총알을 탄창에 담는다.
            _bulletObjectPool[i] = bulletObject;

            // 5. 비활성화 한다.
            bulletObject.SetActive(false);
        }
    }
    // 필요 속성
    [Header("총알 프리팹")] // 복사해올 총알 프리팹 게임 오브젝트
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

    [Header("풀링")]
    public int PoolSize = 30;
    private GameObject[] _bulletObjectPool;  // 게임 총알을 담아둘 풀: 탄창


    public GameObject MakeBullet(Vector3 position)
    {
        // 필요하다면 여기서 생성 이펙트도 생성하고
        // 필요하다면 인자값으로 대미지도 받아서 넘겨주고..

        // 1. 탄창 안에 있는 총알들 중에서
        for (int i = 0; i<PoolSize; i++)
        {
            GameObject bulletObject = _bulletObjectPool[i];

            // 2. 비활성화된 총알 하나를 찾아.
            if (bulletObject.activeInHierarchy == false)
            {
                // 3. 위치를 수정하고, 활성화시킨다.
                bulletObject.transform.position = position;
                bulletObject.SetActive(true);

                return bulletObject;
            }
        }

        Debug.LogError("탄창에 총알 개수가 부족합니다. [정희연을 찾아주세요.]");
        return null;
    }

    public GameObject MakeSubBullet(Vector3 position)
    {
        return Instantiate(SubBulletPrefab, position, Quaternion.identity, transform);
    }
}
