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

    private void Start()
    {
        _speed = StartSpeed;
    }

    private void Update()
    {
        // 목표: Duration 안에 EndSpeed까지 달성하고 싶다.

        // 논리적인 실수(1), 코딩 컨벤션(1)
        float acceleration = (EndSpeed - StartSpeed) / Duration;
        //                     6      / 1.2   = 5
        _speed += Time.deltaTime * acceleration;   // 초당 + 1 * 가속도
        _speed = Mathf.Min(_speed, EndSpeed);
        //         ㄴ 어떤 속성과 어떤 메서드를 가지고 있는지 톺아볼 필요가 있다.


        // 방향을 구한다.
        Vector2 direction = Vector2.up;

        // 공식에 따라 이동한다.
        // 새로운 위치는 = 현재 위치 + 방향 * 속력 * 시간
        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;
        transform.position = newPosition;
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        // 총알은 Enemy와만 충돌 이벤트를 처리한다.
        if (other.CompareTag("Enemy") == false) return;

        // GetComponent는 게임오브젝트에 붙어있는 컴포넌트를 가져올수있다. 
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        // 객체간의 상호 작용을 할때 : 묻지말고 시켜라(디미터의 법칙)
        enemy.Hit(Damage);

        gameObject.SetActive(false);
    }
}