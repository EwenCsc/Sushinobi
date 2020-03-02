using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSub : MonoBehaviour
{
    [SerializeField] private RandomPatternManager patternManager = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Deadly")
        {
            patternManager.SubPattern(collision.gameObject);
        }
    }
}
