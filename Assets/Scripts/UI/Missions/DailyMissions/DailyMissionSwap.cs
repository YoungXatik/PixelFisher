using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyMissionSwap : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI status;

    private List<GameObject> _spawnedDailyMissions = new List<GameObject>();

    [SerializeField] public List<GameObject> dailyMissionsList = new List<GameObject>();

    private float _refreshCooldown = 24f;
    private float _refreshDeadLine = 48f;

    [SerializeField] private Transform dailyMissionsArea;

    private DateTime _lastEnterData;
    private DateTime _dateTimeToRefresh;


    private void InitializePrefabs()
    {
        for (int i = 0; i < dailyMissionsList.Count; i++)
        {
            _spawnedDailyMissions.Add(Instantiate(dailyMissionsList[i],dailyMissionsArea));      
        }
    }

    private void Start()
    {
        _dateTimeToRefresh.TimeOfDay.Add(new TimeSpan(24, 0, 0));
        status.text = _dateTimeToRefresh.ToString();
    }

    [ContextMenu("TestTime")]
    private void AddTime()
    {
        
    }
}
