using NUnit.Framework;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누르면 총알을 만들어서 발사하고 싶다.

    [Header("총구")]
    public Transform FirePosition;
    public float FireOffset = 0.3f;
    public Transform SubFirePositionLeft;
    public Transform SubFirePositionRight;

    [Header("쿨타임")]
    public float CoolTime = 0.6f;
    private float _coolTimer;

    [Header("자동모드")]
    public bool AutoMode = false;

    [Header("사운드")]
    public AudioSource FireSound;

  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) AutoMode = true;
        if (Input.GetKeyDown(KeyCode.Alpha2)) AutoMode = false;


        _coolTimer -= Time.deltaTime;
        if (_coolTimer > 0) return;     // 조기 리턴

        // 1. 발사 버튼을 누르고 있거나 (혹은) or == || 자동 모드라면...
        if (Input.GetKey(KeyCode.Space) || AutoMode)
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (_coolTimer > 0) return;     // 조기 리턴

        FireSound.Play();

        // 발사하고 나면 쿨타이머를 초기화
        _coolTimer = CoolTime;

        // 유니티에서 게임 오브젝트를 생성할때는 new가 instaintate 라는 메서드를 이용한다.
        // 클래스 -> 객체(속성+기능) -> 메모리에 로드된 객체를 인스턴스
        //                        ㄴ 인스턴스화

        // 메인 총알 생성
        MakeBullets();

        // 보조 총알 생성
        MakeSubBullets();
    }
    public void SpeedUp (float value)
    {
        CoolTime = Mathf.Max(0.05f, CoolTime - value);
    }

    // 기획은 다같이
    // 총알
    // 플레이어가 총알 생성(PlayerFire)
    // 적이 총알 생성(EnemyFire, Enemy, EnemyController)
    // 펫도 총알 생성 (PetFire, Pet, PetController)

    private void MakeBullets()
    {
        BulletFactory bulletFactory = GameObject.Find("BulletFactory").GetComponent<BulletFactory>();
        bulletFactory.MakeBullet(position: FirePosition.position + new Vector3(-FireOffset, 0, 0));
        bulletFactory.MakeBullet(position: FirePosition.position + new Vector3(FireOffset, 0, 0));
        
        Vector3 leftPos = FirePosition.position + Vector3.left * FireOffset;
        Vector3 rightPos = FirePosition.position + Vector3.right * FireOffset;

        BulletFactory.Instance.MakeBullet(leftPos);
        BulletFactory.Instance.MakeBullet(rightPos);

    }

    private void MakeSubBullets()
    {
        BulletFactory bulletFactory = GameObject.Find("BulletFactory").GetComponent<BulletFactory>();
        bulletFactory.MakeSubBullet(SubFirePositionLeft.position);
        bulletFactory.MakeSubBullet(SubFirePositionRight.position);
    }
}