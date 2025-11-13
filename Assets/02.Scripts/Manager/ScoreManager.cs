using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 단 하나여야 한다.
    // 전역적인 접근점을 제공해야 한다.
    // 게임 개발에서는 Manager(관리자) 클래스를 보통 싱글톤 패턴으로 사용하는 것이 관행이다.
    private static ScoreManager _instance = null;

    public static ScoreManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }

    public Animator ScoreAnimator;

    // 응집도를 높혀라
    // 응집도 : '데이터'와 '데이터를 조작하는 로직'이 얼마나 잘 모여있냐
    // 응집도를 높이고, 필요한 것만 외부에 공개하는 것을 '캡슐화'

    [SerializeField] private Text _currentScoreTextUI;
    [SerializeField] private Text _bestScoreTextUI;
    private int _currentScore = 0;
    private int _bestScore = 0;
    private Vector3 _originalScale;
    

    private const string CurrentScoreKey = "CurrentScore";
    private const string BestScoreKey = "BestScore";

    private void Start()
    {
        _originalScale = _currentScoreTextUI.transform.localScale;
        Load();
        _currentScore = 0;
        
        Refresh();
        Refreshbest();

    }

    public void AddScore(int score)
    {
        if (score < 0) return;
        _currentScore += score;

        AnimateScore();

        Refresh();

        Save();
    }

    public void BestScore()
    {
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }

        Refreshbest();

        Savebest();
    }

    // 1. 하나의 메서드는 한가지 일만 잘 하면된다.

    private void Refresh()
    {
        _currentScoreTextUI.text = $"현재 점수: {_currentScore:N0}";
    }

    private void Refreshbest()
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

    public void AnimateScore()
    {

        // 1.5배로 커졌다가 0.5초만에 원래 크기로 돌아오기
        _currentScoreTextUI.transform.DOScale(_originalScale * 1.5f, 0.25f)
            .OnComplete(() =>
                _currentScoreTextUI.transform.DOScale(_originalScale, 0.25f)
            );
    }
}
