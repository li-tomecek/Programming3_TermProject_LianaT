using DG.Tweening;
using UnityEngine;

public class Disk : MonoBehaviour
{
    [SerializeField] private float _timeToRotate = 0.6f;

    private SpellComponent[] _spellList;

    void Awake()
    {
        _spellList = gameObject.GetComponentsInChildren<SpellComponent>();
    }   

    public void RotateToFront(SpellPosition position)
    {
        if (position == SpellPosition.Left)
        {
            foreach (SpellComponent spell in _spellList)
                spell.RotateRight();

            gameObject.transform.DOLocalRotate(new Vector3(0f, 120f, 0f), _timeToRotate)
            .SetRelative(true)
            .OnUpdate(UpdateAllSprites);

        }
        else if (position == SpellPosition.Right)
        {
            foreach (SpellComponent spell in _spellList)
                spell.RotateLeft();

            gameObject.transform.DOLocalRotate(new Vector3(0f, -120f, 0f), _timeToRotate)
            .SetRelative(true)
            .OnUpdate(UpdateAllSprites);

        }
    }

    public void UpdateAllSprites()
    {
        foreach (SpellComponent spell in _spellList)
        {
            spell.gameObject.transform.LookAt(Camera.main.transform, Vector3.up);
        }
    }
    
}
