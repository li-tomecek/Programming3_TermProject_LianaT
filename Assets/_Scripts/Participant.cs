using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public List<Disk> GetDisks() { return Disks; }
}
