using UnityEngine;

// 플레이어는 플레이어 관련 스탯을 중앙관리하고, 컴포넌트도 관리한다.
public class Player : MonoBehaviour
{
    private PlayerManualMove _playerManualMove;
    private PlayerAutoMove _playerAutoMove;

    private PlayerSpecialSkill _specialSkillBoom;

    private bool _autoMode = false;
    private float _health = 3;

    public float Speed = 3;
    
    private bool _specialSkill = false;

    private void Start()
    {
        _playerAutoMove = GetComponent<PlayerAutoMove>();
        _playerManualMove = GetComponent<PlayerManualMove>();
        _specialSkillBoom = GetComponent<PlayerSpecialSkill>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) _autoMode = true;
        if (Input.GetKeyDown(KeyCode.Alpha2)) _autoMode = false;

        if (Input.GetKeyDown(KeyCode.Alpha3)) _specialSkill = true;

        if (_autoMode)
        {
            _playerAutoMove.Execute();
        }
        else
        {
            _playerManualMove.Execute();
        }

        if(_specialSkill)
        {
            _specialSkillBoom.Execute();
            _specialSkill = false;
        }
    }


    public void Hit(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(float value)
    {
        _health += value;
    }

}