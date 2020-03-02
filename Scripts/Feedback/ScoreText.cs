using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [HideInInspector] public TextMesh text;
    [HideInInspector] public int score;
    private Vector3 despawnScore;

    private void Start()
    {
        despawnScore = new Vector3(0, 5, 0);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, despawnScore, 0.2f);
        if (transform.position == despawnScore)
        {
            GameState.CurrentScore += score;
            Destroy(gameObject);
        }
    }
}
