/* 
 * @author Garnier Gael
 * @date 10/10/2018
 * @file PowerUpSpawner.cs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUpPrefabs = null; // Links to prefabs
    [SerializeField] private float minDelaySpawn = 0.0f;
    [SerializeField] private float maxDelaySpawn = 0.0f;

    [SerializeField] private float speed = 0.0f;

    private List<GameObject> inGamePowerUp = new List<GameObject>(); // GameObject instanciés
    private bool isWaiting = false;

    private void Update()
    {
        if (!isWaiting)
        {
            isWaiting = true;
            StartCoroutine(WaitSpawnDelay());
        }

        for (int i = 0; i < inGamePowerUp.Count; i++)
        {
            Move(inGamePowerUp[i]);
        }
    }

    private void Move(GameObject powerUp)
    {
        // Displace PowerUp following the cosinus function
        powerUp.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);

        // If get too far from screen or isCollected
        if (transform.position.x < -12 || powerUp.GetComponent<PowerUp>().isCollected)
        {
            inGamePowerUp.Remove(powerUp);
            Destroy(powerUp);
        }
    }

    private void SpawnRandFromPool()
    {
        int index = Random.Range(0, powerUpPrefabs.Length);
        GameObject newPowerUp = Instantiate(powerUpPrefabs[index], transform);
        inGamePowerUp.Add(newPowerUp);
    }

    private IEnumerator WaitSpawnDelay()
    {
        float delay = Random.Range(minDelaySpawn, maxDelaySpawn);

        yield return new WaitForSeconds(delay);

        isWaiting = false;

        SpawnRandFromPool();
    }
}
