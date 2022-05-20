//======================================================================
// Boss03.cs
//======================================================================
// 開発履歴
//
// 2022/05/11 author 竹尾：エルダーマジシャン制作
//                         アニメーションイベントに起因して起こす
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_03 : MonoBehaviour
{
    // プレイヤー
    private GameObject player;
    // ボスの基底クラス
    private EnemyBase BossBase;
    // FireWallの範囲オブジェクト
    public GameObject BurstCircle;
    // エフェクト関連
    EnemyEffect enemyEffect;
    GameObject objEffect;

    [SerializeField] private GameObject Circle;

    // サークルサイズ
    [Header("予測サークルのサイズ")] [SerializeField] private Vector3 vCircleSize = new Vector3(0.5f, 0.5f, 0.5f);

    // 火柱サイズ
    [Header("火柱サイズ")] [SerializeField] private Vector3 vFireSize = new Vector3(0.5f, 3.0f, 0.5f);

    // 衝撃波サイズ
    [Header("衝撃波最小サイズ")] [SerializeField] private Vector3 vBurstMinSize = new Vector3(0.1f, 0.1f, 0.1f);
    [Header("衝撃波最大サイズ")] [SerializeField] private Vector3 vBurstMaxSize = new Vector3(20.0f, 0.1f, 20.0f);
    public bool IsBurst = false;

    public int nCircleInterval = 45;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        BossBase = GetComponent<EnemyBase>();

        // エフェクト取得
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;
        BurstCircle.SetActive(false);
    }

    

    // 連続火柱 ************************************************
    // アニメーションイベントをつけまくる
    private void FirePillarAttack()
    {
        GameObject cube;

        // 当たり判定用キューブ
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // 当たり判定用キューブを非表示
        cube.GetComponent<MeshRenderer>().enabled = false;

        // 火柱のサイズ、座標、角度設定
        cube.transform.localScale = vFireSize;
        transform.Rotate(-90.0f, 0.0f, 0.0f);
        cube.transform.position = player.transform.position;

        // 火柱コンポーネント調整
        // すり抜ける判定にする
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // 必要な情報をセット
        Circle.transform.localScale = vCircleSize;

        // Fire.cs追加
        cube.AddComponent<Fire>();

        // 攻撃サークルセット
        cube.GetComponent<Fire>().SetCircle(Circle);

        // 敵情報セット
        cube.GetComponent<Fire>().enemy = gameObject;

        // プレイヤー情報セット
        cube.GetComponent<Fire>().player = player;

        // プレイヤー情報セット
        cube.GetComponent<Fire>().effect = enemyEffect;
    }
    //**********************************************************



    // 連続衝撃波 **********************************************
    void StartBurst()
    {
        if(IsBurst == false)
        {
            IsBurst = true;
            StartCoroutine(BurstWave(1));
            Debug.Log("発動");
        }
        
    }

    private IEnumerator BurstWave(int atkCount)
    {
        for(int n = 0; n <= atkCount; n++)
        {
            Debug.Log(n);
            // 衝撃波オブジェクト生成
            GameObject circle;
            circle = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

            // 衝撃波のサイズ、座標、角度設定
            circle.transform.localScale = vBurstMinSize;
            circle.transform.rotation = this.transform.rotation;
            circle.transform.position = this.transform.position;
            circle.GetComponent<CapsuleCollider>().isTrigger = true;

            // BurstCircle追加、情報セット
            circle.AddComponent<BurstCircle>();
            BurstCircle burstcircle = circle.GetComponent<BurstCircle>();
            burstcircle.SetPlayer(BossBase.GetComponent<EnemyBase>().player);
            burstcircle.SetEnemy(gameObject);

            float sizePercent;

            // 広がる衝撃波
            for (int i = 0; i <= nCircleInterval; i++)
            {

                yield return null;
                sizePercent = (float)i / nCircleInterval;
                circle.transform.localScale = Vector3.Lerp(vBurstMinSize, vBurstMaxSize, sizePercent);

            }

            // 余韻
            for (int i = 0; i <= 20; i++)
            {
                yield return null;

            }

            Destroy(circle);
        }

        // 余韻
        for (int i = 0; i <=　60; i++)
        {
            yield return null;

        }

        IsBurst = false;
    }
    //**********************************************************
}



