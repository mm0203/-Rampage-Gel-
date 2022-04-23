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
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour
{
    public SoundData SoundData;

    public AudioSource Intro;
    public AudioSource Loop;
    public AudioSource EnvSound_L;
    public AudioSource EnvSound_R;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Stage1();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("Stage2");
        }

        
    }

    // Planet1 *************************************************
    void Stage1()
    {
        int num = 0;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num]);
    }

    void Stage1_Boss()
    {
        int num = 0;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************

    // Planet2 *************************************************
    void Stage2()
    {
        int num = 2;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num / 2]);
    }

    void Stage2_Boss()
    {
        int num = 2;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************

    // Planet3 *************************************************
    void Stage3()
    {
        int num = 4;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num / 2]);
    }

    void Stage3_Boss()
    {
        int num = 4;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************

    // Planet4 *************************************************
    void Stage4()
    {
        int num = 6;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num / 2]);
    }

    void Stage4_Boss()
    {
        int num = 6;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************

    // Planet5 *************************************************
    void Stage5()
    {
        int num = 8;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num / 2]);
    }

    void Stage5_Boss()
    {
        int num = 8;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************


    // Planet6 *************************************************
    void Stage6()
    {
        int num = 10;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num / 2]);
    }

    void Stage6_Boss()
    {
        int num = 10;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************

    // Planet7 *************************************************
    void Stage7()
    {
        int num = 12;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num / 2]);
    }

    void Stage7_Boss()
    {
        int num = 12;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************

    // BGN再生 *************************************************
    void PlayEnvSound(AudioClip clip)
    {
        EnvSound_L.clip = EnvSound_R.clip = null;
        EnvSound_L.clip = EnvSound_R.clip = clip;
        EnvSound_L.loop = EnvSound_R.loop = true;
        EnvSound_L.Play(); EnvSound_R.Play();
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
