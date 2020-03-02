using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDishes : MonoBehaviour
{
    [SerializeField] private GameObject[] dishes = new GameObject[0];
    [SerializeField] private Transform spawnPosition = null;
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float spawnDelay = 0.0f;

    private List<GameObject> inGameDishes = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnDish());
    }

    private void Update()
    {
        for (int i = 0; i < inGameDishes.Count; i++)
        {
            GameObject dish = inGameDishes[i];

            dish.transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (dish.transform.position.x < -spawnPosition.position.x)
            {
                dish.transform.position = spawnPosition.position;
                inGameDishes.Remove(dish);
            }
        }
    }

    private IEnumerator SpawnDish()
    {
        while (true)
        {
            int index = 0;
            do
            {
                index = Random.Range(0, dishes.Length);
            } while (inGameDishes.Contains(dishes[index]));

            inGameDishes.Add(dishes[index]);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
