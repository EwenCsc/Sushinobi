using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverUI gameOver = null;
    private PlayerActions playerActions = null;
    private PlayerInputs playerInput = null;

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Game", true);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerActions = player.GetComponent<PlayerActions>();
        playerInput = player.GetComponent<PlayerInputs>();
    }

    private void Update()
    {
        if (playerActions.isDead && Time.timeScale > 0.0f)
        {
            Time.timeScale = 0.0f;
            playerActions.enabled = false;
            playerInput.enabled = false;
            gameOver.gameObject.SetActive(true);
        }
    }
}
