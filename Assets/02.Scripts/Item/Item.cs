using UnityEngine;

public class Item : MonoBehaviour
{
    // 충돌 트리거가 일어났을 때
    // 만약 플레이어 태그라면
    // 플레이어 게임오브젝트의 플레이어무브 컴포넌트를 읽어온다.
    // 스피드를 +Value 해준다.
    // 나를 삭제

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        PlayerMove playerMove = other.GetComponent<PlayerMove>();
        playerMove.SpeedUp(1);

        Destroy(this.gameObject);
    }
}
