using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    private TrailRenderer trail;

    void Start()
    {
        // Trail Renderer 추가
        trail = gameObject.AddComponent<TrailRenderer>();

        // 기본 설정
        trail.time = 1.0f; // 꼬리가 사라지기까지 걸리는 시간
        trail.startWidth = 0.3f;
        trail.endWidth = 0.0f;
        trail.minVertexDistance = 0.1f;

        // 색상 설정 (시작 → 끝)
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.cyan, 0.0f),
                new GradientColorKey(Color.white, 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(0.0f, 1.0f)
            }
        );
        trail.colorGradient = gradient;

        // 머티리얼 설정 (Glow 느낌)
        trail.material = new Material(Shader.Find("Sprites/Default"));
    }
}
