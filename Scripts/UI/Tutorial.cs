using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutoHorizontal = null;
    [SerializeField] private GameObject tutoVertical = null;
    [SerializeField] private GameObject tutoDash = null;
    [SerializeField] private GameObject tutoJump = null;

    private void Start()
    {
        tutoHorizontal.SetActive(false);
        tutoVertical.SetActive(false);
        tutoDash.SetActive(false);
        tutoJump.SetActive(false);
    }

    private void Update()
    {
        if (!StateMemory.HasSeenTuto)
        {
            Time.timeScale = 0.0f;
            tutoHorizontal.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void VerticalAttackTuto()
    {
        tutoVertical.SetActive(true);
    }

    public void DashTuto()
    {
        tutoVertical.SetActive(false);
        tutoDash.SetActive(true);
    }

    public void JumpTuto()
    {
        tutoDash.SetActive(false);
        tutoJump.SetActive(true);
    }

    public void EndOfTuto()
    {
        tutoJump.SetActive(false);
        gameObject.SetActive(false);

        Time.timeScale = 1.0f;
        StateMemory.HasSeenTuto = true;
    }
}
