using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isAutoMode = false;

    private float _health = 3;

    public void Hit(float damage)
    {
        _health -= damage;
       
        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }

    public void Heal(float Value)
    {
        _health += Value;
    }
}