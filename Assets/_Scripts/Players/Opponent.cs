using System;
using System.Collections;
using UnityEngine;

public class Opponent : Participant
{
    public static Opponent Instance { get; private set; }
    private IAIStrategy _currentStrategy;

    [Header("AI Strategy")]
    [Range(0, 1)][SerializeField] float _aggressiveHealthThreshold = 0.6f;   //The minimum percent of max health for the AI to be aggressive
    [Range(0, 1)][SerializeField] float _defensiveHealthThreshold = 0.3f;   //The maximum percent of max health for the AI to be defensive
    

    void Awake()                //Because of inheritance, cannot use the singleton class. Maybe this could be changed later.
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChooseSpells()
    {
        UpdateStrategy();

        foreach (Disk disk in Disks)
        {
            if (disk.IsRotationLocked())
                break;

            disk.SetActiveSpell(_currentStrategy.ChooseSpell(disk, disk.GetOpposingDisk().FindSpellAtFront()));
        }
    }

    public void UpdateStrategy()
    {
        float healthValue = Health / (float)MaxHealth;

        if (healthValue >= _aggressiveHealthThreshold)
            _currentStrategy = (_currentStrategy is AggressiveStrategy) ? _currentStrategy :  new AggressiveStrategy();

        else if (healthValue <= _defensiveHealthThreshold)
            _currentStrategy = (_currentStrategy is DefensiveStrategy) ? _currentStrategy :  new DefensiveStrategy();

        else if (!(_currentStrategy is RandomStrategy))
            _currentStrategy =   new RandomStrategy();

        Debug.Log($"Chose the {_currentStrategy} at {healthValue * 100f}% health.");
    }


}
