using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("스탯")]
    public float Speed;
    public float Damage = 1;
    private float _health = 100f;

    private void Update()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * (Speed * Time.deltaTime));
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
        player.Hit(Damage);

        Destroy(other.gameObject);    // 나죽자.
    }
}