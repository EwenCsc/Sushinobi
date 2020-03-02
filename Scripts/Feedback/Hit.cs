using System.Collections;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] private GameObject[] hitSprites = null;

    public void DisplayHit()
    {
        StartCoroutine(HitTimer());
    }

    private IEnumerator HitTimer()
    {
        int index = Random.Range(0, hitSprites.Length);
        hitSprites[index].SetActive(true);

        yield return new WaitForSeconds(0.1f);

        hitSprites[index].SetActive(false);
    }
}
