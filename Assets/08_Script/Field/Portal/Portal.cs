//======================================================================
// SceneObject.cs
//======================================================================
// 開発履歴
//
// 2022/04/01 author：松野将之 ポータルのシーン遷移
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

    // プレイヤーと接触でシーン遷移
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // シーン時間　1秒
            FadeManager.Instance.LoadScene("TesrScene", 1.0f);
        }
    }
}
