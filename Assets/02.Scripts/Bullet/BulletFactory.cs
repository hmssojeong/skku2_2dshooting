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
    }
    // 필요 속성
    [Header("총알 프리팹")] // 복사해올 총알 프리팹 게임 오브젝트
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

    public GameObject MakeBullet(Vector3 position)
    {
        // 필요하다면 여기서 생성 이펙트도 생성하고
        // 필요하다면 인자값으로 대미지도 받아서 넘겨주고..

        return Instantiate(BulletPrefab, position, Quaternion.identity, transform);
    }

    public GameObject MakeSubBullet(Vector3 position)
    {
        return Instantiate(SubBulletPrefab, position, Quaternion.identity, transform);
    }
}
