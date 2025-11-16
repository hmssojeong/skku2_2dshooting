using UnityEngine;

public class StraightBullet : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 targetPos;

    [Header("이동")]
    public float StartSpeed = 1f;
    public float EndSpeed = 3f;
    public float Duration = 1f;
    private float _speed;

    [Header("공격력")]
    public float Damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Hit(Damage);
            }

            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _speed = StartSpeed;
    }

    private void Update()
    {
        float acceleration = (EndSpeed - StartSpeed) / Duration;

        _speed += Time.deltaTime * acceleration;
        _speed = Mathf.Min(_speed, EndSpeed);

        Vector3 direction = Vector3.down;
        transform.position += direction * _speed * Time.deltaTime;
    }
}
