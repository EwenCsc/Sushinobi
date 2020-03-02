using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject pausePopup = null;
    [SerializeField] private Text moneyText = null;
    [SerializeField] private GameObject energie = null;

    private void Start()
    {
        moneyText.text = StateMemory.PlayerMoney.ToString();
    }

    public void Pause()
    {
        if (pausePopup.activeSelf)
        {
            Time.timeScale = 1f;
            pausePopup.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            pausePopup.SetActive(true);
        }
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1.0f;
        GameState.ResetDatas();
        AudioManager.Instance.PlaySound("UIClick");
        SceneManager.LoadScene("LoadMenu");
    }

    public void SwitchSound()
    {
        AudioManager.Instance.SwitchMuteSound();
    }

    public void SwitchMusic()
    {
        AudioManager.Instance.SwitchMuteMusic();
    }


}
