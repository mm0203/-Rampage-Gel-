//======================================================================
// Title.cs
//======================================================================
// 開発履歴
//
// 2022/04/12 author：奥田達磨 タイトル実装
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] int nTitleFrame;
    [SerializeField] SceneObject sNextScene;
    [SerializeField] List<GameObject> gTitleMenuChoice;

    void Start()
    {
        //初期化
        nTitleFrame = 0;
    }
    void Update()
    {
        TitleMenu();
    }


    //タイトルメニュー
    public void TitleMenu()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            nTitleFrame++;
            if (nTitleFrame > 2)
                nTitleFrame = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            nTitleFrame--;
            if (nTitleFrame < 0)
                nTitleFrame = 2;
        }
        if (nTitleFrame == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                ChangeScene();
            gTitleMenuChoice[nTitleFrame].SetActive(true);
            gTitleMenuChoice[nTitleFrame + 1].SetActive(false);
            gTitleMenuChoice[nTitleFrame + 2].SetActive(false);
            
        }
        if (nTitleFrame == 1)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("ゲームやめますか？");
                Application.Quit();
            }
            gTitleMenuChoice[nTitleFrame].SetActive(true);
            gTitleMenuChoice[nTitleFrame - 1].SetActive(false);
            gTitleMenuChoice[nTitleFrame + 1].SetActive(false);
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sNextScene);
    }
}
