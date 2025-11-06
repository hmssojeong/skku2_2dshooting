using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // 역할: 충돌하는 모든 게임 오브젝트를 파괴한다.

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
