using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerSpecialSkill : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Player _player;

    [Header("필살기 공격력")]
    public float Damage;

    //private float _timer = 0;
    public float CoolTime = 1f;

    [Header("붐 프리팹")]
    public GameObject BoomPrefab;

    public GameObject SpecialSkillBoom;

    private void Start()
    {
        
    }

    // Update is called once per frame
    public void Execute()
    {  //3초동안 시간이 흐른다.
        //3초동안 무적으로 살아있는다.
        //닿는 모든 에너미를 한방에 죽인다.
        //애니메이션 넣기
        GameObject specialSkillBoom = Instantiate(SpecialSkillBoom, transform.position, Quaternion.identity);

        specialSkillBoom.transform.position = Vector2.zero;
        //_timer += Time.deltaTime;

        //if (_timer>=CoolTime)
        //{ 
            Destroy(specialSkillBoom, CoolTime);
        //}
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Enemy와만 충돌 이벤트를 처리한다.
        if (other.CompareTag("Enemy") == false) return;

        // GetComponent는 게임오브젝트에 붙어있는 컴포넌트를 가져올수있다. 
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        
        MakeSpecialSkillBoom();
        // 객체간의 상호 작용을 할때 : 묻지말고 시켜라(디미터의 법칙)
        enemy.Hit(Damage);

    }

    private void MakeSpecialSkillBoom()
    {
        Instantiate(BoomPrefab,transform.position, Quaternion.identity);
    }
}
