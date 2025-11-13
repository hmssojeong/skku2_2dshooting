using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("플레이어")]
    public Transform player;

    [Header("원형 이동 설정")]
    public float radius = 5f;          // 원의 반지름
    public float orbitSpeed = 50f;     // 회전 속도(degrees/sec)
    public float height = 4f;          // 상단 y 위치

    [Header("총알 설정")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 1.5f;  // 총알 발사 간격
    public float bulletSpeed = 10f;
    public float zigZagAmplitude = 2f; // 지그재그 폭
    public float zigZagFrequency = 5f; // 지그재그 속도

    [Header("보스 체력")]
    public int maxHealth = 300;
    private int currentHealth;

    private float angle = 0f;          // 현재 각도
    private float fireTimer = 0f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (player == null) return;

        OrbitMovement();
        HandleShooting();
    }

    private void OrbitMovement()
    {
        angle += orbitSpeed * Time.deltaTime;
        if (angle > 360f) angle -= 360f;

        float rad = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * radius;
        transform.position = new Vector3(offset.x, height, offset.z);
    }

    private void HandleShooting()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            ShootZigZagBullet();
        }
    }
    private void ShootZigZagBullet()
    {
        if (firePoint == null || player == null) return;

        BulletFactory.Instance.MakeZigZagBullet(
            firePoint.position,
            player.position,
            bulletSpeed,
            zigZagAmplitude,
            zigZagFrequency
        );
    }

    // 보스 데미지 함수
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("보스 몬스터 사망!");
        Destroy(gameObject);
    }
}
