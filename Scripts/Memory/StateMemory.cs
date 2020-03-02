using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class StateMemory
{
    public struct WeaponType
    {
        public string m_name;
        public int m_price;
    }

    public struct ArmorType
    {
        public string m_name;
        public int m_price;
    }

    private static List<WeaponType> specialTypes = new List<WeaponType> { new WeaponType { m_name = "Wasabi", m_price = 100 }, new WeaponType { m_name = "Sword", m_price = 200 }, new WeaponType { m_name = "Greatsword", m_price = 300 } };
    public static List<WeaponType> SpecialTypes { get { return specialTypes; } }

    private static List<ArmorType> armorTypes = new List<ArmorType> { new ArmorType { m_name = "Cloth", m_price = 50 }, new ArmorType { m_name = "Mail", m_price = 100 }, new ArmorType { m_name = "Plate", m_price = 150 } };
    public static List<ArmorType> ArmorTypes { get { return armorTypes; } }

    private static int specialLevel = 0;
    private static int armorLevel = 0;

    private static bool isArmorEquipped = false;
    private static bool isSpecialEquipped = false;

    private static int armorPrice = 50;
    private static int specialPrice = 100;

    private static int currentBestScore = 0;
    private static List<int> bestScores = new List<int>();

    private static bool hasSeenTuto = false;

    private static readonly string bestScoresFilePath = Application.persistentDataPath + "/bestScores.dat";

    public static bool canDie = true;

    public static List<int> GetBestScoresFromFile()
    {
        StreamReader reader;
        List<int> scores = new List<int>();

        if (File.Exists(bestScoresFilePath))
        {
            reader = new StreamReader(bestScoresFilePath, System.Text.Encoding.UTF8);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (line != "") // In case of a blank line
                {
                    scores.Add(int.Parse(line));
                }
            }

            reader.Close();
        }

        return scores;
    }

    public static void WriteBestScoresInFile()
    {
        if (!File.Exists(bestScoresFilePath))
        {
            // StreamWriter tmp dédié à la création du fichier => règle bug Sharing violation
            using (StreamWriter sw = File.CreateText(bestScoresFilePath))
            {
                sw.Close();
            }
        }

        StreamWriter writer = new StreamWriter(bestScoresFilePath, false);

        foreach (int score in bestScores)
        {
            writer.WriteLine(score.ToString());
        }

        writer.Close();
    }

    public static void ResetBestScores()
    {
        bestScores = GetBestScoresFromFile();

        for (int i = 0; i < bestScores.Count; i++)
        {
            bestScores[i] = 0;
        }

        WriteBestScoresInFile();
    }

    /// <summary>
    /// Persistant money save
    /// </summary>
    public static int PlayerMoney
    {
        get
        {
            return PlayerPrefs.GetInt("SushiMoney");
        }
        set
        {
            PlayerPrefs.SetInt("SushiMoney", value);
        }
    }

    public static int ArmorPrice
    {
        get
        {
            return armorPrice;
        }
        set
        {
            armorPrice = value;
        }
    }
    public static int SpecialPrice
    {
        get
        {
            return specialPrice;
        }
        set
        {
            specialPrice = value;
        }
    }

    public static bool IsArmorEquipped
    {
        get
        {
            return isArmorEquipped;
        }
        set
        {
            isArmorEquipped = value;
        }
    }
    public static bool IsSpecialEquipped
    {
        get
        {
            return isSpecialEquipped;
        }
        set
        {
            isSpecialEquipped = value;
        }
    }

    /// Gestion des scores
    public static int CurrentBestScore
    {
        get
        {
            return currentBestScore;
        }

        set
        {
            currentBestScore = value;
        }
    }

    public static List<int> BestScores
    {
        get
        {
            if (bestScores == null)
            {
                bestScores = GetBestScoresFromFile();
                bestScores.Sort();
                bestScores.Reverse();
            }

            return bestScores;
        }

        set
        {
            bestScores = value;
        }
    }

    public static bool HasSeenTuto
    {
        get
        {
            return hasSeenTuto;
        }

        set
        {
            hasSeenTuto = value;
        }
    }

    public static int SpecialLevel
    {
        get
        {
            return PlayerPrefs.GetInt("SushiSpecialLevel");
        }
        set
        {
            PlayerPrefs.SetInt("SushiSpecialLevel", value);
        }
    }

    public static int ArmorLevel
    {
        get
        {
            return PlayerPrefs.GetInt("SushiArmorLevel");
        }
        set
        {
            PlayerPrefs.SetInt("SushiArmorLevel", value);
        }
    }
}
