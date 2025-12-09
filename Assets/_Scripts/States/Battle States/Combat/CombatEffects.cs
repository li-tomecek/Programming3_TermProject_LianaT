using System;
using TMPro;
using UnityEngine;

public class CombatEffects : MonoBehaviour
{
    [Header("Hit Effects")]
    [SerializeField] ParticleSystem _darkAttack;
    [SerializeField] ParticleSystem _holyAttack;
    [SerializeField] ParticleSystem _arcaneAttack;

    [Header("DamageIndicator")]
    [SerializeField] DamageIndicator _damageIndicatorPrefab;
    [SerializeField] Canvas _parentCanvas;
    [SerializeField] Color _healthColor, _damageColor;
    [SerializeField] int _poolAmount = 3;
    private ObjectPool _indicatorPool;

    void Start()
    {
        _indicatorPool = new ObjectPool(_damageIndicatorPrefab.gameObject, _poolAmount, _parentCanvas.gameObject);
        
        Player.Instance.OnHealthChanged += (int amount) => {ShowDamageIndicator(amount, Player.Instance.transform);};
        Opponent.Instance.OnHealthChanged += (int amount) => {ShowDamageIndicator(amount, Opponent.Instance.transform);};
    }

    public void PlayAttackEffect(Vector3 targetPosition, SpellType spellType)
    {
        switch (spellType)
        {
            case SpellType.Holy:
            _holyAttack.gameObject.transform.position = targetPosition;
            _holyAttack.Play();
            break;

            case SpellType.Dark:
            _darkAttack.gameObject.transform.position = targetPosition;
            _darkAttack.Play();
            break;

            case SpellType.Arcane:
            _arcaneAttack.gameObject.transform.position = targetPosition;
            _arcaneAttack.Play();
            break;
        }
    }  
    public void ShowDamageIndicator(int amount, Transform target)
    {
        Color color = (amount > 0) ? _healthColor : _damageColor;
        _indicatorPool.GetActivePooledObject().GetComponent<DamageIndicator>().ShowIndicatorAtTarget(Math.Abs(amount).ToString(), target, color);
    }
}


