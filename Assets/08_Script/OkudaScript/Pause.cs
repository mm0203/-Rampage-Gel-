//======================================================================
// Pause.cs
//======================================================================
// 開発履歴
//
// 2022/03/23 author：奥田達磨 ポーズ機能実装
// 2022/03/25 author：奥田達磨 ポーズメニュー・レベルアップメニュー機能追加 
// 2022/04/25 author：奥田達磨 ポーズバグ修正 
//
//======================================================================
using UnityEngine;
using System.Collections.Generic;

public class Pause : MonoBehaviour
{

    //ポーズ切り替え用フラグ
    [SerializeField] int  nMenuFrame;
    [SerializeField] bool bPause;
    [SerializeField] bool bResume;
    [SerializeField] bool bLevelUpPause;
    [SerializeField] GameObject gPauseMenu;
    [SerializeField] GameObject gLevelUpPause;
    [SerializeField] List<GameObject> gPauseMenuChoice;
    [SerializeField] List<GameObject> gLevelUpMenuChoice;


    void Start()
    {
        //初期化
        nMenuFrame = 0;
        bPause = false;
        bLevelUpPause = false;
        bResume = true;
        
    }
    void Update()
    {
        //Pキー押したら
        //===============================
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (bResume)
            {
                SetbPause(true);
            }
            else
            {
                SetbPause(false);
            }
        }
        //ポーズメニュー
        if (bPause)
        {
            PauseMenu(bPause);
        }
        else
        {
            PauseMenu(bPause);
        }
        //-------------------------------------

        //Lキー押したら
        //=====================================
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (bResume)
            {
                SetbLevelPause(true);
            }
            else
            {
                SetbLevelPause(false);
            }
        }
        //レベルアップ時のポーズメニュー
        if(bLevelUpPause)
        {
            levelUpPause(bLevelUpPause);
        }
        else
        {
            levelUpPause(bLevelUpPause);
        }
        //------------------------------------------ 

        if (bLevelUpPause || bPause)
        {
            Stop();
        }
        else
        {
            Resume();
        }

        //============================================
        //バグ防止（2つのフラグが同じになることを防ぐ）
        //============================================
        if (bPause && bResume)
            SetbResume(false);
        if (!bPause && !bResume)
            SetbResume(true);
        if (bResume && bLevelUpPause)
            SetbResume(false);
        //if (!bResume && !bLevelUpPause)
        //    SetbResume(true);
        if (bPause && bLevelUpPause)
            SetbPause(false);
        //if (!bPause && !bLevelUpPause)
        //    SetbResume(true);
        //--------------------------------------------

    }
    
    //時間停止
    private void Stop()
    {
        Time.timeScale = 0;  // 時間停止
    }

    //再開
    private void Resume()
    {
        Time.timeScale = 1;  // 再開
    }

    private void SetbPause(bool b)
    {
        bPause = b;
    }
    private void SetbLevelPause(bool b)
    {
        bLevelUpPause = b;
    }
    private void SetbResume(bool b)
    {
        bResume = b;
    }
    //ポーズメニュー
    public void PauseMenu(bool b)
    {
        gPauseMenu.SetActive(b);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            nMenuFrame++;
            if (nMenuFrame > 1)
                nMenuFrame = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            nMenuFrame--;
            if (nMenuFrame < 0)
                nMenuFrame = 1;
        }
        if (nMenuFrame == 0)
        {
            gPauseMenuChoice[nMenuFrame].SetActive(true);
            gPauseMenuChoice[nMenuFrame + 1].SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SetbPause(false);               
            }
        }
        if (nMenuFrame == 1)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("ゲームやめますか？");
                Application.Quit();
            }
            gPauseMenuChoice[nMenuFrame].SetActive(true);
            gPauseMenuChoice[nMenuFrame - 1].SetActive(false);
        }
    }
    //レヴェルアップしたときに呼んでね(テストで一応Lキー押したら動くようにしときます)
    //レベルアップメニュー
    public void levelUpPause(bool b)
    {
        gLevelUpPause.SetActive(b);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nMenuFrame++;
            if (nMenuFrame > 2)
                nMenuFrame = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            nMenuFrame--;
            if (nMenuFrame < 0)
                nMenuFrame = 2;
        }
        if (nMenuFrame == 0)
        {
            gLevelUpMenuChoice[nMenuFrame].SetActive(true);
            gLevelUpMenuChoice[nMenuFrame + 1].SetActive(false);
            gLevelUpMenuChoice[nMenuFrame + 2].SetActive(false);
        }
        if (nMenuFrame == 1)
        {
            gLevelUpMenuChoice[nMenuFrame].SetActive(true);
            gLevelUpMenuChoice[nMenuFrame - 1].SetActive(false);
            gLevelUpMenuChoice[nMenuFrame + 1].SetActive(false);
        }
        if(nMenuFrame == 2)
        {
            gLevelUpMenuChoice[nMenuFrame].SetActive(true);
            gLevelUpMenuChoice[nMenuFrame - 1].SetActive(false);
            gLevelUpMenuChoice[nMenuFrame - 2].SetActive(false);
        }
    }
}