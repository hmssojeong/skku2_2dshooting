using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    [Header("필살기 공격력")]
    public float Damage = 100f;

    [Header("폭발 이펙트 프리팹")]
    public GameObject ExplosionPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.TryGetComponent<Enemy>(out var enemy))
        {
            // '묻지 말고 시켜라' 원칙에 따라 Enemy에게 Hit를 시킵니다.
            enemy.Hit(Damage);

            // 폭발 이펙트를 생성합니다.
            if (ExplosionPrefab != null)
            {
                Instantiate(ExplosionPrefab, other.transform.position, Quaternion.identity);
            }
        }
    }
}