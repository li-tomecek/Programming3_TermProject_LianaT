using System;
using UnityEngine;

public class CombatEffects : MonoBehaviour
{
    [Header("Hit Effects")]
    [SerializeField] ParticleSystem _darkAttack;
    [SerializeField] ParticleSystem _holyAttack;
    [SerializeField] ParticleSystem _arcaneAttack;

    [SerializeField] SoundEffect _darkSFX;
    [SerializeField] SoundEffect _holySFX;
    [SerializeField] SoundEffect _arcaneSFX;

    [Header("DamageIndicator")]
    [SerializeField] DamageIndicator _damageIndicatorPrefab;
    [SerializeField] Canvas _parentCanvas;
    [SerializeField] Color _healthColor, _damageColor;
    [SerializeField] int _poolAmount = 3;
    [SerializeField] SoundEffect _healSFX;
   
    [SerializeField] Transform _playerDamageSpawn, _opponentDamageSpawn;
    private ObjectPool _indicatorPool;

    void Start()
    {
        _indicatorPool = new ObjectPool(_damageIndicatorPrefab.gameObject, _poolAmount, _parentCanvas.gameObject);
        
        Player.Instance.OnHealthChanged += (int amount) => {
            ShowDamageIndicator(amount, _playerDamageSpawn);
            if(amount > 0) 
                AudioManager.Instance.PlaySFX(_healSFX);
        };
        Opponent.Instance.OnHealthChanged += (int amount) => {
            ShowDamageIndicator(amount, _opponentDamageSpawn);
            if(amount > 0) 
                AudioManager.Instance.PlaySFX(_healSFX);
        };
    }

    public void PlayAttackEffect(Vector3 targetPosition, SpellType spellType)
    {
        switch (spellType)
        {
            case SpellType.Holy:
            _holyAttack.gameObject.transform.position = targetPosition;
            _holyAttack.Play();
            AudioManager.Instance.PlaySFX(_holySFX);
            break;

            case SpellType.Dark:
            _darkAttack.gameObject.transform.position = targetPosition;
            _darkAttack.Play();
             AudioManager.Instance.PlaySFX(_darkSFX);
            break;

            case SpellType.Arcane:
            _arcaneAttack.gameObject.transform.position = targetPosition;
            _arcaneAttack.Play();
            AudioManager.Instance.PlaySFX(_arcaneSFX);
            break;
        }
    }  
    public void ShowDamageIndicator(int amount, Transform target)
    {
        Color color = (amount > 0) ? _healthColor : _damageColor;
        _indicatorPool.GetActivePooledObject().GetComponent<DamageIndicator>().ShowIndicatorAtTarget(Math.Abs(amount).ToString(), target, color);
    }

}


