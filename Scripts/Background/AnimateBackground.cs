using System.Collections.Generic;
using UnityEngine;

public class AnimateBackground : MonoBehaviour
{
    [SerializeField] private GameObject first = null;
    [SerializeField] private GameObject step = null;
    [SerializeField] private GameObject reverseStep = null;

    [SerializeField] private GameObject[] firstRandom = null;
    [SerializeField] private GameObject[] secondRandom = null;

    [SerializeField] private float speed = 0.0f;

    [SerializeField] private float minSwapDelay = 0.0f;
    [SerializeField] private float maxSwapDelay = 0.0f;

    private float secondDelay = 0.0f;
    private float secondTimer = 0.0f;
    private bool secondSpawn = false;

    private List<GameObject> onScreen = new List<GameObject>();
    private float spriteWidth = 0.0f;

    private void Start()
    {
        spriteWidth = first.GetComponent<SpriteRenderer>().bounds.size.x;

        secondDelay = Random.Range(minSwapDelay, maxSwapDelay);

        onScreen.Add(first);

        AddFirst();
    }

    private void Update()
    {
        CheckSecond();

        CheckOutOfScreen();

        for (int i = 0; i < onScreen.Count; i++)
        {
            onScreen[i].transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void CheckSecond()
    {
        secondTimer += Time.deltaTime;

        if (secondTimer >= secondDelay)
        {
            secondSpawn = !secondSpawn;

            secondTimer = 0.0f;
            secondDelay = Random.Range(minSwapDelay, maxSwapDelay);

            if (secondSpawn)
            {
                AddStep(step);
            }
            else
            {
                AddStep(reverseStep);
            }
        }
    }

    private void AddFirst()
    {
        int index = Random.Range(0, firstRandom.Length);
        GameObject newBack = firstRandom[index];

        newBack.transform.position = new Vector3(first.transform.position.x + spriteWidth, first.transform.position.y);

        onScreen.Add(newBack);
    }

    private void AddStep(GameObject toAdd)
    {
        GameObject previous = onScreen[onScreen.Count - 1];
        toAdd.transform.position = new Vector3(previous.transform.position.x + spriteWidth, previous.transform.position.y);
        onScreen.Add(toAdd);
    }

    private void AddNewRandom()
    {
        GameObject newBack = null;

        do
        {
            if (secondSpawn)
            {
                int index = Random.Range(0, secondRandom.Length);
                newBack = secondRandom[index];
            }
            else
            {
                int index = Random.Range(0, firstRandom.Length);
                newBack = firstRandom[index];
            }

        } while (onScreen.Contains(newBack));

        GameObject previous = onScreen[onScreen.Count - 1];
        newBack.transform.position = new Vector3(previous.transform.position.x + spriteWidth, previous.transform.position.y);
        onScreen.Add(newBack);
    }

    private void CheckOutOfScreen()
    {
        for (int i = 0; i < onScreen.Count; i++)
        {
            if (onScreen[i].transform.position.x <= -spriteWidth)
            {
                onScreen[i].transform.position = new Vector3(spriteWidth, onScreen[i].transform.position.y);
                onScreen.Remove(onScreen[i]);

                if (onScreen.Count < 3)
                {
                    AddNewRandom();
                }
            }
        }
    }
}
