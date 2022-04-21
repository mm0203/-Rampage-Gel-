//======================================================================
// SoundData.cs
//======================================================================
// �J������
//
// 2022/04/21 author:�|���W�j�Y�@����
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreateSoundData")]

public class SoundData : ScriptableObject
{
    [Header("<SoundList>")]
    [Header("Player")]
    public List<AudioClip> PlayerSoundList = new List<AudioClip>();

    [Header("Enemy")]
    public List<AudioClip> EnemySoundList = new List<AudioClip>();

    [Header("System")]
    public List<AudioClip> SystemSoundList = new List<AudioClip>();

    [Header("BGM")]
    public List<AudioClip> BGMSoundList = new List<AudioClip>();
    
}
