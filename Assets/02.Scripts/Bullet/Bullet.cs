using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // 필요 속성
    [Header("이동속도")]
    private float _speed;
    public float StartSpeed = 1;
    public float EndSpeed = 7;
    public float Duration = 1.2f;
    // Update is called once per frame

    private void Start()
    {
        _speed = StartSpeed;
    }
    void Update()
    {
        // 목표: Duration 안에 EndSpeed까지 달성하고 싶다.
        
        
        float Acceleration = (EndSpeed - StartSpeed) / Duration;
        //                        6     /   1.2   =  5
        _speed += Time.deltaTime * Acceleration;  // 초당 +  1 * 가속도
        _speed = Mathf.Min(_speed, EndSpeed);
        //         ㄴ 어떤 속성과 어떤 메서드를 가지고 있는지 톺아볼 필요가 있다.



        // 방향을 구한다.
        //Vector2 direction = new Vector2(0, 1); //위쪽 방향
        Vector2 direction = Vector2.up;

        // 방향에 따라 이동한다.
        // 새로운 위치는 = 현재위치 + 방향 * 속도 * 시간  
        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;
        transform.position = newPosition;
        
    }
}
