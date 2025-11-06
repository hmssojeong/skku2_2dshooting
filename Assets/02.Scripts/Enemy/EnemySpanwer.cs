using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 과제 3. 일정 시간마다 자신의 위치에 적을 생성

    public GameObject EnemyPrefab;
    public float CoolTime = 2f;
    private float _timer;
    public float MinCoolTime = 1f;
    public float MaxCoolTime = 3f;


    private void Start()
    {
        // 쿨타임을 1과 3사이로 랜덤하게 지정한다.
        float randomCoolTime = Random.Range(MinCoolTime, MaxCoolTime);
        CoolTime = randomCoolTime;
    }


    private void Update()
    {
        // 1. 시간이 흐르다가.
        _timer += Time.deltaTime;

        // 2. 쿨타임이 되면
        if (_timer >= CoolTime)
        {
            CoolTime = Random.Range(MinCoolTime, MaxCoolTime); //매직넘버로
            _timer = 0f;

            // 3. 에너미프리팹으로부터 생성
            GameObject enemyObject = Instantiate(EnemyPrefab);
            enemyObject.transform.position = transform.position;
        }
    }

}