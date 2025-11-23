using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rightAnswerstext;
   // private string LevelName;

    void Start()
    {
       // LevelName = PlayerPrefs.GetString("SelectedLevelName");
        rightAnswerstext.text = PlayerPrefs.GetInt("CurrentValueCorrect").ToString(); 
    }

    public void ToMenu()
    {
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene("StartScene");
    }
    public void RestartGame()
    {
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene("GameScene");
    }
}
