using System;
using UnityEngine;

public enum EItemType
{
    MoveSpeedUp,
    HealthUp,
    AttackSpeedUp,
    DestroyAllEnemies,
}

public class Item : MonoBehaviour
{
    [Header("아이템 타입")]
    public EItemType Type;
    public float Value;

    [Header("아이템 프리팹")]
    public GameObject ItemParticleEffect;

    public float WaitTime = 2f;
    public float MoveSpeed = 5f;

    private GameObject _playerObject;
    

    private void Start()
    {
        _playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

        WaitTime -= Time.deltaTime;
        if (WaitTime > 0) return;

        // 1. 플레이어를 찾는다.
        if (_playerObject == null) return;

        // 2. 방향을 구한다.
        Vector2 direction = _playerObject.transform.position - transform.position;

        // 3. 이동한다.
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;

        Apply(other);
        MakeItemParticleEffect();
        Destroy(gameObject);
    }

    private void Apply(Collider2D other)
    {
        // 아이템 타입에 따라서 다르게 적용
        switch (Type)
        {
            case EItemType.MoveSpeedUp:
                {
                    PlayerManualMove playerManualMove = other.GetComponent<PlayerManualMove>();
                    //playerManualMove.SpeedUp(Value);
                    break;
                }

            case EItemType.AttackSpeedUp:
                {
                    PlayerFire playerFire = other.GetComponent<PlayerFire>();
                    playerFire.SpeedUp(Value);
                    break;
                }

            case EItemType.HealthUp:
                {
                    Player player = other.GetComponent<Player>();
                    player.Heal(Value);
                    break;
                }
        }

    }

    private void MakeItemParticleEffect()
    {
        Instantiate(ItemParticleEffect, transform.position, Quaternion.identity);
    }
}