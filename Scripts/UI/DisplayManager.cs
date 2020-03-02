using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {

    enum Displayable { Score, Money};

    [Header("Display")]
    [SerializeField] Displayable toDisplay = Displayable.Score;
	
	// Update is called once per frame
	void Update () {
        string newText = "";

        switch (toDisplay){
            case Displayable.Money:
                newText = StateMemory.PlayerMoney.ToString();
                break;
            case Displayable.Score:
                newText += "Score : " + GameState.CurrentScore;
                break;
            default:
                newText += "ERROR";
                break;
        }

        GetComponent<Text>().text = newText;
	}
}
