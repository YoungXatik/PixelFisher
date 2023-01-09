using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterValues : MonoBehaviour
{
    /*private void OnEnable()
    {
        EventManager.OnLengthValueChanged += UpdateLengthLevel;
        EventManager.OnStrengthValueChanged += UpdateStrengthLevel;
        EventManager.OnOfflineMoneyValueChanged += UpdateOfflineMoneyLevel;
    }

    private void OnDisable()
    {
        EventManager.OnLengthValueChanged -= UpdateLengthLevel;
        EventManager.OnStrengthValueChanged -= UpdateStrengthLevel;
        EventManager.OnOfflineMoneyValueChanged -= UpdateOfflineMoneyLevel;
    }*/

    private int _hookLengthLevel = 0;
    private int _hookStrengthLevel = 0;
    private int _offlineMoneyLevel = 0;
    

    private void Start()
    {
        if (PlayerPrefs.HasKey("HookLengthLevel"))
        {
            _hookLengthLevel = PlayerPrefs.GetInt("HookLengthLevel");
        }

        if (PlayerPrefs.HasKey("HookStrengthLevel"))
        {
            _hookStrengthLevel = PlayerPrefs.GetInt("HookStrengthLevel");
        }

        if (PlayerPrefs.HasKey("OfflineMoneyLevel"))
        {
            _offlineMoneyLevel = PlayerPrefs.GetInt("OfflineMoneyLevel");
        }
    }

    private void UpdateLengthLevel()
    {
        _hookLengthLevel++;
        PlayerPrefs.SetInt("HookLengthLevel", _hookLengthLevel);
    }

    private void UpdateStrengthLevel()
    {
        _hookStrengthLevel++;
        PlayerPrefs.SetInt("HookStrengthLevel", _hookStrengthLevel);
    }

    private void UpdateOfflineMoneyLevel()
    {
        _offlineMoneyLevel++;
        PlayerPrefs.SetInt("OfflineMoneyLevel",_offlineMoneyLevel);
    }

    [ContextMenu("DeletePrefs")]
    private void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
