using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Participant : MonoBehaviour
{
    [SerializeField] protected List<Disk> Disks = new List<Disk>();

    [Header("Stats")]
    [SerializeField] protected int MaxHealth = 100;
    [SerializeField] protected int BaseDamage = 15;
    protected int Health;

    public event Action<int> OnHealthChanged;

    protected virtual void Start()
    {
        Health = MaxHealth;
    }
    
    // --------------------------------------------------
    public void TakeDamage(int damageDealt)
    {
        Health -= damageDealt;
        Health = Math.Clamp(Health, 0, MaxHealth);

        OnHealthChanged?.Invoke(Health);
        Debug.Log($" {name} took {damageDealt} damage.\n Health: {Health}/{MaxHealth}");
    }

    public void HealDamage(int damageHealed)
    {
        TakeDamage(-damageHealed);
    }

    // --------------------------------------------------
    public IEnumerator RotateDisksToSelected()
    {
        float rotationTime = 0f;

        foreach (Disk disk in Disks)
        {
            if (disk.IsRotationLocked())
                break;
            
            rotationTime = Mathf.Max(rotationTime, disk.TimeToRotate);
            disk.RotateToFront(disk.GetActiveSpell());
        }

        yield return new WaitForSeconds(rotationTime);
    }

    // --------------------------------------------------
    public List<Disk> GetDisks() { return Disks; }
    public int GetDamage()
    {
        return BaseDamage;
        //return (int)(BaseDamage * DamageMultiplier);
    }
    public int GetMaxHealth() { return MaxHealth; }
    public int GetCurrentHealth() { return Health; }
}