using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 목표: 위로 계속 이동하고 싶다.

    // 필요 속성
    [Header("이동")]
    public float StartSpeed = 1f;
    public float EndSpeed = 7f;
    public float Duration = 1.2f;
    private float _speed;

    [Header("공격력")]
    public float Damage;

    private void OnEnable()
    {
        _speed = StartSpeed;
    }

    private void Update()
    {
        // 목표: Duration 안에 EndSpeed까지 달성하고 싶다.
        float acceleration = (EndSpeed - StartSpeed) / Duration;
        //                     6      / 1.2   = 5
        _speed += Time.deltaTime * acceleration;   // 초당 + 1 * 가속도
        _speed = Mathf.Min(_speed, EndSpeed);

        // 방향을 구한다.
        Vector3 direction = Vector3.up; // 2D 기준 위쪽
        transform.position += direction * _speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Hit(Damage);
            }


            gameObject.SetActive(false);

        }
        else if (other.CompareTag("Boss"))
        { 
           BossController boss = other.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDamage(Damage); ; // 여기서 BossController의 체력 함수 호출
            }

            gameObject.SetActive(false);
        }
    }
}