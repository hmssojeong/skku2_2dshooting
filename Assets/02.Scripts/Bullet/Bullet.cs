using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // 필요 속성
    [Header("이동속도")]
    private float _speed;
    public float StartSpeed = 1;
    public float EndSpeed = 7;
    public float Acceleration = 1.2f;
    // Update is called once per frame

    private void Start()
    {
        _speed = StartSpeed;
    }
    void Update()
    {
        // 방향을 구한다.
        //Vector2 direction = new Vector2(0, 1); //위쪽 방향
        Vector2 direction = Vector2.up;

        // 방향에 따라 이동한다.
        // 새로운 위치는 = 현재위치 + 방향 * 속도 * 시간  
        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;
        transform.position = newPosition;

        _speed = Mathf.Clamp(_speed, StartSpeed, EndSpeed);

        float Acceleration = 1.2f;
        float BulletSpeed = (EndSpeed - StartSpeed) / Acceleration; // 초당 증가량
        BulletSpeed += BulletSpeed * Time.deltaTime;

        //_speed += Time.deltaTime; -> 1초당 +1과 같다.
    }
}
