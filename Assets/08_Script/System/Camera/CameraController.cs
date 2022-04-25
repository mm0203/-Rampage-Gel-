//======================================================================
// CameraController.cs
//======================================================================
// 開発履歴
//
// 2022/03/14 author：松野将之 カメラのプレイヤー追従機能実装
//                             フリーカメラ機能実装
// 2022/04/25 author：竹尾晃史郎　カメラ演出用のスイッチ仮追加
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //カメラの移動量
    [SerializeField, Range(0.1f, 60.0f)]
    private float PositionStep = 90.0f;

    //マウス感度
    [SerializeField, Range(30.0f, 300.0f)]
    private float MouseSensitive = 90.0f;

    //カメラのtransform  
    private Transform CameraTransform;

    //マウスの始点 
    private Vector3 StartMousePos;

    //カメラ回転の始点情報
    private Vector3 PresentCamRotation;
    private Vector3 PresentCamPos;

    //プレイヤー情報格納用
    private GameObject Player;

    // 相対距離取得用
    private Vector3 Offset;

    // 0425 カメラ演出オンオフ
    public bool bOnDirection = false;

    // カメラの状態
    // private bool bCameraMode = false;

    void Start()
    {
        CameraTransform = this.gameObject.transform;

        //　Playerの情報を取得
        this.Player = GameObject.Find("Player");

        // カメラとPlayerとの相対距離を求める
        Offset = transform.position - Player.transform.position;
    }

    void Update()
    {
        // プレイヤー追従
        if(bOnDirection == false)
        transform.position = Player.transform.position + Offset;

        //カメラの回転
        // CameraRotationMouseControl();

        //カメラのローカル移動
        // CameraPositionKeyControl();  
    }


    //カメラの回転
    private void CameraRotationMouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartMousePos = Input.mousePosition;
            PresentCamRotation.x = CameraTransform.transform.eulerAngles.x;
            PresentCamRotation.y = CameraTransform.transform.eulerAngles.y;
        }

        if (Input.GetMouseButton(0))
        {
            //(移動開始座標 - マウスの現在座標) / 解像度 で正規化
            float x = (StartMousePos.x - Input.mousePosition.x) / Screen.width;
            float y = (StartMousePos.y - Input.mousePosition.y) / Screen.height;

            //回転開始角度 ＋ マウスの変化量 * マウス感度
            float eulerX = PresentCamRotation.x + y * MouseSensitive;
            float eulerY = PresentCamRotation.y + x * MouseSensitive;

            CameraTransform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        }
    }

    //カメラのローカル移動 キー
    private void CameraPositionKeyControl()
    {
        Vector3 campos = CameraTransform.position;

        // 右
        if (Input.GetKey(KeyCode.J)) { campos += CameraTransform.right * Time.deltaTime * PositionStep; }
        // 左
        if (Input.GetKey(KeyCode.L)) { campos -= CameraTransform.right * Time.deltaTime * PositionStep; }
        // 上
        if (Input.GetKey(KeyCode.I)) { campos += CameraTransform.up * Time.deltaTime * PositionStep; }
        // 下
        if (Input.GetKey(KeyCode.K)) { campos -= CameraTransform.up * Time.deltaTime * PositionStep; }
        // ズームイン
        if (Input.GetKey(KeyCode.Space)) { campos += CameraTransform.forward * Time.deltaTime * PositionStep; }
        // ズームアウト
        if (Input.GetKey(KeyCode.LeftShift)) { campos -= CameraTransform.forward * Time.deltaTime * PositionStep; }

        CameraTransform.position = campos;
    }
}
