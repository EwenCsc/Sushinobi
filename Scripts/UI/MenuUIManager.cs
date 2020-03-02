using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject menuPopup = null;
    [SerializeField] GameObject playPopup = null;
    [SerializeField] GameObject optionPopup = null;

    [SerializeField] Toggle specialToggle = null;
    [SerializeField] Text specialEquipPriceText = null;

    [SerializeField] Toggle armorToggle = null;
    [SerializeField] Text armorEquipPriceText = null;

    [SerializeField] private Image background = null;
    [SerializeField] private Text moneyText = null;

    [Header("Background Zooming")]
    [SerializeField] private Vector3 minBackgroundScale = new Vector3(1f,1f,1f);
    [SerializeField] private Vector3 maxBackgroundScale = new Vector3(2f, 2f, 2f);

    [SerializeField] private float scaleStep = 1.0f;
    private bool zoom;
    private bool unzoom;
    [SerializeField] private float minBackgroundHeight = -100f;
    [SerializeField] private float maxBackgroundHeight = 0f;

    [SerializeField] private Text canDieText = null;

    [Header("Doors")]
    [SerializeField] private GameObject leftDoor = null;
    [SerializeField] private GameObject rightDoor = null;

    public void Start()
    {
        AudioManager.Instance.PlayMusic("Menu", true);

        armorEquipPriceText.text = StateMemory.ArmorPrice.ToString();
        specialEquipPriceText.text = StateMemory.SpecialPrice.ToString();

        playPopup.SetActive(false);
        optionPopup.SetActive(false);
        zoom = false;
        unzoom = false;

        Time.timeScale = 1.0f;
        moneyText.text = StateMemory.PlayerMoney.ToString();
    }

    private void Update()
    {
        if (zoom)
        {
            if (background.transform.localScale == maxBackgroundScale)
            {
                zoom = false;
                playPopup.SetActive(true);
            }
            else
            {
                float addedScale = Mathf.Clamp(background.transform.localScale.x + scaleStep * Time.deltaTime, 1f, 2f);
                

                Vector3 bufferPosition = background.transform.position;
                bufferPosition.y -= Mathf.Clamp(minBackgroundHeight * Time.deltaTime * scaleStep, minBackgroundHeight, maxBackgroundHeight);
                background.transform.localScale = new Vector3(addedScale, addedScale, addedScale);
                background.transform.position = bufferPosition;

                float addedTranslate = Mathf.Clamp(75f * scaleStep * Time.deltaTime, 0f, 150f);
                leftDoor.transform.position = new Vector3(leftDoor.transform.position.x - addedTranslate, leftDoor.transform.position.y, leftDoor.transform.position.z);
                rightDoor.transform.position = new Vector3(rightDoor.transform.position.x + addedTranslate, rightDoor.transform.position.y, rightDoor.transform.position.z);
            }
        }
        if (unzoom)
        {
            if (background.transform.localScale == minBackgroundScale)
            {
                unzoom = false;
                menuPopup.SetActive(true);
            }
            else
            {
                float addedScale = Mathf.Clamp(background.transform.localScale.x - scaleStep * Time.deltaTime, 1f, 2f);
                Vector3 bufferPosition = background.transform.position;
                bufferPosition.y += Mathf.Clamp(minBackgroundHeight * Time.deltaTime * scaleStep, minBackgroundHeight, maxBackgroundHeight);


                background.transform.localScale = new Vector3(addedScale, addedScale, addedScale);
                background.transform.position = bufferPosition;

                float addedTranslate = Mathf.Clamp(75f * scaleStep * Time.deltaTime, 0f, 150f);
                leftDoor.transform.position = new Vector3(leftDoor.transform.position.x + addedTranslate, leftDoor.transform.position.y, leftDoor.transform.position.z);
                rightDoor.transform.position = new Vector3(rightDoor.transform.position.x - addedTranslate, rightDoor.transform.position.y, rightDoor.transform.position.z);
            }
        }
    }

    //-----------------------------//
    //-----GAME--------------------//
    //-----------------------------//
    public void DisplayPlay()
    {
        unzoom = false;
        zoom = true;

        menuPopup.SetActive(false);
    }

    public void HidePlay()
    {
        zoom = false;
        unzoom = true;

        specialToggle.isOn = false;
        armorToggle.isOn = false;

        playPopup.SetActive(false);
    }

    public void Validate()
    {
        if (StateMemory.PlayerMoney >= 0)
        {
            SceneManager.LoadScene("LoadGame");
        }
    }

    public void SwitchArmor()
    {
        if (StateMemory.IsArmorEquipped)
        {
            StateMemory.PlayerMoney += StateMemory.ArmorPrice;
        }
        else
        {
            StateMemory.PlayerMoney -= StateMemory.ArmorPrice;
        }

        StateMemory.IsArmorEquipped = !StateMemory.IsArmorEquipped;
    }

    public void SwitchWeapon()
    {
        if (StateMemory.IsSpecialEquipped)
        {
            StateMemory.PlayerMoney += StateMemory.SpecialPrice;
        }
        else
        {
            StateMemory.PlayerMoney -= StateMemory.SpecialPrice;
        }

        StateMemory.IsSpecialEquipped = !StateMemory.IsSpecialEquipped;
    }

    //-----------------------------//
    //-----OPTIONS-----------------//
    //-----------------------------//
    public void DisplayOptions()
    {
        optionPopup.SetActive(true);
    }

    public void HideOptions()
    {
        optionPopup.SetActive(false);
    }

    public void SwitchSound()
    {
        AudioManager.Instance.SwitchMuteSound();
    }

    public void SwitchMusic()
    {
        AudioManager.Instance.SwitchMuteMusic();
    }

    //-----------------------------//
    //-----STORE-------------------//
    //-----------------------------//
    public void OpenStore()
    {
        SceneManager.LoadScene("Store");
    }

    //DEBUG MILESTONE
    public void ResetStats()
    {
        StateMemory.ArmorLevel = 0;
        StateMemory.SpecialLevel = 0;
        StateMemory.PlayerMoney = 1000;

        StateMemory.ResetBestScores();
    }

    public void CanDie()
    {
        if (StateMemory.canDie)
        {
            StateMemory.canDie = false;
            canDieText.text = "Can't die";
        }
        else
        {
            StateMemory.canDie = true;
            canDieText.text = "Can die";
        }
    }
}
