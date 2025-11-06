using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 과제 3. 일정 시간마다 자신의 위치에 적을 생성

    public GameObject EnemyPrefab;
    public GameObject EnemyPrefabDirect;
    public GameObject EnemyPrefabTrace;
    public float CoolTime = 2f;
    public float MinCoolTime = 1f;
    public float MaxCoolTime = 3f;
    public float TraceEnemyProbability = 0.3f;
    private float _timer = 0f;

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

            Generation();

        }

    }

    public void Generation()
    {
        float randomValue = UnityEngine.Random.value; // 0.0 ~ 1.0 사이의 랜덤 값 생성
        //0.75

        if (randomValue <= TraceEnemyProbability) //0.3
        {
            Instantiate(EnemyPrefabTrace, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(EnemyPrefabDirect, transform.position, Quaternion.identity);
        }

    }

}