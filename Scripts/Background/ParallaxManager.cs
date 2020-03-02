using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drawn and move parallax items
/// </summary>
public class ParallaxManager : MonoBehaviour
{
    public List<Sprite> sprites;
    public bool isActivated;

    public float minTimeBetweenTwoSpawn;
    public float maxTimeBetweenTwoSpawn;

    private List<ParallaxItem> parallaxItems;
    private List<int> lastItemIndex;
    private int parallaxVariety; // 3 oblige d'avoir 3 sprites différents avant de relancer le même

    private float lastSpawnTime = 0.0f;
    private float nextSpawnTime = 0.0f;

    [SerializeField] float speed = 0.0f; // must be the same as Carpet

    // Use this for initialization
    void Start()
    {
        lastSpawnTime = 0;
        nextSpawnTime = Random.Range(minTimeBetweenTwoSpawn, maxTimeBetweenTwoSpawn);

        parallaxItems = new List<ParallaxItem>();

        parallaxVariety = 3;

        lastItemIndex = new List<int>();

        for (int i = 0; i < sprites.Count; i++)
        {
            GameObject item = new GameObject("parallax_" + i);
            item.transform.parent = this.transform;
            item.transform.localPosition = Vector3.zero;

            ParallaxItem itemScript = item.AddComponent<ParallaxItem>();
            itemScript.spriteRenderer = item.AddComponent<SpriteRenderer>();
            itemScript.spriteRenderer.sprite = sprites[i];
            itemScript.speed = speed;

            parallaxItems.Add(itemScript);
        }
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawnTime += Time.deltaTime;
        if (lastSpawnTime >= nextSpawnTime)
        {
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        int rand;
        int nbTry = 0;

        do
        {
            rand = Random.Range(0, sprites.Count);
            nbTry++;
        }
        while (lastItemIndex.Contains(rand) || nbTry < sprites.Count);

        lastItemIndex.Add(rand);

        if (lastItemIndex.Count > parallaxVariety)
        {
            lastItemIndex.RemoveAt(0);
        }

        lastSpawnTime = 0;
        ActivateItem(parallaxItems[rand]);
    }

    private void ActivateItem(ParallaxItem item)
    {
        item.Activate();
    }

    private void DesactivateItem(ParallaxItem item)
    {
        item.Desactivate();
    }
}
