using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum EnemyType
    {
        Horizontal,
        Vertical,
        Boss
    }

    [SerializeField] private GameObject textScorePrefab = null;
    [SerializeField] private int score = 0;
    [SerializeField] private int ultiPoints = 0;

    [SerializeField] private EnemyType ennemyType = EnemyType.Horizontal;
    [HideInInspector] public string weakness = "";

    private SpriteRenderer spriteRenderer = null;
    private bool die = false;
    private float fadeTimer = 0.0f;

    void Start()
    {
        switch (ennemyType)
        {
            case EnemyType.Horizontal:
                weakness = "Horizontal";
                break;

            case EnemyType.Vertical:
                weakness = "Vertical";
                break;

            case EnemyType.Boss:
                weakness = "Boss";
                break;

            default:
                return;
        }

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (transform.position.x - playerPosition.x <= 7.0f)
        {
            gameObject.GetComponent<Animator>().SetTrigger("attack");
        }

        if (die)
        {
            fadeTimer += Time.deltaTime;
            if (fadeTimer < 1.0f)
            {
                spriteRenderer.color = new Color(255, 255, 255, 1.0f - (fadeTimer * 2.0f));
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Hit(string inputDir)
    {
        if (inputDir.Contains(weakness))
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.GetComponent<Animator>().SetTrigger("die");
        gameObject.GetComponent<Collider2D>().enabled = false;

        GameObject newTextScore = Instantiate(textScorePrefab);
        newTextScore.transform.position = transform.position;
        newTextScore.GetComponent<ScoreText>().score = score;

        TextMesh textMesh = newTextScore.GetComponent<TextMesh>();
        textMesh.text = score.ToString();
        textMesh.characterSize = Random.Range(1f, 1.5f);

        die = true;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>().AddUlti(ultiPoints);
    }
}
