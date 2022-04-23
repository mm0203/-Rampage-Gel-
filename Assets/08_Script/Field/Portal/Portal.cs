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

    // 0423 シーン管理
    public SceneData SceneData;
    [SerializeField] string sNowScene = "現在のシーン";

    private void Start()
    {
        DecideNextScene();
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
        m_nextScene = null;
        sNowScene = SceneManager.GetActiveScene().name;

        for(int i = 0; i <= SceneData.GameScene.Count; i++)
        {
            Debug.Log(SceneData.GameScene[i].m_SceneName);
            if(sNowScene == SceneData.GameScene[i].m_SceneName)
            {
                m_nextScene = SceneData.GameScene[i + 1];
                break;
            }
        }
        if (m_nextScene == null) Debug.LogError("次に遷移するシーンがありません");
    }
    //**********************************************************
}
