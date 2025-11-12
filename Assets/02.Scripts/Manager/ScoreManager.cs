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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            TestSave();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            TestLoad();
        }
    }
    private void TestSave()
    {
        // 유니티에서는 값을 저장할때 'PlayerPrefs' 모듈을 씁니다.
        // 저장 가능한 자료형은: int, float, string
        // 저장을 할때는 저장할 이름(key)과 값(value) 이 두 형태로 저장을 해요.
        // 저장: Set
        // 로드: Get

        PlayerPrefs.SetInt("age", 19);
        PlayerPrefs.SetString("name", "김홍일");
        Debug.Log("저장됐습니다");
    }

    private void TestLoad()
    {
        int age = 17;
        if (PlayerPrefs.HasKey("age"))                               //검사
        {
            age = PlayerPrefs.GetInt(key: "age");
        }
        
        string name = PlayerPrefs.GetString(key: "name", "티모");   //default 인자


        Debug.Log($"{name}: {age}");
    }

}
