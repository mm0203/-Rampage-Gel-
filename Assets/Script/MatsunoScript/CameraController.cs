using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //プレイヤー情報格納用
    private GameObject player;
    // 相対距離取得用
    private Vector3 offset;

    void Start()
    {
        //　Playerの情報を取得
        this.player = GameObject.Find("Player");

        // カメラとPlayerとの相対距離を求める
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        //　新しいトランスフォームの値を代入
        transform.position = player.transform.position + offset;
    }
}
