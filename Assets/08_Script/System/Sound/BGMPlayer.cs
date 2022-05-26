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

        // 現在シーン
        //Scene scene = SceneManager.GetSceneByName("Stage1-1");
        //SceneManager.SetActiveScene(scene);

    }

    private void Start()
    {
        // BossDirectionを読み込む
        //BossDirection = this.gameObject.GetComponent<BossDirection>();

        // ステージの切り替えを検知する為、「activeSceneChanged」にこの関数を入れる
        SceneManager.activeSceneChanged += ActiveSceneChanged;

        ChangeStageBGM();

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            Destroy(gameObject);
        }
    }

    // 特定シーンの切り替えを検知しBGMを変更 *******************
    void ChangeStageBGM()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Stage1-1":
                Stage1();
                break;
            case "Stage1-5":
                //BossDirection.StartDirection(1);
                break;

            case "Stage2-1":
                Stage2();
                break;
            case "Stage2-5":
                //BossDirection.StartDirection(2);
                break;

            case "Stage3-1":
                Stage3();
                break;
            case "Stage3-5":
                //BossDirection.StartDirection(3);
                break;

            case "Stage4-1":
                Stage4();
                break;
            case "Stage4-5":
                //BossDirection.StartDirection(4);
                break;

            case "Stage5-1":
                Stage5();
                break;
            case "Stage5-5":
                //BossDirection.StartDirection(5);
                break;

            case "Stage6-1":
                Stage6();
                break;
            case "Stage6-5":
               // BossDirection.StartDirection(6);
                break;

            case "Stage7-1":
                Stage7();
                break;
            case "Stage7-5":
                //BossDirection.StartDirection(7);
                break;

            default:
                Debug.Log("Continue BGM");
                break;
        }
    }

    // シーンの切り替えを検知 **********************************
    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        ChangeStageBGM();
    }
    //**********************************************************

    // BGM停止 *************************************************
    public void StopBGM()
    {
        Intro.clip = EnvSound_L.clip;
        Intro.Play();
        Loop.Stop();
        //EnvSound_L.Stop();
        //EnvSound_R.Stop();
    }
    //**********************************************************

    

    // BGM再開 *************************************************
    public void ReStartBGM()
    {
        Intro.Play();
        Loop.Play();
        //EnvSound_L.Play();
        //EnvSound_R.Play();
    }
    //**********************************************************

    // Planet1 *************************************************
    public void Stage1()
    {
        int num = 0;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num]);
    }

    public void Stage1_Boss()
    {
        int num = 0;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************

    // Planet2 *************************************************
    public void Stage2()
    {
        int num = 2;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num / 2]);
    }

    public void Stage2_Boss()
    {
        int num = 2;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************

    // Planet3 *************************************************
    public void Stage3()
    {
        int num = 4;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num / 2]);
    }

    public void Stage3_Boss()
    {
        int num = 4;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************

    // Planet4 *************************************************
    public void Stage4()
    {
        int num = 6;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num / 2]);
    }

    public void Stage4_Boss()
    {
        int num = 6;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************

    // Planet5 *************************************************
    public void Stage5()
    {
        int num = 8;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num / 2]);
    }

    public void Stage5_Boss()
    {
        int num = 8;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************


    // Planet6 *************************************************
    public void Stage6()
    {
        int num = 10;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num / 2]);
    }

    public void Stage6_Boss()
    {
        int num = 10;
        StartIntro_Boss(SoundData.BossBGMSoundList[num], num);
    }
    //**********************************************************

    // Planet7 *************************************************
    public void Stage7()
    {
        int num = 12;
        StartIntro(SoundData.StageBGMSoundList[num], num);
        PlayEnvSound(SoundData.EnvSoundList[num / 2]);
    }

    public void Stage7_Boss()
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
