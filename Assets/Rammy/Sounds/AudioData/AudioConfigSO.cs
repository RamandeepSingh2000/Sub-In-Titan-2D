using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/AudioData")]
public class AudioConfigSO : ScriptableObject
{
    public AudioClip clip;
    [Range(0, 1)]
    public float volume;
    [Range(0, 1)]
    public float pitch;
    public bool loop;

    public static void SetData(AudioConfigSO config, AudioSource source)
    {
        source.clip = config.clip;
        source.volume = config.volume;
        source.pitch = config.pitch;
        source.loop = config.loop;
    }
}
