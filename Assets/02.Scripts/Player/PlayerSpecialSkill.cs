using UnityEngine;

public class PlayerSpecialSkill : MonoBehaviour
{
    [Header("필살기 지속시간")]
    public float Duration = 3f;

    [Header("필살기 프리팹")]
    public GameObject SpecialSkillBoomPrefab;

    public void Execute()
    {
        GameObject boom = Instantiate(SpecialSkillBoomPrefab, Vector2.zero, Quaternion.identity);
        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFXSpecialSkill);
        Destroy(boom, Duration);
    }
}