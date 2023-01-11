using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyMissionSwap : MonoBehaviour
{
    [field: SerializeField] private float _msToWait;
    private float _trueMsToWait;
    private ulong _lastOpen;

    [SerializeField] private TextMeshProUGUI timerStatus;

    private List<GameObject> _spawnedDailyMissions = new List<GameObject>();

    [SerializeField] public List<GameObject> dailyMissionsList = new List<GameObject>();

    [SerializeField] private Transform dailyMissionsArea;

    private void Awake()
    {
        _trueMsToWait = _msToWait;
    }

    private void Start()
    {
        StartTick();
        _lastOpen = ulong.Parse(PlayerPrefs.GetString("LastOpen"));
        if (!PlayerPrefs.HasKey("DailyHookedRareFish") && !PlayerPrefs.HasKey("DailyHookedCurrentFish"))
        {
            InitializePrefabs();   
        }
    }

    public void StartTick()
    {
        if (!PlayerPrefs.HasKey("LastOpen"))
        {
            _lastOpen = (ulong) DateTime.Now.Ticks;
            PlayerPrefs.SetString("LastOpen", _lastOpen.ToString());
        }
    }

    [ContextMenu("Clear Saved Time")]
    private void ClearSavedTime()
    {
        PlayerPrefs.DeleteKey("LastOpen");
    }
    
    private bool IsReady()
    {
        ulong diff = ((ulong) DateTime.Now.Ticks - _lastOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float) (_msToWait - m) / 1000f;

        if (secondsLeft < 0)
        {
            RefreshDailyMissions();
            return true;
        }

        return false;
    }

    private void Update()
    {
        if (IsReady())
        {
            return;
        }
        ulong diff = ((ulong) DateTime.Now.Ticks - _lastOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float) (_msToWait - m) / 1000f;

        string t = "";

        t += ((int) secondsLeft / 3600).ToString() + "ч";
        secondsLeft -= ((int) secondsLeft / 3600) * 3600;
        t += ((int) secondsLeft / 60).ToString("00") + "м";
        t += ((int) secondsLeft % 60).ToString("00") + "с";

        timerStatus.text = t;
    }

    private void InitializePrefabs()
    {
        for (int i = 0; i < dailyMissionsList.Count; i++)
        {
            _spawnedDailyMissions.Add(Instantiate(dailyMissionsList[i], dailyMissionsArea));
        }
    }

    private void RefreshDailyMissions()
    {
        ClearSavedTime();
        StartTick();
        for (int i = 0; i < _spawnedDailyMissions.Count; i++)
        {
            Destroy(_spawnedDailyMissions[i].gameObject);
        }
        _spawnedDailyMissions.Clear();
        DeleteDailyMissionsPrefs();
        InitializePrefabs();
    }

    private void RefreshTimerStatus()
    {
        _msToWait = _trueMsToWait;
        _lastOpen = ulong.Parse(PlayerPrefs.GetString("LastOpen"));
        StartTick();
        
    }

    private void DeleteDailyMissionsPrefs()
    {
        PlayerPrefs.DeleteKey("DailyHookedRareFish");
        PlayerPrefs.DeleteKey("DailyHookedRareFishNeedValue");
        PlayerPrefs.DeleteKey("DailyHookedRareFishMoneyReward");
        PlayerPrefs.DeleteKey("DailyRareFishCollected");
        
        PlayerPrefs.DeleteKey("DailyHookedCurrentFish");
        PlayerPrefs.DeleteKey("CurrentFishIndex");
        PlayerPrefs.DeleteKey("DailyHookedCurrentFishNeedValue");
        PlayerPrefs.DeleteKey("DailyHookedCurrentFishMoneyReward");
        PlayerPrefs.DeleteKey("DailyCurrentFishCollected");
    }
}