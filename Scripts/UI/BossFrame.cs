using UnityEngine;
using UnityEngine.UI;

public class BossFrame : MonoBehaviour
{
    [SerializeField] private GameObject background = null;
    [SerializeField] private RawImage[] arrows = null;

    public void Display(bool canDisplay)
    {
        background.SetActive(canDisplay);

        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].gameObject.SetActive(canDisplay);
            arrows[i].color = Color.white;
        }
    }

    public void SetArrows(Boss.Weakness weakness, int i)
    {
        Quaternion rotation = Quaternion.identity;

        switch (weakness)
        {
            case Boss.Weakness.Up:
                rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                break;

            case Boss.Weakness.Down:
                rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                break;

            case Boss.Weakness.Left:
                rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                break;

            case Boss.Weakness.Right:
                rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                break;

            case Boss.Weakness.TopLeft:
                rotation = Quaternion.Euler(0.0f, 0.0f, -45.0f);
                break;

            case Boss.Weakness.TopRight:
                rotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);
                break;

            case Boss.Weakness.DownLeft:
                rotation = Quaternion.Euler(0.0f, 0.0f, -135.0f);
                break;

            case Boss.Weakness.DownRight:
                rotation = Quaternion.Euler(0.0f, 0.0f, 135.0f);
                break;

            default:
                return;
        }

        arrows[i].transform.rotation = rotation;
    }

    public void SetArrowColored(int i)
    {
        arrows[i].color = Color.red;
    }
}
