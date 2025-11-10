using System;
using UnityEngine;

public enum EItemType
{
    MoveSpeedUp,
    HealthUp,
    AttackSpeedUp,
}

public class Item : MonoBehaviour
{
    [Header("아이템 타입")]
    public float Speed;
    public EItemType Type;
    public float Value;

    private float _timer = 0;
    public float CoolTime = 2f;

    public void Update()
    {
        _timer += Time.deltaTime; //시간을 더해줘
        Get();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;

        Apply(other);
        
        Destroy(gameObject);
    }

    private void Get()
    {
        if (_timer > CoolTime)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject == null) return;
            Vector2 playerPosition = playerObject.transform.position;


            Vector2 direction = playerPosition - (Vector2)transform.position;
            direction.Normalize();

            transform.Translate(direction * Speed * Time.deltaTime);

        }
    }
    private void Apply(Collider2D other)
    {
        // 아이템 타입에 따라서 다르게 적용
        switch (Type)
        {
            case EItemType.MoveSpeedUp:
                {
                    PlayerMove playerMove = other.GetComponent<PlayerMove>();
                    playerMove.SpeedUp(Value);
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
}