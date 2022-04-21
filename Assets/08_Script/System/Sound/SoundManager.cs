//======================================================================
// SoundManager.cs
//======================================================================
// 開発履歴
//
// 2022/04/21 author:竹尾晃史郎　制作
//                               SEあまりにも再生しすぎると重くなる
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 音データリスト
    public SoundData SoundData;

    private void Update()
    {
        // デバック用（一気に再生するとクソ重くなる）
        //if(Input.GetKeyDown(KeyCode.U))
        //{
        //    Play_PlayerBound(this.gameObject);
        //    Play_PlayerBurst(this.gameObject);
        //    Play_PlayerBurstShot(this.gameObject);
        //    Play_PlayerCharge(this.gameObject);
        //    Play_PlayerDamage(this.gameObject);
        //    Play_PlayerDamageatGuardA(this.gameObject);
        //    Play_PlayerGetItem(this.gameObject);
        //    Play_PlayerGuard(this.gameObject);
        //    Play_PlayerGuardBreak(this.gameObject);
        //    Play_PlayerGuardRecover(this.gameObject);
        //    Play_PlayerLVUP(this.gameObject);
        //    Play_PlayerShotWeek(this.gameObject);
        //    Play_PlayerStageChange(this.gameObject);
        //    Play_SystemDecide(this.gameObject);
        //    Play_SystemSelect(this.gameObject);
        //}
    }

    // PlayerのSE ***********************************************
    public void Play_PlayerBound(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[0]);
    }

    public void Play_PlayerBurst(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[1]);
    }

    public void Play_PlayerBurstShot(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[2]);
    }

    public void Play_PlayerCharge(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[3]);
    }

    public void Play_PlayerDamage(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[4]);
    }

    public void Play_PlayerDamageatGuardA(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[5]);
    }

    public void Play_PlayerGetItem(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[6]);
    }

    public void Play_PlayerGuard(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[7]);
    }

    public void Play_PlayerGuardBreak(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[8]);
    }

    public void Play_PlayerGuardRecover(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[9]);
    }

    public void Play_PlayerLVUP(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[10]);
    }

    public void Play_PlayerShotWeek(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[11]);
    }

    public void Play_PlayerStageChange(GameObject player)
    {
        PlaySE(player, SoundData.PlayerSoundList[12]);
    }
    //**********************************************************
    
    // EnemyのSE ***********************************************
    public void Play_EnemyDamage(GameObject enemy)
    {
        PlaySE(enemy, SoundData.EnemySoundList[0]);
    }

    public void Play_EnemyDown(GameObject enemy)
    {
        PlaySE(enemy, SoundData.EnemySoundList[1]);
    }
    //**********************************************************

    // SystemのSE **********************************************
    public void Play_SystemDecide(GameObject system)
    {
        PlaySE(system, SoundData.SystemSoundList[0]);
    }

    public void Play_SystemSelect(GameObject system)
    {
        PlaySE(system, SoundData.SystemSoundList[1]);
    }
    //**********************************************************




    // SE再生
    void PlaySE(GameObject obj, AudioClip clip)
    {
        AudioSource audioSource;
        audioSource = obj.AddComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
        StartCoroutine(Checking(audioSource));
    }

    // 音終了判定とコンポーネント削除
    private IEnumerator Checking(AudioSource audio)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                
                Destroy(audio);
                break;
            }
        }
    }
}
