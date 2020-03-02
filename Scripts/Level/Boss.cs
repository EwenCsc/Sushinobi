using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum Weakness
    {
        Up,
        Down,
        Left,
        Right,
        TopLeft,
        TopRight,
        DownLeft,
        DownRight,
        Count
    }

    private Transform player = null;
    private BossFrame bossFrame = null;
    private bool slowTime = false;
    private bool failed = false;

    private Vector2 startPosition = Vector2.zero;
    private Vector2 endPosition = Vector2.zero;

    private Weakness[] weaknesses = new Weakness[3];

    private int tryCount = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossFrame = GameObject.Find("BossFrame").GetComponent<BossFrame>();

        for (int i = 0; i < weaknesses.Length; i++)
        {
            weaknesses[i] = (Weakness)Random.Range(0, (int)Weakness.Count);
            bossFrame.SetArrows(weaknesses[i], i);
        }       
    }

    private void Update()
    {
        float offsetX = transform.position.x - player.position.x;

        if (!slowTime && !failed)
        {
            if (offsetX > 7.0f && offsetX < 10.0f)
            {
                slowTime = true;
                Time.timeScale = 0.1f;
                bossFrame.Display(true);
            }
        }
        else
        {
            CheckPlayerInput();
            CheckPlayerKeyboardInput();

            if (offsetX < 7.0f)
            {
                Unfreeze();
            }
        }
    }

    private void Unfreeze()
    {
        slowTime = false;
        Time.timeScale = 1.0f;
        bossFrame.Display(false);
    }

    private void CheckPlayerInput()
    {
        if (Input.touchCount > 0 && tryCount < 3)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                //Put finger on screen
                case TouchPhase.Began:
                    startPosition = touch.position;
                    break;

                //Finger get off screen
                case TouchPhase.Ended:
                    endPosition = touch.position;

                    if (GetDirection() == weaknesses[tryCount])
                    {
                        bossFrame.SetArrowColored(tryCount);

                        if (tryCount == 2)
                        {
                            StartCoroutine(WinDelay());
                        }
                    }
                    else
                    {
                        Unfreeze();
                        failed = true;
                    }

                    tryCount++;
                    break;

                default:
                    break;
            }
        }
    }

    private void CheckPlayerKeyboardInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPosition = Input.mousePosition;

            if (GetDirection() == weaknesses[tryCount])
            {
                bossFrame.SetArrowColored(tryCount);

                if (tryCount == 2)
                {
                    StartCoroutine(WinDelay());
                }
            }
            else
            {
                Unfreeze();
                failed = true;
            }

            tryCount++;
        }
    }

    private Weakness GetDirection()
    {
        Weakness result = Weakness.Count;

        Vector2 direction = endPosition - startPosition;
        direction.Normalize();

        if (direction.x > -0.3f && direction.x < 0.3f)
        {
            if (direction.y < 0.0f)
            {
                result = Weakness.Down;
            }
            else if (direction.y > 0.0f)
            {
                result = Weakness.Up;
            }
        }
        else if (direction.y > -0.3f && direction.y < 0.3f)
        {
            if (direction.x < 0.0f)
            {
                result = Weakness.Left;
            }
            else
            {
                result = Weakness.Right;
            }
        }
        else if (direction.x < -0.3f)
        {
            if (direction.y < -0.3f)
            {
                result = Weakness.TopLeft;
            }
            else if (direction.y > 0.3f)
            {
                result = Weakness.DownLeft;
            }
        }
        else if (direction.x > 0.3f)
        {
            if (direction.y < -0.3f)
            {
                result = Weakness.TopRight;
            }
            else if (direction.y > 0.3f)
            {
                result = Weakness.DownRight;
            }
        }

        return result;
    }

    private IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(0.1f);

        Die();
    }

    public void Die()
    {
        Unfreeze();
        enabled = false;
        gameObject.GetComponent<Enemy>().Die();
    }
}
