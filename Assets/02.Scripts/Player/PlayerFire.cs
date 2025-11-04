using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누르면 총알을 만들어서 발사하고 싶다.

    // 필요 속성
    [Header("총알 프리팹")] // 복사해올 총알 프리팹 게임 오브젝트
    public GameObject BulletPrefab;

    [Header("총구")]
    public Transform FirePosition;

    public float FireOffset = 0.3f;
    public float FireCoolTime = 0.6f;
    public float FireTime = 0;

    // 자동 공격 모드 여부
    public bool AutoFire = true;

    private void Update()
    {
        // 1. 발사 버튼을 누르고 있으면..
        if(Input.GetKey(KeyCode.Space) && Time.time >= FireTime)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                AutoFire = true;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                AutoFire = false;
            }

            // 다음 발사 시간을 지금 시간 + 쿨타임으로 설정
            FireTime = Time.time + FireCoolTime;
            // -> Time.time: 게임 시작 후 경과된 총 시간입니다.

            // 유니티에서 게임 오브젝트를 생성할때는 new가 instaintate 라는 메서드를 이용한다.
            // 클래스 -> 객체(속성+기능) -> 메모리에 로드된 객체를 인스턴스
            //                           ㄴ 인스턴스화

            //GameObject bullet = new BulletPrefab(); -> GameObject bullet = Instantiate(BulletPrefab);
            // 2. 프리팹으로부터 총알(게임 오브젝트)을 생성한다.
            GameObject bullet1 = Instantiate(BulletPrefab);
            GameObject bullet2 = Instantiate(BulletPrefab);

            // 3. 생성된 총알의 위치를 총구의 위치로 옮긴다.
            bullet1.transform.position = FirePosition.position + new Vector3(-FireOffset, 0, 0); // 생성 후 총구의 위치로 수정
            bullet2.transform.position = FirePosition.position + new Vector3(FireOffset, 0, 0);
        }
    }
}
