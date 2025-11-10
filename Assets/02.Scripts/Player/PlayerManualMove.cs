using Unity.VisualScripting;
using UnityEngine;

// 플레이어 이동
public class PlayerManualMove : MonoBehaviour
{
    public float ShiftSpeed = 1.2f;

    [Header("시작위치")]
    private Vector2 _originPosition;
    private Animator _animator;


    [Header("이동범위")]
    public float MinX = -2;
    public float MaxX = 2;
    public float MinY = -5;
    public float MaxY = 0;

    private Player _player;

    private void Start()
    {
        // 캐싱
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();

        // 처음 시작 위치 저장
        _originPosition = transform.position;
    }



    public void Execute()
    {
        float finalSpeed = _player.Speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            finalSpeed = finalSpeed * ShiftSpeed;
        }

        if (Input.GetKey(KeyCode.R))
        {
            TranslateToOrigin(finalSpeed);
            return;
        }


        float h = Input.GetAxisRaw("Horizontal"); // 수평 입력에 대한 값을 -1, 0, 1로 가져온다.
        float v = Input.GetAxisRaw("Vertical");   // 수직 입력에 대한 값을 -1, 0, 1로 가져온다.


        Vector2 direction = new Vector2(h, v);
        direction.Normalize();


        // 첫 번째 방식: Play 메서드를 이용한 강제 적용
        //if(direction.x < 0)  _animator.Play("Left");
        //if(direction.x == 0) _animator.Play("Idle");
        //if(direction.x > 0)  _animator.Play("Right");
        // 장점: 빠르게 쓰기 편하다.
        // 단점: Transition, Timing, State가 무시되고, 남용하기 쉬어서 어디서 애니메이션을 수정하는지 알 수 없어지게된다.


        // 두 번째 방식:
        _animator.SetInteger("x", (int)direction.x);



        Vector2 position = transform.position; // 현재 위치
        Vector2 newPosition = position + direction * finalSpeed * Time.deltaTime;  // 새로운 위치


        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);


        transform.position = newPosition;         // 새로운 위치로 갱신
    }

    private void TranslateToOrigin(float speed)
    {
        // 방향을 구한다.
        Vector2 direction = _originPosition - (Vector2)transform.position;

        // 이동을 한다.
        transform.Translate(direction * speed * Time.deltaTime);
    }


}