//======================================================================
// SceneObject.cs
//======================================================================
// 開発履歴
//
// 2022/04/01 author：松野将之 ポータルのシーン遷移
// 2022/04/23 author：竹尾晃史郎　シーン追加が面倒なためSceneData追加
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // 次のシーン
    public SceneObject m_nextScene;   
    public SceneData SceneData;        // 0423追加
    string sNowScene = "現在のシーン"; // 0423追加

    private void Start()
    {
        DecideNextScene(); // 0423追加
    }

    // プレイヤーと接触でシーン遷移
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // シーン時間　1秒
            FadeManager.Instance.LoadScene(m_nextScene, 1.0f);
        }
    }



    // 0423 シーン決定 *****************************************
    void DecideNextScene()
    {
        m_nextScene = null;                             // 前のデータが残らないよう消す
        sNowScene = SceneManager.GetActiveScene().name; // 今のシーンの名前をとる

        for(int i = 0; i <= SceneData.GameScene.Count; i++)
        {
            if(sNowScene == SceneData.GameScene[i].m_SceneName)
            {
                m_nextScene = SceneData.GameScene[i + 1]; // 次のシーンを当てる
                break;                                    // 次のシーンが解れば後はいい
            }
        }

        if (m_nextScene == null) 
            Debug.LogError("これは最終ステージ？");
    }
    //**********************************************************
}
