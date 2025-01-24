using System;
using Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SoundType
{
    EnemyDeath,
    EnemyHit,
    PlayerDeath,
    PlayerHit,
}

[ExecuteInEditMode]
public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private SoundData[] soundData;
    [SerializeField] private AudioSource audioSource;

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundData, names.Length);
        for (int i = 0; i < soundData.Length; i++)
        {
            soundData[i].name = names[i];
        }
    }
#endif
    

    public void PlaySound(SoundType soundType)
    {
        SoundData data = soundData[(int) soundType];
        AudioClip randomClip = data.sounds[Random.Range(0, data.sounds.Length)];
        audioSource.clip = randomClip;
        if (data.randomizedPitch)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
        }
        audioSource.volume = data.volume;
        audioSource.transform.position = Camera.main.transform.position;
        audioSource.PlayOneShot(randomClip);
    }
    
    public void PlaySpatialSound(SoundType soundType, Vector3 position)
    {
        audioSource.transform.position = position;
        PlaySound(soundType);
    }
}

[Serializable]
public class SoundData
{
    [HideInInspector] public string name;
    public AudioClip[] sounds;
    public bool randomizedPitch;
    public float volume = 1f;
}
