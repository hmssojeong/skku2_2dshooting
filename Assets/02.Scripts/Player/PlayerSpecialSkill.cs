using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerSpecialSkill : MonoBehaviour
{
    [Header("필살기 공격력")]
    public float Damage;

    //private float _timer = 0;
    public float Duration = 1f;

    [Header("붐 프리팹")]
    public GameObject BoomPrefab;

    public GameObject SpecialSkillBoom;

    // Update is called once per frame
    public void Execute()
    {  
        GameObject specialSkillBoom = Instantiate(SpecialSkillBoom, transform.position, Quaternion.identity);

        specialSkillBoom.transform.position = Vector2.zero;

        Destroy(specialSkillBoom, Duration);     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Enemy와만 충돌 이벤트를 처리한다.
        if (!other.CompareTag("Enemy")) return;

        // GetComponent는 게임오브젝트에 붙어있는 컴포넌트를 가져올수있다. 
        Enemy enemy = other.GetComponent<Enemy>();
        
        MakeSpecialSkillBoom();
        // 객체간의 상호 작용을 할때 : 묻지말고 시켜라(디미터의 법칙)
        enemy.Hit(Damage);

    }

    private void MakeSpecialSkillBoom()
    {
        Instantiate(BoomPrefab,transform.position, Quaternion.identity);
    }
}
