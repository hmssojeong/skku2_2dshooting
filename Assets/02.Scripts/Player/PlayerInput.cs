using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        // Player 컴포넌트 찾아서 캐싱
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _player.SetAutoMode(true);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            _player.SetAutoMode(false);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            _player.UseSpecialSkill();
    }
}
