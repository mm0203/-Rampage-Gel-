//======================================================================
// SoundData.cs
//======================================================================
// ŠJ”­—š—ğ
//
// 2022/04/21 author:’|”öWj˜Y@§ì
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

    [Header("StageBGM")]
    public List<AudioClip> StageBGMSoundList = new List<AudioClip>();

    [Header("BossBGM")]
    public List<AudioClip> BossBGMSoundList = new List<AudioClip>();

    [Header("Environmental")]
    public List<AudioClip> EnvSoundList = new List<AudioClip>();

}
