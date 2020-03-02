using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPatternManager : MonoBehaviour
{
    private enum PatternType
    {
        Maki,
        Yakitori,
        Wall,
        Spikes,
        Boss,
        None
    }

    [SerializeField] private GameObject[] patternPrefab = new GameObject[0];

    [HideInInspector] public List<GameObject> inGamePattern = new List<GameObject>();
    [SerializeField] private float speed = 0.0f;

    //DEBUG
    [SerializeField] private float horizontalTimer = 0.0f;
    [SerializeField] private float verticalTimer = 0.0f;
    [SerializeField] private float dashTimer = 0.0f;
    [SerializeField] private float jumpTimer = 0.0f;
    [SerializeField] private float bossTimer = 0.0f;

    [SerializeField] private int minBossCount = 0;
    [SerializeField] private int maxBossCount = 0;

    private int makiChances = 35; //35%
    private int yakitoriChances = 70; //35%
    private int wallChances = 85; //15%
    private int spikeChances = 100; //15%

    private int bossChances = 0;
    private int bossCount = 0;

    private float spawnDelay = 0.0f;
    private PatternType lastSpawn = PatternType.None;

    private float difficulty = 2.0f;

    private void Start()
    {
        StartCoroutine(Spawn());
        StartCoroutine(IncreaseDifficulty());
        bossChances = Random.Range(minBossCount, maxBossCount);
    }

    private void Update()
    {
        CheckDeadPattern();

        MovePattern();
    }

    private PatternType GetRandomPattern()
    {
        PatternType type = PatternType.None;

        do
        {
            int random = Random.Range(0, 100);

            if (random < makiChances)
            {
                type = PatternType.Maki;

                makiChances -= 10;
                yakitoriChances = 70; //30%
            }
            else if (random < yakitoriChances)
            {
                type = PatternType.Yakitori;

                makiChances = 35; //30%
                yakitoriChances -= 10;
            }
            else if (random < wallChances)
            {
                type = PatternType.Wall;

                makiChances = 35; //30%
                yakitoriChances = 70; //30%
            }
            else if (random < spikeChances)
            {
                type = PatternType.Spikes;

                makiChances = 35; //30%
                yakitoriChances = 70; //30%
            }
        } while (type == lastSpawn);

        bossCount++;

        return type;
    }

    //Instantiate a new random barrier pattern at spawn position
    private void AddPattern()
    {
        if (patternPrefab.Length > 0)
        {
            PatternType type = GetRandomPattern();

            if (bossCount >= bossChances)
            {
                type = PatternType.Boss;
                bossChances = Random.Range(minBossCount, maxBossCount);
                bossCount = 0;
                spawnDelay = bossTimer;
            }
            else
            {
                switch (type)
                {
                    case PatternType.Maki:
                        spawnDelay = horizontalTimer;
                        lastSpawn = PatternType.None;
                        break;

                    case PatternType.Yakitori:
                        spawnDelay = verticalTimer;
                        lastSpawn = PatternType.None;
                        break;

                    case PatternType.Wall:
                        spawnDelay = dashTimer;
                        lastSpawn = PatternType.Wall;

                        break;

                    case PatternType.Spikes:
                        spawnDelay = jumpTimer;
                        lastSpawn = PatternType.Spikes;
                        break;

                    default:
                        return;
                }
            }

            if (bossCount >= bossChances - 1)
            {
                spawnDelay = bossTimer;
            }

            GameObject newBarrier = Instantiate(patternPrefab[(int)type]);

            Vector3 spawnPosition = newBarrier.transform.position;
            spawnPosition.x = 20.0f;
            newBarrier.transform.position = spawnPosition;

            inGamePattern.Add(newBarrier);
        }
    }

    //Destroy the specified pattern and remove it from it's list
    public void SubPattern(GameObject gameObject)
    {
        if (inGamePattern.Contains(gameObject))
        {
            inGamePattern.Remove(gameObject);
        }

        Destroy(gameObject);
    }

    private void CheckDeadPattern()
    {
        for (int i = 0; i < inGamePattern.Count; i++)
        {
            if (!inGamePattern[i].activeSelf)
            {
                SubPattern(inGamePattern[i]);
            }
        }
    }

    private void MovePattern()
    {
        for (int i = 0; i < inGamePattern.Count; i++)
        {
            inGamePattern[i].transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay * difficulty);

            AddPattern();
        }
    }

    private IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(15.0f);

        difficulty = 1.75f;

        yield return new WaitForSeconds(15.0f);

        difficulty = 1.5f;

        yield return new WaitForSeconds(15.0f);

        difficulty = 1.25f;

        yield return new WaitForSeconds(15.0f);

        difficulty = 1.0f;
    }
}
