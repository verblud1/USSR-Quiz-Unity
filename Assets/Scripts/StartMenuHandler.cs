using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class StartMenuHandler : MonoBehaviour
{
    [SerializeField] private Transform ButtonsListUI;

    void Start()
    {
        //update rigth count

        for (int i = 0; i < ButtonsListUI.childCount; i++)
        {
            Transform buttonTransform = ButtonsListUI.GetChild(i);
            GameObject button = buttonTransform.gameObject;
            Transform buttonTextTransform = buttonTransform.GetChild(1).GetChild(1).GetChild(1).GetChild(0);

            TextMeshProUGUI textComponent = buttonTextTransform.GetComponentInChildren<TextMeshProUGUI>();

            if (textComponent != null)
            {
                if(i == 0)
                    textComponent.text = PlayerPrefs.GetInt("easy_correct").ToString();
                if(i == 1)
                    textComponent.text = PlayerPrefs.GetInt("medium_correct").ToString();
                if (i == 2)
                    textComponent.text = PlayerPrefs.GetInt("hard_correct").ToString();
            }

        }
    }


    public void ButtonLevelClick(string level)
    {

        if (level == "easy")
        {
            PlayerPrefs.SetString("SelectedLevelName", "easy"); 
        }
        if (level == "medium")
        {
            PlayerPrefs.SetString("SelectedLevelName", "medium");
        }
        if (level == "hard")
        {
            PlayerPrefs.SetString("SelectedLevelName", "hard");
        }

        YG2.InterstitialAdvShow();
        SceneManager.LoadScene("GameScene");
    }
}
