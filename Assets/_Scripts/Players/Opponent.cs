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
            SpellComponent[] spells = disk.gameObject.GetComponentsInChildren<SpellComponent>();

            //For now, the opponent will randomly choose one of the non-active spells for each disk.
            //Enemy AI would go here
            
            if (disk.IsRotationLocked())
                break;
            do
            {
                chosenSpell = spells[UnityEngine.Random.Range(0, spells.Length)];

            } while (chosenSpell == disk.GetActiveSpell());

            disk.SetTargetable(true);
            disk.RotateToFront(chosenSpell);
            disk.SetTargetable(false);

        }

        yield return new WaitForSeconds(rotationTime);
    }


}
