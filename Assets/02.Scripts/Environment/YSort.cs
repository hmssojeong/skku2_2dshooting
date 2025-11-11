using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    private SpriteRenderer _sr;
    void Awake() => _sr = GetComponent<SpriteRenderer>();
    void LateUpdate()
    {
        _sr.sortingOrder = -(int)(transform.position.y * 100);
    }
}