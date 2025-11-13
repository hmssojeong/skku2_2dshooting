using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public static BossSpawner instance;

    [Header("보스 프리팹")]
    public GameObject bossPrefab;

    [Header("스폰 위치")]
    public Transform spawnPoint;

    [Header("보스 스폰 조건 점수")]
    public int bossSpawnScore = 5000;

    private bool bossSpawned = false;

    private void Awake()
    {
        // 싱글톤 설정
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        // 게임 시작 시 점수가 조건 이상이면 보스 소환
        if (ScoreManager.Instance != null)
        {
            CheckSpawnBoss(ScoreManager.Instance.CurrentScore);
        }
    }

    /// <summary>
    /// 점수가 바뀔 때마다 호출
    /// </summary>
    public void CheckSpawnBoss(int currentScore)
    {
        if (bossSpawned) return;

        if (currentScore >= bossSpawnScore)
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        if (bossPrefab == null)
        {
            Debug.LogError("보스 프리팹이 할당되지 않았습니다!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("스폰 위치가 할당되지 않았습니다!");
            return;
        }

        Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        bossSpawned = true;
        Debug.Log("보스 소환 완료!");
    }
}
