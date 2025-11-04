using UnityEngine;

public class PracticePlayer : MonoBehaviour
{
    public GameObject BulletPrefab;

    public Transform FirePosition;
    public float FireOffset = 0.3f;
    public float CoolTime = 0.6f;
    private float _coolTimer; // 타이머용 변수

    private bool _isAutoFire = true;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            _isAutoFire = true;
        if(Input.GetKeyDown(KeyCode.Alpha2))
            _isAutoFire = false;

        _coolTimer = _coolTimer - Time.deltaTime;
        if (_coolTimer > 0) return; // 쿨타이머가 0보다 크면 총알 발사를 무시한다. 함수 실행을 중단하고 바로 끝내는 명령.

        if(_isAutoFire)
        {
            _coolTimer = CoolTime;

            Fire();
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _coolTimer = CoolTime; // 쿨타임이 끝나서 총알을 발사하면, 다시 _coolTimer를 0.6초로 초기화해서 다음 총알을 바로 못 쏘게 만드는 것.

                Fire();
            }
        }
    }

    private void Fire()
    {
        GameObject bullet1 = Instantiate(BulletPrefab);
        GameObject bullet2 = Instantiate(BulletPrefab);

        bullet1.transform.position = FirePosition.position + new Vector3(-FireOffset, 0, 0);
        bullet2.transform.position = FirePosition.position + new Vector3(FireOffset, 0, 0);


    }
}
