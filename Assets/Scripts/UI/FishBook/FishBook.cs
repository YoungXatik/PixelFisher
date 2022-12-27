    using System.Collections;
using System.Collections.Generic;
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.UI;

    public class FishBook : MonoBehaviour
    {
        #region SingleTon

        public static FishBook Instance;

        #endregion
        
        [Header("UI Elements")] 
        [SerializeField] private GameObject menuImage;
        [SerializeField] private Button mainMenuOpenButton;
        [SerializeField] private Button mainMenuCloseButton;

        [Header("CommonMenu")] 
        [SerializeField] private Transform commonMenuScrollArea;
        [SerializeField] private GameObject commonMenu;
        [SerializeField] private Button commonMenuButton;
        
        [Header("RareMenu")]
        [SerializeField] private Transform rareMenuScrollArea;
        [SerializeField] private GameObject rareMenu;
        [SerializeField] private Button rareMenuButton;
        
        [Header("FindsMenu")]
        [SerializeField] private GameObject findsMenu;
        [SerializeField] private Button findsMenuButton;

        public bool IsOpen { get; private set; } 
        
        public void OpenMainMenu()
        {
            menuImage.SetActive(true);
            mainMenuCloseButton.interactable = false;
            menuImage.transform.DOScale(1, 0.5f).From(0).SetEase(Ease.Linear).OnComplete(delegate
            {
                mainMenuCloseButton.interactable = true;
                IsOpen = true;
            });
            
        }

        public void CloseMainMenu()
        {
            mainMenuOpenButton.interactable = false;
            menuImage.transform.DOScale(0, 0.5f).From(1).SetEase(Ease.Linear).OnComplete(delegate
            {
                mainMenuOpenButton.interactable = true;
                menuImage.SetActive(false);
                rareMenu.SetActive(false);
                findsMenu.SetActive(false);
                commonMenu.SetActive(false);
                rareMenuButton.interactable = true;
                findsMenuButton.interactable = true;
                commonMenuButton.interactable = true;
                IsOpen = false;
            });
        }

        public void OpenCommonMenu()
        {
            rareMenu.SetActive(false);
            findsMenu.SetActive(false);
            commonMenu.SetActive(true);
            rareMenuButton.interactable = false;
            findsMenuButton.interactable = false;
            commonMenuButton.interactable = false;
            commonMenu.transform.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear).OnComplete(delegate
            {
                rareMenuButton.interactable = true;
                findsMenuButton.interactable = true;
            });
        }

        public void OpenRareMenu()
        {
            rareMenu.SetActive(true);
            findsMenu.SetActive(false);
            commonMenu.SetActive(false);
            rareMenuButton.interactable = false;
            findsMenuButton.interactable = false;
            commonMenuButton.interactable = false;
            rareMenu.transform.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear).OnComplete(delegate
            {
                commonMenuButton.interactable = true;
                findsMenuButton.interactable = true;
            });
        }

        public void OpenFindsMenu()
        {
            rareMenu.SetActive(false);
            findsMenu.SetActive(true);
            commonMenu.SetActive(false);
            rareMenuButton.interactable = false;
            findsMenuButton.interactable = false;
            commonMenuButton.interactable = false;
            findsMenu.transform.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear).OnComplete(delegate
            {
                rareMenuButton.interactable = true;
                commonMenuButton.interactable = true;
            });
        }
        
    }
