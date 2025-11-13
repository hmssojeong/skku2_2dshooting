using UnityEngine;

public class ZigZagBullet : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float amplitude;
    private float frequency;
    private float timer;

    public void Initialize(Vector3 targetPos, float speed, float amplitude, float frequency)
    {
        this.speed = speed;
        this.amplitude = amplitude;
        this.frequency = frequency;

        direction = (targetPos - transform.position).normalized;
        transform.forward = direction;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // 지그재그 이동
        Vector3 right = Vector3.Cross(direction, Vector3.up);
        Vector3 zigzagOffset = right * Mathf.Sin(timer * frequency) * amplitude;

        transform.position += direction * speed * Time.deltaTime + zigzagOffset * Time.deltaTime;
    }
}
