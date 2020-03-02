/*
/*
 * @author Gaël
 * @date 28/11/2018
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Text[] bestScoresHolders = null;

    // Use this for initialization
    void Start()
    {
        List<int> scores = StateMemory.GetBestScoresFromFile();

        // Add current score in list
        scores.Add(GameState.CurrentScore);

        // Sort List
        scores.Sort(); // Sort by Desc
        scores.Reverse(); // Order by Asc

        // Remove worst scores
        if (scores.Count > 5)
        {
            while (scores.Count > 5)
            {
                scores.RemoveAt(scores.Count - 1);
            }
        }

        // Save scores in file
        StateMemory.BestScores = scores;
        StateMemory.WriteBestScoresInFile();

        // Changing the music
        AudioManager.Instance.PlayMusic("Death", false);

        // Saving money into file
        GainMoney();

        // Print the bests scores on screen
        if (StateMemory.BestScores.Count > 0)
        {
            for (int i = 0; i < StateMemory.BestScores.Count; i++)
            {
                bestScoresHolders[i].gameObject.SetActive(true);
                bestScoresHolders[i].text = (i + 1) + " score : " + StateMemory.BestScores[i].ToString();
            }
        }
    }

    /// <summary>
    /// Method to call onClick for BackToMenu button
    /// </summary>
    public void BackToMenu()
    {
        GameState.ResetDatas();
        SceneManager.LoadScene("LoadMenu");
    }

    /// <summary>
    /// Save the new player's money amount
    /// </summary>
    /// <returns>Int the amount of money added</returns>
    private void GainMoney()
    {
        StateMemory.PlayerMoney += GameState.CurrentScore / 100;
    }
}
