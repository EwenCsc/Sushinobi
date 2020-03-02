using UnityEngine;

public class ArmorDisplay : MonoBehaviour
{
    private void Start()
    {
        switch (StateMemory.ArmorLevel)
        {
            case 0:
                DisplayArmorPiece(0);
                break;

            case 1:
                DisplayArmorPiece(1);
                break;

            case 2:
                DisplayArmorPiece(2);
                break;

            default:
                break;
        }
    }

    private void DisplayArmorPiece(int pIndex)
    {
        GameObject armorPiece = transform.GetChild(pIndex).gameObject;
        armorPiece.SetActive(true);
    }
}
