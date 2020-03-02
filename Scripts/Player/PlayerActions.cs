using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpSpeed = 0f;
    [SerializeField] private float dashSpeed = 0f;
    [SerializeField] private float dashLength = 0f;

    [SerializeField] private GameObject armor = null;
    [SerializeField] private int currentArmor = 0;

    [SerializeField] private Hit hit = null;

    [SerializeField] private GameObject ultimatePrefab = null;
    [SerializeField] private GameObject ultimateAura = null;
    private int specialAttackLevel = 0;
    [SerializeField] GameObject EnergieReceptacle;
    Vector2 newEnergieReceptacle;

    // Player datas
    private bool canJump = true;
    private bool canDash = true;
    [HideInInspector] public bool isDashing = false;

    [HideInInspector] public float initialXPosition = 0f;
    [HideInInspector] public float dashEndXPosition = 0f;

    private Rigidbody2D playerRb = null;
    private PlayerAnimations playerAnimations = null;

    [HideInInspector] public bool isDead = false;

    private void Start()
    {
        initialXPosition = transform.position.x;
        dashEndXPosition = transform.position.x + dashLength;

        playerRb = gameObject.GetComponent<Rigidbody2D>();
        playerAnimations = gameObject.GetComponent<PlayerAnimations>();
        
        EnergieReceptacle.GetComponent<RectTransform>().transform.localScale = Vector2.zero;
        newEnergieReceptacle = Vector2.zero;

        if (StateMemory.IsSpecialEquipped)
        {
            AddUlti(25);
        }

        if (StateMemory.IsArmorEquipped)
        {
            AddArmor();
        }
    }

    private void Update()
    {
        if (isDashing)
        {
            if (transform.position.x > dashEndXPosition)
            {
                transform.position = new Vector3(dashEndXPosition, transform.position.y, transform.position.z);

                isDashing = false;

                playerAnimations.DashEnd();
            }
        }
        else if (!canDash && !isDashing)
        {
            Vector2 velocity = playerRb.velocity;

            //If player back to initial position, reset velocity and force position
            if (transform.position.x < initialXPosition)
            {
                transform.position = new Vector3(initialXPosition, transform.position.y, transform.position.z);
                velocity.x = 0.0f;

                canDash = true;
                playerAnimations.ResetFXAnimatorPosition();
            }
            else
            {
                velocity.x = (-dashSpeed / 3.0f);
            }

            playerRb.velocity = velocity;
        }

        EnergieReceptacle.GetComponent<RectTransform>().transform.localScale = Vector2.MoveTowards(EnergieReceptacle.GetComponent<RectTransform>().transform.localScale, newEnergieReceptacle, 0.01f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MainOnCollisionEnter(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainOnTriggerEnter(collision);
    }

    public void Jump()
    {
        if (canDash && canJump)
        {
            canJump = false;
            playerRb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

            playerAnimations.JumpStart(); // Animation logic
        }
    }

    public void Dash()
    {
        AudioManager.Instance.PlaySound("Dash");
        if (canDash)
        {
            playerAnimations.DashStart();
            playerRb.AddForce(Vector2.right * dashSpeed, ForceMode2D.Impulse);

            canDash = false;
            isDashing = true;
        }
    }

    public void AttackHorizontal(Enemy enemy)
    {
        enemy.Hit("Horizontal");
        playerAnimations.AttackHorizontal();
    }

    public void AttackVertical(Enemy enemy)
    {
        enemy.Hit("Vertical");
        playerAnimations.AttackVertical();
    }

    /// <summary>
    /// Player got hit and take damage
    /// </summary>
    public void Hit()
    {
        hit.DisplayHit();

        if (currentArmor > 0)
        {
            AudioManager.Instance.PlaySound("PlayerHit");

            currentArmor--;

            if (currentArmor == 0)
            {
                armor.SetActive(false);
            }
        }
        else
        {
            //DEBUG MILESTONE
            isDead = StateMemory.canDie;

            //isDead = true;
        }
    }

    // COLLISIONS and TRIGGERS
    // Called when player OnCollisionEnter2D
    public void MainOnCollisionEnter(Collision2D collision)
    {
        canJump = true;
        playerAnimations.JumpEnd();
    }

    public void MainOnTriggerEnter(Collider2D other)
    {
        if ((other.CompareTag("Deadly") && !isDashing) || other.name.Contains("Spike"))
        {
            Hit();
        }
    }

    public void AddUlti(int ultiPoints)
    {
        if (specialAttackLevel < 25)
        {
            specialAttackLevel += ultiPoints;
            specialAttackLevel = Mathf.Clamp(specialAttackLevel, 0, 25);
            newEnergieReceptacle = new Vector2(specialAttackLevel / 25f, specialAttackLevel / 25f);
        }
        if (!ultimateAura.activeSelf && specialAttackLevel >= 25)
        {
            ultimateAura.SetActive(true);
        }
    }

    public void AddArmor()
    {
        currentArmor = StateMemory.ArmorLevel + 1;

        armor.SetActive(true);
    }

    public void ShootUlti()
    {
        if (ultimateAura.activeSelf)
        {
            specialAttackLevel = 0;
            ultimateAura.SetActive(false);

            GameObject hado = Instantiate(ultimatePrefab);
            hado.transform.position = new Vector3(-4.0f, -1.0f);

            playerAnimations.AttackSpecial();
            AudioManager.Instance.PlaySound("Ulti");
        }
    }
}
