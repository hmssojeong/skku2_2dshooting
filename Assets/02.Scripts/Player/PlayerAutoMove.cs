using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    private Player _player;
    private Animator _animator;

    private void Start()
    {
        _player = gameObject.GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
    }

    public void Execute()
    {
        // 1.  모든 적을 찾는다.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies == null || enemies.Length == 0)
        {
            // 적이없다면 움직이지 않는다.
            _animator.Play("Idle");
            return;
        }

        // 2. 가장 가까운 적을 찾는다.
        GameObject closestEnemy = enemies[0];
        float closestDistance = Vector2.Distance(transform.position, enemies[0].transform.position);
        for (int i = 1; i < enemies.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, enemies[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemies[i];
            }
        }

        Vector2 enemyPosition = closestEnemy.transform.position;

        Vector2 direction = Vector2.zero;
        // 3. 왼쪽이면 왼쪽
        if (enemyPosition.x < transform.position.x)
        {
            direction.x = -1;
            _animator.Play("Left");
        }
        // 4. 오른쪽이면 오른쪽이동
        else
        {
            direction.x = 1;
            _animator.Play("Right");
        }

        transform.Translate(direction * _player.Speed * Time.deltaTime);

        // 5. 적이 없다면 origin이동
    }


    // 1. 분기문 나누기
    // 2. 클래스 나누기
    // 3. 컴포넌트 나누기
    // 4. 상태 패턴 
}