//======================================================================
// PlayerMove.cs
//======================================================================
// 開発履歴
//
// 2022/03/01 author：松野将之 プレイヤーの移動作成(マウス)
// 2022/03/05 author：田村敏基 画面のどこを操作しても動くように大改造
// 2022/03/09 author：田村敏基 パッド操作実装
// 2022/03/09 author：田村敏基 移動方向を向くように変更
// 2022/03/25 author：田村敏基 アニメーション実装
// 2022/03/27 author：田村敏基 updateの最初にアニメーションを持ってくるよう変更
// 2022/03/28 author：竹尾　応急 エフェクト発生組み込み
// 2022/05/02                    音組み込み
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 判定コンポーネントアタッチ
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CameraShaker))]

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private LineRenderer Direction = null;  // 発射方向
    [SerializeField] private Animator anime;
    [SerializeField] private GameObject effectmove;
    private PlayerState state;
    private PlayerStatus status;
    private Rigidbody rb;
    private CameraShaker shaker;
    private SoundManager sound;

    private Vector3 vCurrentForce = Vector3.zero; // 発射方向の力   
    private Vector3 vDragStart = Vector3.zero; // ドラッグ開始点
    
    [Header("発射威力")]
    [SerializeField] private float fInitial = 100.0f; // 初速倍率
    [Header("減速率")]
    [SerializeField] private float fLate = 0.85f; // 減速率
    [Header("最大威力に到達する時間")]
    [SerializeField] private float fInputTime = 0.8f;
    [SerializeField] private float fStockPower = 0; // 蓄積時間
    private float fTimeToMove = 999.0f; // Time.deltaTimeを使う場合
    //private int nTimeToMove = 999;    // フレームを使う場合
    [SerializeField] private float fDistance = 0; // ステータスのスピードと連動 //上昇量0.02
    //[SerializeField] private int nDistance = 0; // ステータスのスピードと連動 //上昇量1
    private bool bShot = false;

 
    

    //*応急* エフェクトスクリプト
    [SerializeField] AID_PlayerEffect effect;
    

    void Start()
    {
        state = GetComponent<PlayerState>();
        status = GetComponent<PlayerStatus>();
        rb = GetComponent<Rigidbody>();
        shaker = GetComponent<CameraShaker>();
        effectmove.SetActive(false);

        Direction.enabled = false;
    }

    void Update()
    {
        // 無いコンポーネントを入れなおす
        SetComponent();

        // Y軸固定
        FixedtoY();

        // アニメーション
        MoveAnim();
        // ステート管理
        IsState(!state.IsNormal);
        // 方向転換
        LookToMove(rb.velocity);
        // 移動
        PadMove();
        KeyBoardMove();

        MoveBrake();

    }

    // アニメーション ******************************************
    void MoveAnim()
    {
        anime.SetFloat("pull", fStockPower);
        anime.SetFloat("blowway", rb.velocity.magnitude);
    }
    //**********************************************************

    // 状態 ****************************************************
    void IsState(bool state)
    {
        if (state)
        {
            fStockPower = 0;
            Direction.enabled = false;
            effectmove.SetActive(false);
            return;
        }
    }
    //**********************************************************

    // 移動方向に向く ******************************************
    void LookToMove(Vector3 vector)
    {
        if (vector != new Vector3(0, 0, 0))
        {
            transform.rotation = Quaternion.LookRotation(vector);
        }
    }
    //**********************************************************

    // マウス座標を3D座標に変換 ********************************
    private Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
    }
    //**********************************************************

    // ブレーキ処理 ********************************************
    void MoveBrake()
    {
        // float Time.deltaTimeを使用する例
        if (fTimeToMove > fDistance)
        {
            // 減速
            rb.velocity *= fLate;
        }
        else
        {
            fTimeToMove += Time.deltaTime;
        }

        // int フレームを利用する例
        //if (nTimeToMove > nDistance)
        //{
        //    rb.velocity *= fLate;
        //}
        //else
        //{
        //    nTimeToMove ++;
        //}
    }
    //**********************************************************

    // キーボード操作 ******************************************
    private void KeyBoardMove()
    {
        // 左クリック入力
        if (Input.GetMouseButton(0))
        {
            // 押されたとき
            if (Input.GetMouseButtonDown(0))
            {
                if (state.IsNormal) sound.Play_PlayerCharge(this.gameObject);
                vDragStart = GetMousePosition(); // マウスの初期位置を取得
                fStockPower = 0;
            }
            
            var position = GetMousePosition(); // 動かしたマウス座標の位置を取得            
            vCurrentForce = vDragStart - position; // マウスの初期座標と動かした座標の差分を取得
            
            LookToMove(vCurrentForce); // 動く方向を見る

            // 矢印の引っ張り処理
            Direction.enabled = true;
            // 動く方向と逆に矢印が出るように
            Direction.SetPosition(0, rb.position); 
            Direction.SetPosition(1, rb.position - vCurrentForce.normalized * 2);

            // マウスを押してる間、威力を高める
            if (fStockPower < fInputTime)
            {
                fStockPower += Time.deltaTime;
            }
            else
            {
                fStockPower = fInputTime;
            }

        }

        // 左クリック離れたとき
        if (Input.GetMouseButtonUp(0))
        {
            if (state.IsNormal) sound.Play_PlayerShotWeek(this.gameObject);

            // 瞬間的に力を加えてはじく
            rb.AddForce(vCurrentForce.normalized * fStockPower * fInitial, ForceMode.Impulse);
            status.fBreakTime = 0.0f;
            vCurrentForce = Vector3.zero;
            effectmove.SetActive(true);
            

            // 初期化
            fStockPower = 0;
            Direction.enabled = false;
            effect.StartEffect(0, this.gameObject, 1.0f);


            fTimeToMove = 0;
            //nTimeToMove = 0;
        }
    }
    //**********************************************************

    private void PadMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // スティックを倒してるなら
        if (Mathf.Abs(x) >= 0.5f || Mathf.Abs(y) >= 0.5f)
        {
            if (state.IsNormal && bShot == true) sound.Play_PlayerCharge(this.gameObject);

            // フラグを立てる
            bShot = true;
            // 入力方向を逆にして受け取る
            vCurrentForce = new Vector3(-x * 1000, 0, -y * 1000);

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
            if (state.IsNormal) sound.Play_PlayerShotWeek(this.gameObject);

            // フラグを下す
            bShot = false;
            // 瞬間的に力を加えてはじく
            rb.AddForce(vCurrentForce.normalized * fStockPower * fInitial, ForceMode.Impulse);
            effectmove.SetActive(true);
            // 初期化
            fStockPower = 0;
            status.fBreakTime = 0.0f;
            vCurrentForce = Vector3.zero;
            Direction.enabled = false;

            //*応急*
            effect.StartEffect(0, this.gameObject, 1.0f);

            fTimeToMove = 0;
            //nTimeToMove = 0;
        }
    }
    //**********************************************************

    // SoundManagerを入れる ************************************
    void SetComponent()
    {
        if(sound == null)
        {
            sound = GameObject.FindWithTag("SoundPlayer").GetComponent<SoundManager>();
        }
    }
    //**********************************************************

    // Y軸固定 *************************************************
    void FixedtoY()
    {
        
    }

    //*応急*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            shaker.Do();
        }

    }
}