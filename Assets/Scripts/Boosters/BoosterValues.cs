using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoosterValues : MonoBehaviour
{
    private int _hookLengthLevel = 0;
    private int _hookStrengthLevel = 0;
    private int _offlineMoneyLevel = 0;
    

    private void Start()
    {
        if (PlayerPrefs.HasKey("HookLengthLevel"+ SceneManager.GetActiveScene().name))
        {
            _hookLengthLevel = PlayerPrefs.GetInt("HookLengthLevel"+ SceneManager.GetActiveScene().name);
        }

        if (PlayerPrefs.HasKey("HookStrengthLevel"+ SceneManager.GetActiveScene().name))
        {
            _hookStrengthLevel = PlayerPrefs.GetInt("HookStrengthLevel" + SceneManager.GetActiveScene().name);
        }

        if (PlayerPrefs.HasKey("OfflineMoneyLevel"+ SceneManager.GetActiveScene().name))
        {
            _offlineMoneyLevel = PlayerPrefs.GetInt("OfflineMoneyLevel" + SceneManager.GetActiveScene().name);
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
