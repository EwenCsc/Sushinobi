using UnityEngine;

public class BreakWall : MonoBehaviour
{
    private Animator animator = null;
    private SpriteRenderer spriteRenderer = null;

    private bool fade = false;
    private float fadeTimer = 0.0f;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (fade)
        {
            fadeTimer += Time.deltaTime;

            if (fadeTimer < 1.0f)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - (fadeTimer * 2.0f));
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerActions>().isDashing)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                animator.SetTrigger("break");
                fade = true;
            }
        }
    }
}
