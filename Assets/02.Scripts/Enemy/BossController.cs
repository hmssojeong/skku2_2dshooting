using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("플레이어")]
    public GameObject Player;

    [Header("원형 이동 설정")]
    public float radius = 5f;          // 원의 반지름
    public float orbitSpeed = 50f;     // 회전 속도(degrees/sec)
    public float height = 4f;          // 상단 y 위치

    [Header("총알 설정")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float StartSpeed = 1f;
    public float EndSpeed = 3f;
    public float Duration = 1f;
    private float _speed;

    [Header("보스 체력")]
    public int maxHealth = 5000;
    private float currentHealth;

    private float angle = 0f;          // 현재 각도
    private float fireTimer = 0f;

    private Animator _animator;

    private void Awake()
    {
        currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        OrbitMovement();
        HandleShooting();
        ShootZigZagBullet();
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

        if (firePoint == null || Player == null)
        {
            return;
        }

        BulletFactory.Instance.MakeZigZagBullet();
    }

    // 보스 데미지 함수
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        _animator.SetTrigger("BossHit");

        if (currentHealth <= 0)
        {
            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFXEnemyExplosion);
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("보스 몬스터 사망!");
        Destroy(gameObject);
    }
}
