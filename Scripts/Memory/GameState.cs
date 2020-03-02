/* 
 * @author Garnier Gael
 * @date 17/10/2018
 * @file GameState.cs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    static GameState()
    {
        KillCount = 0;
        CurrentScore = 0;
        CurrentCombo = 0;
        CurrentMultiplier = 1;
        PlayerLife = 1;
        DefaultScoreMultiplier = 1;
    }

    public static int KillCount { get; set; }

    public static int CurrentScore { get; set; }

    public static int CurrentCombo { get; set; }

    public static int CurrentMultiplier { get; set; }

    public static int PlayerLife { get; set; }

    public static int DefaultScoreMultiplier { get; private set; }

    public static void ResetDatas()
    {
        KillCount = 0;
        CurrentScore = 0;
        CurrentCombo = 0;
        CurrentMultiplier = 1;
        PlayerLife = 1;
        DefaultScoreMultiplier = 1;

        StateMemory.IsArmorEquipped = false;
        StateMemory.IsSpecialEquipped = false;
    }

}
