using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// 플레이어 이동
public class PlayerMove : MonoBehaviour
{
    // 목표
    // "키보드 입력"에 따라 "방향"을 구하고 그 방향으로 이동시키고 싶다.

    // 구현 순서:
    // 1. 키보드 입력
    // 2. 방향 구하는 방법
    // 3. 이동

    private GameObject closestEnemy = null; // 가장 가까운 적

    // 필요 속성:
    [Header("능력치")]
    private float _speed = 3f;


    /*    public float MaxSpeed = 10;
        public float MinSpeed = 1;*/
    public float ShiftSpeed = 1.2f;

    [Header("시작위치")]
    private Vector2 _originPosition;


    [Header("이동범위")]
    public float MinX = -2;
    public float MaxX = 2;
    public float MinY = -5;
    public float MaxY = 0;

    private float _distance = 2f;

    [Header("자동전투 설정")]
    public bool AutoBattle = false;

    private void Start()
    {
        // 처음 시작 위치 저장
        _originPosition = transform.position;
    }

    public void SpeedUp(float value)
    {
        _speed += value;

        // _speed = Mathf.Min(_speed, MaxSpeed);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AutoBattle = !AutoBattle;
        }

            
            /*      if (Input.GetKeyDown(KeyCode.Q))
                    {
                        Speed++;
                    }
                    else if (Input.GetKey(KeyCode.E))
                    {
                        Speed--;
                    }

                    // 1 ~ 10
                    Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed); */

            float finalSpeed = _speed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // 1.2 ~ 12
                finalSpeed = finalSpeed * ShiftSpeed;
            }

            if (Input.GetKey(KeyCode.R))
            {
                // 원점으로 돌아간다.
                TranslateToOrigin(finalSpeed);
                return;
            }

        if (!AutoBattle)
        {
            HandleManualMove(finalSpeed);
        }
        else
        {
            HandleAutoBattleMove(finalSpeed);
        }
    }
        private void FindClosestEnemyByY() // 가까운적찾기
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // 적 배열 저장
            float minDeltaY = Mathf.Infinity; //무한대로 시작하기 때문에 첫번째 적갱신
            closestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float deltaY = Mathf.Abs(enemy.transform.position.y - transform.position.y);
                //플레이어와 적 사이의 y좌표차이 구하기

            // 플레이어와 적사이에 y좌표가 미니보다 작으면 그것은 작은 y좌표값
                if (deltaY < minDeltaY)
                {
                    minDeltaY = deltaY;
                    closestEnemy = enemy;
                }
            }
        }
    private void HandleManualMove(float speed)
    {

        // 1. 키보드 입력을 감지한다. 
        // 유니티에서는 Input이라고 하는 모듈이 입력에 관한 모든것을 담당하다.
        float h = Input.GetAxisRaw("Horizontal"); // 수평 입력에 대한 값을 -1, 0, 1로 가져온다.
        float v = Input.GetAxisRaw("Vertical");   // 수직 입력에 대한 값을 -1, 0, 1로 가져온다.


        // 2. 입력으로부터 방향을 구한다.
        // 벡터: 크기와 방향을 표현하는 물리 개념
        Vector2 direction = new Vector2(h, v);

        // 2-1. 방향을 크기 1로 만드는 정규화를 한다.
        direction.Normalize();
        // direction = direction.normalized;

        // Debug.Log($"direction: {direction.x}, {direction.y}");

        // 오른쪽        (1, 0)
        // 위쪽          0. 1)
        // 대각선위오른쪽 (1, 1)


        // 3. 그 방향으로 이동을한다.
        Vector2 position = transform.position; // 현재 위치


        // 쉬운 버전
        // transform.Translate(direction * Speed * Time.deltaTime);

        // 새로운 위치 = 현재 위치 + (방향 * 속력) * 시간
        // 새로운 위치 = 현재 위치 + 속도 * 시간
        //       새로운 위치 = 현재 위치  +  방향     *  속력   * 시간
        Vector2 newPosition = position + direction * speed * Time.deltaTime;  // 새로운 위치


        // Time.deltaTime: 이전 프레임으로부터 현재 프레임까지 시간이 얼마나 흘렀는지.. 나타내는 값
        //                 1초 / fps 값과 비슷하다.

        // 이동속도 : 10
        // 컴퓨터1 :  50FPS : Update -> 초당 50번  실행 -> 10 * 50  = 500   * Time.deltaTime = 두개의 값이 같아진다.
        // 컴퓨터2 : 100FPS : Update -> 초당 100번 실행 -> 10 * 100 = 1000  * Time.deltaTime

        // -1, 0, 1, 0.00000001 이 숫자 3개 말고는 다 매직넘버이므로 변수로 빼야된다.

        // 1. 포지션 값에 제한을 둔다.
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
    private void HandleAutoBattleMove(float speed) //자동전투
    {
        // 1. 매프레임 또는 일정 시간 마다 가장 가까운 적 찾는다.
        FindClosestEnemyByY();

        // 2. 가장 가까운 적이 있다면 해당 적의 X 위치로 플레이어 X위치를 이동.
        if (closestEnemy == null)
            return;
        
        
            float targetX = closestEnemy.transform.position.x; // 목표 X 위치
            float targetY = closestEnemy.transform.position.y;
            Vector2 currentPosition = transform.position; // 현재 플레이어 위치

            transform.position = Vector2.MoveTowards(currentPosition, new Vector2(targetX, targetY-_distance), _speed * Time.deltaTime);
        
    }
    private void TranslateToOrigin(float speed)
    {
        // 방향을 구한다.
        Vector2 direction = _originPosition - (Vector2)transform.position;

        // 이동을 한다.
        transform.Translate(direction * speed * Time.deltaTime);
    }


}