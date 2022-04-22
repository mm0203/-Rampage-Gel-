//======================================================================
// BGMPlayer.cs
//======================================================================
// 開発履歴
//
// 2022/04/21 author:竹尾晃史郎　制作
//                               BGMのみイントロ→ループできるようにする
//                               繋ぎが甘い(音源の問題？)
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public SoundData SoundData;

    public AudioSource Intro;
    public AudioSource Loop;
    public AudioSource EnvSound;


    private void Start()
    {
        DontDestroyOnLoad(this);

        //確認用
        Stage1();
    }

    void Stage1()
    {
        int num = 0;
        StartIntro(SoundData.StageBGMSoundList[num], num);
    }

    void Stage1_Boss()
    {
        int num = 0;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }

    void Stage2()
    {
        int num = 2;
        StartIntro(SoundData.StageBGMSoundList[num], num);
    }

    void Stage2_Boss()
    {
        int num = 2;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }

    void Stage3()
    {
        int num = 4;
        StartIntro(SoundData.StageBGMSoundList[num], num);
    }

    void Stage3_Boss()
    {
        int num = 4;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }

    void Stage4()
    {
        int num = 6;
        StartIntro(SoundData.StageBGMSoundList[num], num);
    }

    void Stage4_Boss()
    {
        int num = 6;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }

    void Stage5()
    {
        int num = 8;
        StartIntro(SoundData.StageBGMSoundList[num], num);
    }

    void Stage5_Boss()
    {
        int num = 8;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }

    void Stage6()
    {
        int num = 10;
        StartIntro(SoundData.StageBGMSoundList[num], num);
    }

    void Stage6_Boss()
    {
        int num = 10;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }

    void Stage7()
    {
        int num = 12;
        StartIntro(SoundData.StageBGMSoundList[num], num);
    }

    void Stage7_Boss()
    {
        int num = 12;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }

    // BGN再生 *************************************************
    void PlayEnvSound()
    {
        EnvSound.clip = null;
    }

    void StartIntro(AudioClip clip, int listnum)
    {
        Intro.clip = null;
        Loop.clip = null;
        Intro.clip = clip;
        Intro.Play();
        StartCoroutine(Checking(Intro, listnum));
    }

    void ChangeLoopBGM(int Intronum)
    {
        Loop.clip = SoundData.StageBGMSoundList[Intronum + 1];
        Loop.Play();
        Loop.loop = true;
    }
    //**********************************************************

    // BossBGM再生 *********************************************
    void StartIntro_Boss(AudioClip clip, int listnum)
    {
        Intro.clip = null;
        Loop.clip = null;
        Intro.clip = clip;
        Intro.Play();
        StartCoroutine(Checking_Boss(Intro, listnum));
    }

    void ChangeLoopBGM_Boss(int Intronum)
    {
        Loop.clip = SoundData.BossBGMSoundList[Intronum + 1];
        Loop.Play();
        Loop.loop = true;
    }
    //**********************************************************



    // 音終了判定とコンポーネント削除
    private IEnumerator Checking(AudioSource audio, int num)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                ChangeLoopBGM(num);
                break;
            }
        }
    }

    // Boss用
    private IEnumerator Checking_Boss(AudioSource audio, int num)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                ChangeLoopBGM_Boss(num);
                break;
            }
        }
    }
}
