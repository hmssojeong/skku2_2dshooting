using System;
using UnityEngine;

// Enum : 열거형 : 기억하기 어려운 상수들을 기억하기 쉬운 이름 하나로 묶어(그룹) 관리하는 표현 방식
public enum EEnemyType
{
    Directional,
    Trace,
}

public class Enemy : MonoBehaviour
{
    [Header("스탯")]
    public float Speed;
    public float Damage = 1;
    private float _health = 100f;

    [Header("적 타입")]
    public EEnemyType Type;

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



    public void Hit(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 몬스터는 플레이어와만 충돌처리할 것이다.
        if (!other.gameObject.CompareTag("Player")) return;

        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        player.Hit(Damage);

        Destroy(gameObject);    // 나죽자.
    }
}