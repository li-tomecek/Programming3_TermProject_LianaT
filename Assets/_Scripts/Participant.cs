using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Participant : MonoBehaviour
{
    [SerializeField] protected List<Disk> Disks = new List<Disk>();
    [SerializeField] protected int Health = 100;

    public void TakeDamage(int damageDealt)
    {
        Health -= damageDealt;
        Health = Math.Max(0, Health);
    }
}
