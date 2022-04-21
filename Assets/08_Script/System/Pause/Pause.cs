//======================================================================
// Pause.cs
//======================================================================
// 開発履歴
//
// 2022/03/23 author：奥田達磨 ポーズ機能実装
// 2022/03/25 author：奥田達磨 ポーズメニュー・レベルアップメニュー機能追加 
// 2022/04/25 author：奥田達磨 ポーズバグ修正 
// 2022/04/21 author：竹尾晃史郎 アイテム決定機能追加（ダブり発生どうしようか）
//
//======================================================================
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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

    // 4/21 追加（竹）
    [SerializeField] ItemManager itemManager;
    public Image SelectItemIcon_L, SelectItemIcon_C, SelectItemIcon_R;
    int setItem_L = 0, setItem_C = 0, setItem_R = 0;
    bool bLotteryComp = false;


    void Start()
    {
        //初期化
        nMenuFrame = 0;
        bPause = false;
        bLevelUpPause = false;
        bResume = true;

        // 4/21 追加
        itemManager = GameObject.FindWithTag("ItemManager").GetComponent<ItemManager>();
        
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
            // 4/21 追加
            //itemManager
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

        // LVUPしなくてもずっと呼ばれるためif追加（竹尾）
        if(b)
        {
            // アイテム番号割り当て（竹尾）
            if(bLotteryComp == false)
            {
                setItem_L = itemManager.nItemGacha(); // "L" eft
                setItem_C = itemManager.nItemGacha(); // "C" enter
                setItem_R = itemManager.nItemGacha(); // "R" ight

                SelectItemIcon_L.sprite = itemManager.setItemIcon(setItem_L);
                SelectItemIcon_C.sprite = itemManager.setItemIcon(setItem_C);
                SelectItemIcon_R.sprite = itemManager.setItemIcon(setItem_R);

                bLotteryComp = true;
            }
            // ****************************

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
                Debug.Log("左アイテム選択中");
                
                gLevelUpMenuChoice[nMenuFrame].SetActive(true);
                gLevelUpMenuChoice[nMenuFrame + 1].SetActive(false);
                gLevelUpMenuChoice[nMenuFrame + 2].SetActive(false);

                //決定処理（竹尾）
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    itemManager.nItemCount(setItem_L);
                    SetbLevelPause(false);
                    bLotteryComp = false;
                }
            }
            if (nMenuFrame == 1)
            {
                Debug.Log("中央アイテム選択中");
                
                gLevelUpMenuChoice[nMenuFrame].SetActive(true);
                gLevelUpMenuChoice[nMenuFrame - 1].SetActive(false);
                gLevelUpMenuChoice[nMenuFrame + 1].SetActive(false);

                //決定処理（竹尾）
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    itemManager.nItemCount(setItem_C);
                    SetbLevelPause(false);
                    bLotteryComp = false;
                }
            }
            if (nMenuFrame == 2)
            {
                Debug.Log("右アイテム選択中");
                
                gLevelUpMenuChoice[nMenuFrame].SetActive(true);
                gLevelUpMenuChoice[nMenuFrame - 1].SetActive(false);
                gLevelUpMenuChoice[nMenuFrame - 2].SetActive(false);

                //決定処理（竹尾）
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    itemManager.nItemCount(setItem_R);
                    SetbLevelPause(false);
                    bLotteryComp = false;
                }
            }
        }
       
    }
}