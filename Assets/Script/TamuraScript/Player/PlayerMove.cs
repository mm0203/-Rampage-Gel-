//======================================================================
// PlayerMove.cs
//======================================================================
// 開発履歴
//
// 2022/03/01 author：松野将之 プレイヤーの移動作成(マウス)
// 2022/03/05 author：田村敏基 画面のどこを操作しても動くように大改造
// 2022/03/09 author：田村敏基 パッド操作実装
// 2022/03/09 author：田村敏基 移動方向を向くように変更
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 判定コンポーネントアタッチ
[RequireComponent(typeof(Rigidbody))]

public class PlayerMove : MonoBehaviour
{
    // 初速倍率
    [SerializeField] private float fInitial = 50.0f;
    // 減速率
    [SerializeField] private float fLate = 0.995f;

    private PlayerState state;
    private Rigidbody rb;

    // 発射方向
    [SerializeField] private LineRenderer Direction = null;
    // 発射方向の力
    private Vector3 vCurrentForce = Vector3.zero;
    // ドラッグ開始点
    private Vector3 vDragStart = Vector3.zero;

    // 蓄積時間
    private float fStockPower = 0;

    private bool bShot = false;

    void Start()
    {
        state = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!state.IsNormal)
        {
            fStockPower = 0;
            Direction.enabled = false;
            return;
        }

        // 動いてる方向を見る
        if (rb.velocity != new Vector3(0, 0, 0))
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }

        PadMove();
        KeyBoardMove();
        // 減速
        rb.velocity *= fLate;
    }

    // キーボード操作
    private void KeyBoardMove()
    {
        // 左クリック入力
        if (Input.GetMouseButton(0))
        {
            // 押されたとき
            if (Input.GetMouseButtonDown(0))
            {
                // マウスの初期位置を取得
                vDragStart = GetMousePosition();
                fStockPower = 0;
            }
            // 動かしたマウス座標の位置を取得
            var position = GetMousePosition();

            // マウスの初期座標と動かした座標の差分を取得
            vCurrentForce = vDragStart - position;

            // 動く方向を見る
            if (vCurrentForce != new Vector3(0, 0, 0))
            {
                transform.rotation = Quaternion.LookRotation(vCurrentForce);
            }

            // 矢印の引っ張り処理
            Direction.enabled = true;
            // 動く方向と逆に矢印が出るように
            Direction.SetPosition(0, rb.position);
            Direction.SetPosition(1, rb.position - vCurrentForce.normalized * 2);

            // マウスを押してる間、威力を高める
            if (fStockPower < 2)
            {
                fStockPower += Time.deltaTime;
            }
        }

        // 左クリック離れたとき
        if (Input.GetMouseButtonUp(0))
        {
            // 瞬間的に力を加えてはじく
            rb.AddForce(vCurrentForce.normalized * fStockPower * fInitial, ForceMode.Impulse);

            // 初期化
            fStockPower = 0;
            Direction.enabled = false;
        }
    }

    private void PadMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // スティックを倒してるなら
        if (Mathf.Abs(x) >= 0.01f || Mathf.Abs(y) >= 0.01f)
        {
            // フラグを立てる
            bShot = true;
            // 入力方向を逆にして受け取る
            vCurrentForce = new Vector3(-x * Time.deltaTime, 0, -y * Time.deltaTime);

            // 動く方向を見る
            transform.rotation = Quaternion.LookRotation(vCurrentForce);

            // 矢印の引っ張り処理
            Direction.enabled = true;
            // 動く方向と逆に矢印が出るように
            Direction.SetPosition(0, rb.position);
            Direction.SetPosition(1, rb.position - vCurrentForce.normalized * 2);

            fStockPower += Time.deltaTime;
            if (fStockPower < 2)
            {
                fStockPower += Time.deltaTime;
            }
        }
        else if (bShot == true)
        {
            // フラグを下す
            bShot = false;
            // 瞬間的に力を加えてはじく
            rb.AddForce(vCurrentForce.normalized * fStockPower * fInitial, ForceMode.Impulse);
            // 初期化
            fStockPower = 0;
            Direction.enabled = false;
        }
    }

    // マウス座標を3D座標に変換
    private Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
    }
}
