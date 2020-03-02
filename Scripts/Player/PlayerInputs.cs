using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Recover user input relative to player and call appropriate methods
/// </summary>
public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playerZone = null;
    [SerializeField] private BoxCollider2D enemyZone = null;
    [SerializeField] private BoxCollider2D attackZone = null;

    private PlayerActions playerActions = null;
    private PlayerAnimations playerAnimations = null;

    [SerializeField] private Slash slash = null;

    // Relative to inputs
    private RandomPatternManager patternManager;
    private Vector2 startPosition = Vector2.zero;
    private Vector2 endPosition = Vector2.zero;
    private Vector2 distance = Vector2.zero;

    private bool hasMoved = false;
    Vector2 touchPosition = Vector2.zero;

    string lastDirection;

    private void Start()
    {
        lastDirection = "";

        playerActions = gameObject.GetComponent<PlayerActions>();
        playerAnimations = gameObject.GetComponent<PlayerAnimations>();

        patternManager = FindObjectOfType<RandomPatternManager>();
    }

    private void Update()
    {
        CheckInputsKeyboard();
        CheckInputsMobile();
        lastDirection = GetDirection();
    }

    public void CheckInputsMobile()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                //Put finger on screen
                case TouchPhase.Began:
                    startPosition = touch.position;

                    touchPosition = Camera.main.ScreenToWorldPoint(startPosition);
                    if (enemyZone.bounds.Contains(touchPosition))
                    {
                        slash.Move(touch.position);
                    }
                    break;

                //Finger move on screen
                case TouchPhase.Moved:
                    endPosition = touch.position;

                    distance = new Vector2(Mathf.Abs(endPosition.x - startPosition.x), Mathf.Abs(endPosition.y - startPosition.y));

                    if (enemyZone.bounds.Contains(touchPosition))
                    {
                        slash.Move(touch.position);
                    }

                    if (distance.magnitude > 10.0f && !hasMoved)
                    {
                        DoAction();
                    }

                    break;

                //Finger get off screen
                case TouchPhase.Ended:
                    endPosition = touch.position;

                    distance = new Vector2(Mathf.Abs(endPosition.x - startPosition.x), Mathf.Abs(endPosition.y - startPosition.y));

                    if (enemyZone.bounds.Contains(touchPosition))
                    {
                        slash.Destruct();
                    }

                    if (!hasMoved)
                    {
                        DoAction();
                    }

                    hasMoved = false;
                    break;

                default:
                    break;
            }
        }
    }

    //DEBUG
    public void CheckInputsKeyboard()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;

            touchPosition = Camera.main.ScreenToWorldPoint(startPosition);
            if (enemyZone.bounds.Contains(touchPosition))
            {
                slash.Move(Input.mousePosition);
            }
        }

        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;

            distance = new Vector2(Mathf.Abs(endPosition.x - startPosition.x), Mathf.Abs(endPosition.y - startPosition.y));

            if (enemyZone.bounds.Contains(touchPosition))
            {
                slash.Move(Input.mousePosition);
            }

            if (distance.magnitude > 10.0f && !hasMoved)
            {
                DoAction();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPosition = Input.mousePosition;

            distance = new Vector2(Mathf.Abs(endPosition.x - startPosition.x), Mathf.Abs(endPosition.y - startPosition.y));

            if (enemyZone.bounds.Contains(touchPosition))
            {
                slash.Destruct();
            }

            if (!hasMoved)
            {
                DoAction();
            }

            hasMoved = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack("Horizontal");
            Attack("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DoPlayerAction("Up");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DoPlayerAction("Right");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            playerActions.ShootUlti();
        }
    }

    private void DoAction()
    {
        string direction = GetDirection();

        Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(startPosition);

        if (lastDirection == "Tap" && direction == "Tap")
        {
            playerActions.ShootUlti();
        }

        // If the input movement started in player zone
        if (playerZone.GetComponent<Collider2D>().bounds.Contains(fingerPosition))
        {
            DoPlayerAction(direction);
        }

        // If the input movement started in ennemy zone
        if (enemyZone.GetComponent<Collider2D>().bounds.Contains(fingerPosition))
        {
            Attack(direction);
        }
    }

    public string GetDirection()
    {
        string result = "";

        if (distance.magnitude < 10.0f)
        {
            return "Tap";
        }

        //Horizontal
        if (distance.x > distance.y)
        {
            result = "Horizontal / ";

            //Check diagonal
            if (distance.y > distance.x / 2.0f)
            {
                if (endPosition.y < startPosition.y) result += "Down-";
                else result += "Up-";
            }

            if (endPosition.x > startPosition.x)
            {
                result += "Right";
            }
            else if (endPosition.x < startPosition.x)
            {
                result += "Left";
            }
        }

        //Vertical
        else if (distance.y > distance.x)
        {
            result = "Vertical / ";

            if (endPosition.y > startPosition.y)
            {
                result += "Up";
            }
            else if (endPosition.y < startPosition.y)
            {
                result += "Down";
            }

            //Check diagonal
            if (distance.x > distance.y / 2.0f)
            {
                if (endPosition.x < startPosition.x)
                {
                    result += "-Left";
                }
                else
                {
                    result += "-Right";
                }
            }
        }
        return result;
    }

    private void DoPlayerAction(string direction)
    {
        if (direction.Contains("Up"))
        {
            playerActions.Jump();
            hasMoved = true;
        }
        else if (direction.Contains("Right"))
        {
            playerActions.Dash();
            hasMoved = true;
        }

        direction = "NULL";
    }

    private void Attack(string direction)
    {
        Enemy enemy = null;

        if (direction.Contains("Horizontal"))
        {
            AudioManager.Instance.PlaySound("Slash");
            playerAnimations.AttackHorizontal();
            hasMoved = true;

            if (IsEnemyInRange(ref enemy))
            {
                playerActions.AttackHorizontal(enemy);
            }
        }
        else if (direction.Contains("Vertical"))
        {
            AudioManager.Instance.PlaySound("Slash");
            playerAnimations.AttackVertical();
            hasMoved = true;

            if (IsEnemyInRange(ref enemy))
            {
                playerActions.AttackVertical(enemy);
            }
        }
    }

    private bool IsEnemyInRange(ref Enemy enemy)
    {
        if (patternManager.inGamePattern.Count > 0)
        {
            for (int i = 0; i < patternManager.inGamePattern.Count; i++)
            {
                Enemy[] currentEnemies = patternManager.inGamePattern[i].GetComponentsInChildren<Enemy>();
                for (int j = 0; j < currentEnemies.Length; j++)
                {
                    Collider2D enemyCollider = currentEnemies[j].GetComponent<Collider2D>();

                    if (enemyCollider.enabled)
                    {
                        Vector3 enemyPosition = currentEnemies[j].gameObject.transform.position;
                        if (enemyPosition.x >= transform.position.x && attackZone.bounds.Intersects(enemyCollider.bounds))
                        {
                            enemy = currentEnemies[j];

                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}
