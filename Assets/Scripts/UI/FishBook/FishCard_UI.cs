using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishCard_UI : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image fishImage;
    [SerializeField] private Button collectRewardButton;

    [Header("Values")] 
    [SerializeField] private FishType fishType;
    [SerializeField] private string lockedFishDescription;

    private void Start()
    {
        if (fishType.isCollected)
        {
            descriptionText.text = fishType.fishDescription;
            fishImage.sprite = fishType.fishSprite;
            collectRewardButton.gameObject.SetActive(true);
        }
        else
        {
            descriptionText.text = lockedFishDescription;
            fishImage.sprite = fishType.lockedFishSprite;
            collectRewardButton.gameObject.SetActive(false);
        }
    }
}
