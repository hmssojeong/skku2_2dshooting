using UnityEngine;

// 플레이어 이동
public class PlayerMove : MonoBehaviour
{
    // 목표
    // "키보드 입력"에 따라 "방향"을 구하고 그 방향으로 이동시키고 싶다.

    // 구현 순서:
    // 1. 키보드 입력
    // 2. 방향 구하는 방법
    // 3. 이동

    // 필요 속성:
    [Header("능력치")]
    public float Speed = 3;
    public float MaxSpeed = 10;
    public float MinSpeed = 1;
    float ShiftSpeed = 2f;


    [Header("이동범위")]
    public float MinX = -2;
    public float MaxX = 2;
    public float MinY = -2;
    public float MaxY = 0;

    private void Start()
    {
       
    }


    // 게임 오브젝트가 게임을 시작 후 최대한 많이
    private void Update()
    {
        // 1. 키보드 입력을 감지한다.
        // 유니티에서는 Input이라고 하는 모듈이 입력에 관한 모든것을 담당하다.
        float h = Input.GetAxisRaw("Horizontal"); // 수평 입력에 대한 값을 -1 ~ 0 ~ 1로 가져온다.
        float v = Input.GetAxisRaw("Vertical");   // 수직 입력에 대한 값을 -1 ~ 0 ~ 1로 가져온다.

        Debug.Log($"h: {h}, v; {v}");

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed++;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Speed--;
        }

        // 1 ~ 10
        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);

        float finalspeed = Speed; // 지역 변수
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 1.2 ~ 12
            finalspeed = finalspeed * ShiftSpeed;
        }
 

        if (Input.GetKey(KeyCode.R))
        {
            Vector2 target = Vector2.zero; // 원점
            transform.position = Vector2.MoveTowards(transform.position, target, finalspeed * Time.deltaTime);
        }


        // 2. 입력으로부터 방향을 구한다.
        // 벡터: 크기와 방향을 표현하는 물리 개념
        Vector2 direction = new Vector2(h, v);

        // 2-1. 방향을 크기 1로 만드는 정규화를 한다.
        direction.Normalize();
        // direction = direction.normalized; // 위의 한줄과 동일한 기능

        Debug.Log($"direction: {direction.x}, {direction.y}");

        // 3. 그 방향으로 이동을 한다.
        Vector2 position = transform.position;  // 현재 위치

        // 쉬운버전 ->대신에 정밀한 제어가 어렵다.
        // transform.Translate(translation: direction * Speed * Time.deltaTime);

        // 새로운 위치 = 현재 위치 + (방향 * 속력) * 시간
        // 새로운 위치 = 현재 위치 + 속도 * 시간

        //      새로운 위치   현재 위치    방향      속력
        Vector2 newPosition = position + direction * finalspeed * Time.deltaTime;    // 새로운 위치

        // Time.deltaTime: 이전 프레임으로부터 현재 프레임까지 시간이 얼마나 흘렀는지.. 나타내는 값
        //                 1초  /  fps 값과 비슷하다.

        // 이동속도 : 10
        // 컴퓨터1 :  50FPS : Update -> 초당 50번  실행 -> 10 * 50  = 500   * Time.deltaTime  = 두개의 값이 같아진다.
        // 컴퓨터2 : 100FPS : Update -> 초당 100번 실행 -> 10 * 100 = 1000  * Time.deltaTime
        // -> 밸런스를 필요로하는 곳에 다 해줘야한다.(이동속도, 회전, 확대축소 등등)
        // 컴퓨터 성능에 따라 같은 캐릭터라도 이동속도가 달라질 수 있어서 Time.deltaTime를 곱해줌으로써 똑같이 갈 수 있도록 해준다.


        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }

        if (newPosition.y < MinY)
        {
            newPosition.y = MinY;
        }
        else if (newPosition.y > MaxY)
        {
            newPosition.y = MaxY;

        }

        transform.position = newPosition; // 새로운 위치로 갱신
    }
}
