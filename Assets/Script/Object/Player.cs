//======================================================================
// Player.cs
//======================================================================
// 開発履歴
//
// 2022/03/01 author：松野将之 プレイヤーの移動作成(マウス)
// 2022/03/05 aythor：田村敏基 画面のどこを操作しても動くように大改造
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 判定コンポーネントアタッチ
[RequireComponent(typeof(Rigidbody))]

public class Player : PlayerManager
{
    // 初速倍率
    [SerializeField] private float fInitial = 50.0f;
    // 減速率
    [SerializeField] private float fLate = 0.995f;

    // 発射方向
    [SerializeField] private LineRenderer Direction = null;
    // 発射方向の力
    private Vector3 vCurrentForce = Vector3.zero;
    // ドラッグ開始点
    private Vector3 vDragStart = Vector3.zero;

    // 蓄積時間
    private float StockPower = 0;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (!IsNormal) return;

        // 左クリック入力
        if (Input.GetMouseButton(0))
        {
            // 押されたとき
            if (Input.GetMouseButtonDown(0))
            {
                // マウスの初期位置を取得
                vDragStart = GetMousePosition();
                StockPower = 0;
            }
            // 動かしたマウス座標の位置を取得
            var position = GetMousePosition();

            // マウスの初期座標と動かした座標の差分を取得
            vCurrentForce = vDragStart - position;

            // 矢印の引っ張り処理
            Direction.enabled = true;
            // 動く方向と逆に矢印が出るように
            Direction.SetPosition(0, rb.position);
            Direction.SetPosition(1, rb.position - vCurrentForce.normalized * 2);

            // マウスを押してる間、威力を高める
            if(StockPower < 2)
            {
                StockPower += Time.deltaTime;
            }
        }

        // 左クリック離れたとき
        if (Input.GetMouseButtonUp(0))
        {
            // 瞬間的に力を加えてはじく
            rb.AddForce(vCurrentForce.normalized * StockPower * fInitial, ForceMode.Impulse);

            // 初期化
            StockPower = 0;
            Direction.enabled = false;
        }

        // 減速
        rb.velocity *= fLate;
    }

    // マウス座標を3D座標に変換
    private Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
    }




    //private bool b_shot = false;
    //private Vector2 StartPos;
    //private Vector2 EndPos;
    //private Vector2 Move;

    //// 初速倍率
    //[SerializeField] private float Initial_Vec = 10.0f;
    //// 減速率
    //[SerializeField] private float Decelerate = 0.9f;

    //protected override void Start()
    //{
    //    base.Start();
    //}

    //protected override void Update()
    //{
    //    base.Update();

    //    // Update初期処理
    //    First();

    //    // キーボード移動
    //    if (Input.GetMouseButton(0)) // 左クリック
    //    {
    //        // 弾を発射してない時
    //        if (!b_shot)
    //        {
    //            // マウスの初期位置を取得
    //            StartPos = Input.mousePosition;
    //        }

    //        b_shot = true;
    //    }
    //    else
    //    {
    //        // 弾を発射したら
    //        if (b_shot)
    //        {
    //            // マウスの移動した後の位置
    //            EndPos = Input.mousePosition;

    //            // 移動位置の差分を取得
    //            Move = (StartPos - EndPos).normalized;
    //            Debug.Log(Move);
    //            b_shot = false;
    //        }
    //    }

    //    // パッド移動

    //    // ハードモード

    //    // Update最終処理
    //    Last();
    //}

    //private void First()
    //{
    //    Move = Vector2.zero;
    //}

    //private void Last()
    //{
    //    // 2次元を3次元に変換
    //    var move = new Vector3(Move.x, 0.0f, Move.y);
    //    rb.AddForce(move * Initial_Vec, ForceMode.Impulse);
    //    rb.velocity *= Decelerate;
    //}

    //private Rigidbody rb = null;
    //// 発射方向
    //[SerializeField]
    //private LineRenderer Direction = null;
    //// 最大付与力量
    //private const float fMaxMagnitude = 2.0f;
    //// 発射方向の力
    //private Vector3 vCurrentForce = Vector3.zero;
    //// メインカメラ
    //private Camera MainCamera = null;
    //// メインカメラ座標
    //private Transform MainCameraTransform = null;
    //// ドラッグ開始点
    //private Vector3 vDragStart = Vector3.zero;

    //// 初期化処理
    //public void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();

    //    // メインカメラの情報を取得
    //    MainCamera = Camera.main;
    //    MainCameraTransform = MainCamera.transform;
    //}

    //// マウス座標をワールド座標に変換して取得
    //private Vector3 GetMousePosition()
    //{
    //    // Z座標を補間
    //    var position = Input.mousePosition;
    //    position.z = MainCameraTransform.position.z;
    //    position = MainCamera.ScreenToWorldPoint(position);

    //    return position;
    //}

    //// マウスクリック開始
    //public void OnMouseDown()
    //{
    //    // マウスの初期位置を取得
    //    vDragStart = GetMousePosition();

    //    // 矢印の引っ張り処理
    //    Direction.enabled = true;
    //    Direction.SetPosition(0, rb.position);
    //    Direction.SetPosition(1, rb.position);
    //}

    //// マウスクリック中の処理
    //public void OnMouseDrag()
    //{
    //    // 動かしたマウス座標の位置を取得
    //    var position = GetMousePosition();

    //    // マウスの初期座標と動かした座標の差分を取得
    //    vCurrentForce = position - vDragStart;

    //    // 2点間の距離を比較
    //    if (vCurrentForce.magnitude > fMaxMagnitude * fMaxMagnitude)
    //    {
    //        vCurrentForce *= fMaxMagnitude / vCurrentForce.magnitude;
    //    }

    //    // 動く方向と逆に矢印が出るように
    //    Direction.SetPosition(0, rb.position - vCurrentForce);
    //    Direction.SetPosition(1, rb.position);
    //}

    //// マウスを話した時の処理
    //public void OnMouseUp()
    //{
    //    Direction.enabled = false;

    //    // 慣性
    //    Flip(vCurrentForce * 3.0f);
    //}

    //// プレイヤーを弾く
    //public void Flip(Vector3 force)
    //{
    //    // 瞬間的に力を加えてはじく
    //    rb.AddForce(force, ForceMode.Impulse);
    //}
}
