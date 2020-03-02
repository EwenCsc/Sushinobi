using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject pausePopup = null;
    [SerializeField] private GameObject deathPopup = null;

    void Start ()
    {
        pausePopup.SetActive(false);
        deathPopup.SetActive(false);
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

    public void SwitchSound()
    {
        AudioManager.Instance.SwitchMuteSound();
    }

    public void SwitchMusic()
    {
        AudioManager.Instance.SwitchMuteMusic();
    }
}
