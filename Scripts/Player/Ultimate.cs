using UnityEngine;

public class Ultimate : MonoBehaviour
{
    [SerializeField] private float speed = 0.0f;
    private Rigidbody2D rb = null;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * speed;
    }

    private void Update()
    {
        if (transform.position.x >= 12f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Deadly")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            Boss boss = collision.GetComponent<Boss>();

            if (boss != null)
            {
                boss.Die();
            }
            else if (enemy != null)
            {
                enemy.Die();
            }
            else
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}
