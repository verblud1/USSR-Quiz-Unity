using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class WarningPanel : MonoBehaviour
{
    [SerializeField] private Transform warningPanel;

    public void OpenPanel()
    {
        warningPanel.gameObject.SetActive(true);
    }
    public void ToBack()
    {
        warningPanel.gameObject.SetActive(false);
    }
    public void ToMenu()
    {
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene("StartScene");
    }

}
