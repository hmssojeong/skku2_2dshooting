using UnityEngine;

public class ZigZagBullet : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 targetPos;

    private float speed;
    private float amplitude;
    private float frequency;

    private float time;

    public void Initialize(Vector3 target, float speed, float amplitude, float frequency)
    {
        this.targetPos = target;
        this.speed = speed;
        this.amplitude = amplitude;
        this.frequency = frequency;

        this.startPos = transform.position;
        this.time = 0f;
    }

    private void OnEnable()
    {
        time = 0f;
    }

    private void Update()
    {
        time += Time.deltaTime;

        // 직선 이동
        Vector3 direction = (targetPos - startPos).normalized;
        Vector3 forwardMove = direction * speed * Time.deltaTime;

        // 지그재그 이동 (좌우)
        float zigzagOffset = Mathf.Sin(time * frequency) * amplitude;
        Vector3 side = new Vector3(-direction.y, direction.x, 0); // 방향에 수직 방향

        transform.position += forwardMove + side * zigzagOffset * Time.deltaTime;

        // 화면 벗어나면 비활성화
        if (transform.position.y > 10f || transform.position.y < -10f ||
            transform.position.x > 10f || transform.position.x < -10f)
        {
            gameObject.SetActive(false);
        }
    }
}
