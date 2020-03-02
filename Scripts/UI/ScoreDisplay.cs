using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	private void Update ()
    {
        gameObject.GetComponent<Text>().text = "Score : " + GameState.CurrentScore;
	}
}
