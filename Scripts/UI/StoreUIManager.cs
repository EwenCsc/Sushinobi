using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StoreUIManager : MonoBehaviour
{
    [Header("Windows")]
    [SerializeField] GameObject upgradesPopup = null;
    [SerializeField] GameObject cosmeticsPopup = null;

    [Header("UI Elements")]
    [Header("Armor Upgrade")]
    [SerializeField] private Button armorUpgradeButton = null;
    [SerializeField] private Image armorUpgradeImage = null;

    [Header("Special Upgrade")]
    [SerializeField] private Button specialUpgradeButton = null;
    [SerializeField] private Image specialUpgradeImage = null;

    [Header("Next/Previous")]
    [SerializeField] private Button previousButton = null;
    [SerializeField] private Button nextButton = null;

    [SerializeField] private GameObject upgradesList = null;
    [SerializeField] private Scrollbar upgradesScrollbar = null;

    [SerializeField] private Text UpgradePrice = null;

    [Header("Ressources")]
    [SerializeField] private Sprite[] specialSprites = null;
    [SerializeField] private Sprite[] armorSprites = null;

    private int currentItem = 0;

    #region UPGRADES
    public void DisplayUpgrades()
    {
        upgradesPopup.SetActive(true);

        UpdateSpecial();
        UpdateArmor();

        AudioManager.Instance.PlaySound("UIClick");
    }

    public void HideUpgrades()
    {
        upgradesPopup.SetActive(false);

        AudioManager.Instance.PlaySound("UIClick");
    }

    private void UpdateSpecial()
    {
        specialUpgradeImage.sprite = specialSprites[StateMemory.SpecialLevel];

        UpdatePriceTag("Weapon");
    }

    public void UpgradeSpecial()
    {
        if (StateMemory.SpecialTypes.Count - 1 > StateMemory.SpecialLevel && StateMemory.PlayerMoney >= StateMemory.SpecialTypes[StateMemory.SpecialLevel].m_price)
        {
            StateMemory.SpecialLevel++;
            StateMemory.PlayerMoney -= StateMemory.SpecialTypes[StateMemory.SpecialLevel].m_price;

            UpdateSpecial();
        }
    }

    public void UpdateArmor()
    {
        armorUpgradeImage.sprite = armorSprites[StateMemory.ArmorLevel];

        UpdatePriceTag("Armor");
    }

    public void UpgradeArmor()
    {
        if (StateMemory.ArmorTypes.Count - 1 > StateMemory.ArmorLevel && StateMemory.PlayerMoney >= StateMemory.ArmorTypes[StateMemory.ArmorLevel].m_price)
        {
            StateMemory.ArmorLevel++;
            StateMemory.PlayerMoney -= StateMemory.ArmorTypes[StateMemory.ArmorLevel].m_price;

            UpdateArmor();
        }
    }

    public void UpdatePriceTag(string _product)
    {
        switch (_product)
        {
            case "Armor":
                if (StateMemory.ArmorTypes.Count - 1 > StateMemory.ArmorLevel)
                {
                    UpgradePrice.text = (StateMemory.ArmorTypes[StateMemory.ArmorLevel + 1].m_price).ToString();
                }
                else
                {
                    UpgradePrice.text = "No More Upgrade";
                }
                break;
            case "Weapon":
                if (StateMemory.SpecialTypes.Count - 1 > StateMemory.SpecialLevel)
                {
                    UpgradePrice.text = (StateMemory.SpecialTypes[StateMemory.SpecialLevel + 1].m_price).ToString();
                }
                else
                {
                    UpgradePrice.text = "No More Upgrade";
                }
                break;
            default:
                break;
        }
    }

    public void NextUpgradable()
    {
        upgradesScrollbar.value += 1f;
        if(currentItem  != 1)
        {
            ++currentItem;
            UpdatePriceTag("Weapon");
        }

    }

    public void PreviousUpgradable()
    {
        upgradesScrollbar.value -= 1f;
        if (currentItem != 0)
        {
            --currentItem;
            UpdatePriceTag("Armor");
        }
    }
    #endregion

    #region COSMETICS
    public void DisplayCosmetics()
    {
        cosmeticsPopup.SetActive(true);
        AudioManager.Instance.PlaySound("UIClick");
    }

    public void HideCosmetics()
    {
        cosmeticsPopup.SetActive(false);
        AudioManager.Instance.PlaySound("UIClick");
    }
    #endregion


    public void ReturnMenu()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
        AudioManager.Instance.PlaySound("UIClick");
    }
}
