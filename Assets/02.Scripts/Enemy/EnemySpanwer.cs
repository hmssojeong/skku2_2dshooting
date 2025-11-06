using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 과제 3. 일정 시간마다 자신의 위치에 적을 생성

    // 프리팹을 참조할 배열
    public GameObject[] EnemyPrefabs;

    public int[] EnemySpawnRate;


    public float CoolTime = 2f;
    private float _timer;


    private void Start()
    {
        // 쿨타임을 1과 2사이로 랜덤하게 지정한다.
        float randomCoolTime = Random.Range(1f, 2f);
        CoolTime = randomCoolTime;
    }


    private void Update()
    {
        // 1. 시간이 흐르다가.
        _timer += Time.deltaTime;

        // 2. 쿨타임이 되면
        if (_timer >= CoolTime)
        {
            _timer = 0f;

            
            GameObject enemyObject = Instantiate(SelectRamdomEnemy());       // 70%
            enemyObject.transform.position = transform.position;

            // 3. 에너미프리팹으로부터 생성
            /*if (UnityEngine.Random.Range(0, 100) > 70)
            {
                GameObject enemyObject = Instantiate(EnemyPrefabs[(int)EEnemyType.Directional]);       // 70%
                enemyObject.transform.position = transform.position;
            }
            else
            {
                GameObject enemyObject = Instantiate(EnemyPrefabs[(int)EEnemyType.Trace]);             // 30%
                enemyObject.transform.position = transform.position;
            }*/
        }
    }

    private GameObject SelectRamdomEnemy()
    {
        int randomNumber = Random.Range(1, 101);


    
         for (int i = 0; i < EnemyPrefabs.Length; i++)
         {
            randomNumber -= EnemySpawnRate[i];
            
            if (randomNumber <= 0)
            {
               // 95        70  
               //    25     20
               //     5      10  =i
               return EnemyPrefabs[i];
            }
            
         }

        return EnemyPrefabs[0];
    }

}