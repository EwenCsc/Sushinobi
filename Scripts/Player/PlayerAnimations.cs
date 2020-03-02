using UnityEngine;

/// <summary>
/// Here is updated all the animator logic
/// </summary>
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator bodyAnimator = null;
    [SerializeField] private Animator armAnimator = null;
    [SerializeField] private Animator FXAnimator = null;
    [SerializeField] private SpriteRenderer eyes = null;

    private float eyeTimer = 0.0f;
    private float blinkTimer = 0.0f;
    private float closedTimer = 0.0f;

    private void Start ()
    {
        blinkTimer = Random.Range(1.0f, 2.0f);
    }

    private void Update()
    {
        eyeTimer += Time.deltaTime;
        if (eyeTimer >= blinkTimer)
        {
            if (!eyes.gameObject.activeSelf)
            {
                eyes.gameObject.SetActive(true);
            }

            closedTimer += Time.deltaTime;
            if (closedTimer >= 0.15f)
            {
                eyeTimer = 0.0f;
                closedTimer = 0.0f;
                blinkTimer = Random.Range(2.0f, 5.0f);
                eyes.gameObject.SetActive(false);
            }
        }
    }

    public void JumpStart()
    {
        bodyAnimator.SetBool("isOnGround", false);
        FXAnimator.SetBool("isOnGround", false);
    }

    public void JumpEnd()
    {
        // If in the first frame of the scene, player have a collision
        // Avoid null reference exception
        if (!bodyAnimator) return;

        bodyAnimator.SetBool("isOnGround", true);
        FXAnimator.SetBool("isOnGround", true);
    }

    public void DashStart()
    {
        bodyAnimator.SetBool("isDashing", true);

        Vector3 position = FXAnimator.transform.position;
        position.x += 2.5f;
        FXAnimator.transform.position = position;

        FXAnimator.SetBool("isDashing", true);
    }

    public void DashEnd()
    {
        bodyAnimator.SetBool("isDashing", false);        
    }

    public void ResetFXAnimatorPosition()
    {
        FXAnimator.SetBool("isDashing", false);

        Vector3 position = FXAnimator.transform.position;
        position.x -= 2.5f;
        FXAnimator.transform.position = position;
    }

    public void AttackHorizontal()
    {
        armAnimator.SetTrigger("horizontalAttack");
    }

    public void AttackVertical()
    {
        armAnimator.SetTrigger("verticalAttack");
    }

    public void AttackSpecial()
    {
        armAnimator.SetTrigger("specialAttack");
    }
}
