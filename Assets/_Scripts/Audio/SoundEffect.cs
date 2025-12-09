using Unity.VisualScripting;
using UnityEngine;
/*
    Based on Luc Rancourt's SFX.cs
*/
[CreateAssetMenu(fileName = "SoundEffect", menuName = "ScriptableObjects/SoundEffect")]
public class SoundEffect : ScriptableObject
{
    [field: SerializeField] public AudioClip Clip { get; private set; }
    [field: SerializeField, Range(0.0f, 2.0f)] public float Volume { get; private set; } = 1.0f;
    [field: SerializeField, Range(0.1f, 2.0f)] public float Pitch { get; private set; } = 1.0f;
    [field: SerializeField] public bool Loop {get; private set;} = false;
}
