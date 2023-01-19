using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapPoint : MonoBehaviour
{
    [SerializeField] private Transform warningImage;
    
    [SerializeField] private Image lockImage;
    [SerializeField] private TextMeshProUGUI sceneNumberText;
    
    [SerializeField] private Image toDoImage;
    [SerializeField] private Image boosterImage;
    [SerializeField] private TextMeshProUGUI fishCountText;
    [SerializeField] private TextMeshProUGUI upgradesCountText;

    [SerializeField] private Button selectSceneButton;
    [SerializeField] private Button openRequirementsButton;

    [SerializeField] private int loadSceneNumber;
    [SerializeField] private int needToCatchFishCount;
    [SerializeField] private int needToUpgradeCount;

    private enum UpgradeType
    {
        HookLength, HookStrength, OfflineMoney
    }

    [SerializeField] private UpgradeType upgradeType;

    [SerializeField] private Booster hookLengthBooster, hookStrengthBooster, offlineMoneyStrength;
    [SerializeField] private Sprite hookLengthBoosterSprite, hookStrengthBoosterSprite, offlineMoneyStrengthSprite;

    private void OnEnable()
    {
        EventManager.OnFishCollected += CheckPointForOpen;
        EventManager.OnLengthValueChanged += CheckPointForOpen;
        EventManager.OnStrengthValueChanged += CheckPointForOpen;
        EventManager.OnOfflineMoneyValueChanged += CheckPointForOpen;
    }

    private void OnDisable()
    {
        EventManager.OnFishCollected -= CheckPointForOpen;
        EventManager.OnLengthValueChanged -= CheckPointForOpen;
        EventManager.OnStrengthValueChanged -= CheckPointForOpen;
        EventManager.OnOfflineMoneyValueChanged -= CheckPointForOpen;
    }

    private void Start()
    {
        CheckPointForOpen();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (loadSceneNumber == SceneManager.GetActiveScene().buildIndex)
        {
            lockImage.gameObject.SetActive(false);
            selectSceneButton.interactable = true;
            sceneNumberText.text = $"{loadSceneNumber}";
        }
        else
        {
            lockImage.gameObject.SetActive(true);
            selectSceneButton.interactable = false;
        }

        fishCountText.text = $"{needToCatchFishCount}";
        upgradesCountText.text = $"{needToUpgradeCount}";

        switch (upgradeType)
        {
            case UpgradeType.HookLength:
                boosterImage.sprite = hookLengthBoosterSprite;
                break;
            case UpgradeType.HookStrength:
                boosterImage.sprite = hookStrengthBoosterSprite;
                break;
            case UpgradeType.OfflineMoney:
                boosterImage.sprite = offlineMoneyStrengthSprite;
                break;
        }
    }

    private void CheckPointForOpen()
    {
        var values = MissionMenuValues.Instance;
        switch (upgradeType)
        {
            case UpgradeType.HookLength:
                if (hookLengthBooster.currentValueLevel >= needToUpgradeCount &&
                    values.GetCatchFishesCount() >= needToCatchFishCount)
                {
                    OpenPoint();
                }
                break;
            case UpgradeType.HookStrength:
                if (hookStrengthBooster.currentValueLevel >= needToUpgradeCount &&
                    values.GetCatchFishesCount() >= needToCatchFishCount)
                {
                    OpenPoint();
                }
                break;
            case UpgradeType.OfflineMoney:
                if (offlineMoneyStrength.currentValueLevel >= needToUpgradeCount &&
                    values.GetCatchFishesCount() >= needToCatchFishCount)
                {
                    OpenPoint();
                }
                break;
        }
    }

    public void OpenWarningImage()
    {
        warningImage.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear);
    }

    public void CloseWarningImage()
    {
        warningImage.DOScale(0, 0.25f).From(1).SetEase(Ease.Linear);
    }
    
    public void SelectLevel()
    {
        AsyncLoadScene.Instance.ActivateScreen(loadSceneNumber);
    }

    private void OpenPoint()
    {
        UpdateUI();
        if (loadSceneNumber != SceneManager.GetActiveScene().buildIndex)
        {
            EventManager.OnNewLevelOpenedInvoke();
        }
        lockImage.gameObject.SetActive(false);
        selectSceneButton.interactable = true;
        sceneNumberText.text = $"{loadSceneNumber}";
        openRequirementsButton.enabled = false;
        Destroy(toDoImage);
    }

    public void OpenPointRequirements()
    {
        UpdateUI();
        toDoImage.transform.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear);
        openRequirementsButton.interactable = false;
        StartCoroutine(ClosePointRequirements());
    }

    [SerializeField] private float timeToCloseRequirements;
    
    private IEnumerator ClosePointRequirements()
    {
        yield return new WaitForSeconds(timeToCloseRequirements);
        toDoImage.transform.DOScale(0, 0.25f).From(1).SetEase(Ease.Linear);   
        openRequirementsButton.interactable = true;
    }
}
