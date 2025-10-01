using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Participant : MonoBehaviour
{
    [SerializeField] protected List<Disk> Disks = new List<Disk>();

    [Header("Stats")]
    [SerializeField] protected int MaxHealth = 100;
    [SerializeField] protected int BaseDamage = 15;
    protected int Health;
    protected float DamageMultiplier = 1f;

    public event Action<int> OnHealthChanged;

    protected virtual void Start()
    {
        Health = MaxHealth;
    }
    // --------------------------------------------------
    public void TakeDamage(int damageDealt)
    {
        Health -= damageDealt;
        Health = Math.Max(0, Health);

        OnHealthChanged?.Invoke(Health);
    }

    public void HealDamage(int damageHealed)
    {
        TakeDamage(-damageHealed);
    }
    // --------------------------------------------------
    public List<Disk> GetDisks() { return Disks; }
    public int GetTotalDamage()
    {
        return (int)(BaseDamage * DamageMultiplier);
    }
    public int GetMaxHealth() { return MaxHealth; }
}