/* 
 * @author Garnier Gael
 * @date 15/10/2018
 * @file PowerUp.cs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private TextMesh mesh; // Score display
    private int currentScore = 0; // Current player's score
    private int multiplier = 1;

    public void AddScore(int value)
    {
        currentScore += (value * multiplier);
    }
}
