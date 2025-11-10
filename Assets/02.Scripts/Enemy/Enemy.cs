using System;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;

// Enum : 열거형 : 기억하기 어려운 상수들을 기억하기 쉬운 이름 하나로 묶어(그룹) 관리하는 표현 방식
public enum EEnemyType
{
    Directional,             // 0
    Trace,                   // 1
    MovePause                // 2
}

public class Enemy : MonoBehaviour
{
    [Header("스탯")]
    public float Speed;
    public float Damage = 1;
    private float _health = 100f;

    [Header("적 타입")]
    public EEnemyType Type;

    public float CoolTime = 2f;

    [Header("적 움직임 시간")]
    public float MoveDuration = 0.5f; // 첫 번쨰 전체 움직이는 시간
    private float _currentFirstMoveTime = 0;    //시작할 때부터 멈추기 전까지의 시간
    public float StopDuration = 2f; // 전체 멈추는 시간
    private float _currentStopTime = 0;     //멈추기 시작했을 때 현재까지 멈춘 시간

    [Header("아이템 프리팹")]
    public GameObject[] ItemPrefabs;
    public int[] ItemWeights;

    private void Update()
    {
        // 두가지 타입
        if (Type == EEnemyType.Directional)
        {
            MoveDirectional();
        }
        else if (Type == EEnemyType.Trace)
        {
            MoveTrace();
        }
        else if (Type == EEnemyType.MovePause)
        {
            MovePause();
        }

        // 0. 타입에 따라 동작이 다르네?              -> 함수로 쪼개자..
        // 1. 함수가 너무 많아질거 같네?  (OCP위반)    -> 클래스로 쪼개자..
        // 2. 쪼개고 나니까 똑같은 기능/속성이 있네     -> 상속
        // 3. 상속을 하자니 책임이 너무 크네(SRP위반)   -> 조합
    }

    private void MoveDirectional()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * (Speed * Time.deltaTime));
    }

    private void MoveTrace()
    {
        // 1. 플레이어의 위치를 구한다.
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject == null) return;
        Vector2 playerPosition = playerObject.transform.position;

        // 2. 위치에 따라 방향을 구한다.
        Vector2 direction = playerPosition - (Vector2)transform.position;
        direction.Normalize();

        // 3. 방향에 맞게 이동한다.
        transform.Translate(direction * Speed * Time.deltaTime);
    }

    private void MovePause()
    {
        if (_currentFirstMoveTime <= MoveDuration)
        {
            _currentFirstMoveTime += Time.deltaTime;
            transform.position += Vector3.down * Speed * Time.deltaTime;
            return;
        }

        if (_currentStopTime <= StopDuration)
        {
            _currentStopTime += Time.deltaTime;
            return;
        }

        transform.position += Vector3.down * Speed * Time.deltaTime;
    }


    public void Hit(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            DropItem();
            Destroy(this.gameObject);
            
        }
    }

    private void DropItem()
    {
        // 50% 확률로 리턴
        if (Random.Range(0, 2) == 0) return;

        // 가중치의 합
        // ItemWeight [70, 20, 10]
        int weightSum = 0; // 100
        for (int i = 0; i < ItemWeights.Length; ++i)
        {
            weightSum += ItemWeights[i];
        }

        // 0 ~ 100 가중치의 합
        int randomValue = UnityEngine.Random.Range(0, weightSum); // 80

        //가중치 값을 더해가며 구간을 비교한다.
        // <            70 -> 0번째 아이템 생성되고
        // < (70+20)    90 -> 1번째 아이템 생성되고
        // < (90+10)   105 -> 2번째 아이템이 생성된다.
        int sum = 0;
        for ( int i = 0; i < ItemWeights.Length; ++i)
        {
            sum += ItemWeights[i];
            if(randomValue < sum)
            {
                Instantiate(ItemPrefabs[i], transform.position, Quaternion.identity);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 몬스터는 플레이어와만 충돌처리할 것이다.
        if (!other.gameObject.CompareTag("Player")) return;

        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        player.Hit(Damage);

        Destroy(gameObject); // 나죽자.

    }
}