using UnityEngine;

public class Follower : MonoBehaviour
{

    public float CurShotDelay;
    public float MaxShotDelay;
    public PlayerFire playerfire;

    [Header("총알 프리팹")]
    public GameObject BulletPrefab;

    public GameObject bullet;
    public GameObject BulletFollowerPrefab;
    private void Update()
    {
        Follow();
        Fire();
        Reload();
    }

    private void Follow()
    {

    }

    public void Fire()
    {
        if (CurShotDelay < MaxShotDelay)
            return;

        GameObject bullet3 = Instantiate(BulletPrefab);
        bullet3.transform.position = transform.position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up*10, ForceMode2D.Impulse);

        CurShotDelay = 0;

    }

    private void Reload()
    {
        CurShotDelay += Time.deltaTime;
    }
}
