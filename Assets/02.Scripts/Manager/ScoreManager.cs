using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 응집도를 높혀라
    // 응집도 : '데이터'와 '데이터를 조작하는 로직'이 얼마나 잘 모여있냐
    // 응집도를 높이고, 필요한 것만 외부에 공개하는 것을 '캡슐화'

    [SerializeField] private Text _currentScoreTextUI;
    private int _currentScore = 0;

    private void Start()
    {
        Refesh();
    }

    public void AddScore(int score)
    {
        if (score < 0) return;
        _currentScore += score;

        Refesh();
    }

    private void Refesh()
    {
        _currentScoreTextUI.text = $"현재 점수: {_currentScore}";
    }
}
