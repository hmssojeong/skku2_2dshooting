using System.Transactions;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("스탯")]
    public float Speed;
    public float Health = 100f;

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.down; // 아래 방향으로
        transform.Translate(translation: direction * (Speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //몬스터는 플레이어와만 충돌처리할 것이다.
        //if(other.gameObject.tag == "Player") //name으로 하면 누군가가 이름을 다른것으로 바꿀경우 문제가 생기기때문에 tag로 구분하는게 좋다.
        if (!other.gameObject.CompareTag("Player")) return;
        {
            Destroy(gameObject);       // 나 죽고,
            Destroy(other.gameObject); // 너 죽자 (플레이어, 총알)
        }
    }

}
