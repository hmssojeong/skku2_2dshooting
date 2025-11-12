using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 응집도를 높혀라
    // 응집도 : '데이터'와 '데이터를 조작하는 로직'이 얼마나 잘 모여있냐
    // 응집도를 높이고, 필요한 것만 외부에 공개하는 것을 '캡슐화'

    [SerializeField] private Text _currentScoreTextUI;
    [SerializeField] private Text _bestScoreTextUI;
    private int _currentScore = 0;
    private int _bestScore = 0;

    private const string CurrentScoreKey = "CurrentScore";
    private const string BestScoreKey = "BestScore";

    private void Start()
    {
        Load();
        _currentScore = 0;
        Refesh();
        Refeshbest();

    }

    public void AddScore(int score)
    {
        if (score < 0) return;
        _currentScore += score;

        Refesh();

        Save();
    }

    public void BestScore()
    {
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }

        Refeshbest();

        Savebest();
    }

    // 1. 하나의 메서드는 한가지 일만 잘 하면된다.

    private void Refesh()
    {
        _currentScoreTextUI.text = $"현재 점수: {_currentScore:N0}";
    }

    private void Refeshbest()
    {
        _bestScoreTextUI.text = $"최고 점수: {_bestScore:N0}";
    }

    private void Save()
    {
        PlayerPrefs.SetInt(CurrentScoreKey, _currentScore);
    }

    private void Savebest()
    {
        PlayerPrefs.SetInt(BestScoreKey, _bestScore);
    }

    private void Load()
    {
        _currentScore = PlayerPrefs.GetInt(CurrentScoreKey, 0);
        _bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
    }

}
