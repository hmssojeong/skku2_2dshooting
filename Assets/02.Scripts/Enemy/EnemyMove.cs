using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("스탯")]
    public float Speed = 3f;
    public float Damage = 2f;
    private float _health = 100f;

    public float MoveDuration = 0.5f; // 첫 번쨰 전체 움직이는 시간
    private float _currentFirstMoveTime = 0;    //시작할 때부터 멈추기 전까지의 시간
    public float StopDuration = 2f; // 전체 멈추는 시간
    private float _currentStopTime = 0;     //멈추기 시작했을 때 현재까지 멈춘 시간

    private void Update()
    {
        if(_currentFirstMoveTime <= MoveDuration)
        {
            _currentFirstMoveTime += Time.deltaTime;
            transform.position += Vector3.down * Speed * Time.deltaTime;
            return;
        }

        if(_currentStopTime <= StopDuration)
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