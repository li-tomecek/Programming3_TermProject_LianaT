using System;
using System.Collections;
using UnityEngine;

public class Opponent : Participant
{
    public static Opponent Instance { get; private set; }

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

    public IEnumerator ChooseRotations()
    {
        float rotationTime = 0f;
        SpellComponent chosenSpell;

        foreach (Disk disk in Disks)
        {
            rotationTime = Math.Max(rotationTime, disk.TimeToRotate);

            do
            {
                chosenSpell = disk.gameObject.GetComponentsInChildren<SpellComponent>()[UnityEngine.Random.Range(0, 3)];

            } while (chosenSpell == disk.GetActiveSpell());

            disk.SetInteractable(true);
            disk.RotateToFront(chosenSpell);
            disk.SetInteractable(false);

        }

        yield return new WaitForSeconds(rotationTime);
    }


}
