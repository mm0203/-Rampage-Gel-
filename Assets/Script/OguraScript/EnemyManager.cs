//======================================================================
// EnemyManager.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵生成処理追加
// 2022/03/18 author：小椋駿 画面外に敵が生成するように
// 2022/03/28 author：小椋駿 敵のレベルアップ処理追加
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 重複禁止
[DisallowMultipleComponent]

public class EnemyManager : MonoBehaviour
{
    // 敵の最大数
    [Header("敵の数のMAX")] [SerializeField] int MaxEnemy = 2;

    // 出現範囲
    [Header("敵の出現座標範囲")] [SerializeField, Range(1.0f, 100.0f)] float InstantiateX = 6.5f;
    [SerializeField, Range(1.0f, 100.0f)] float InstantiateZ = 3.5f;

    // プレイヤーとどれだけ離れて生成するか
    [Header("生成距離")] [SerializeField] Vector2 vDistance = new Vector2(10.0f, 5.0f);

    // 敵の種類
    [SerializeField] List<GameObject> EnemyList;
    // 出現している敵のリスト
    public List<GameObject> NowEnemyList;
    // 敵生成の時間
    float fCreateTime = 1.0f;

    // 敵のレベルアップ関連
    [Header("敵のレベルアップ秒数")][SerializeField]float fLevelUpTime = 20.0f;
    float fLevelUpCount;
    int nEnemyLevel = 0;

    GameObject player;
    GameObject enemy;

    int debug = 0;


    //---------------
    // 初期化
    //---------------
    void Start()
    {
        player = GameObject.Find("Player");
        fLevelUpCount = fLevelUpTime;

        // 敵生成
        for (int i = 0; i < MaxEnemy;i++)
        {
            CreateEnemy();
        }
    }

    //---------------
    // 更新
    //---------------
    void Update()
    {
        // 減ったら新しく生成
        if (NowEnemyList.Count < MaxEnemy)
        {
            // 1秒毎に生成(仮)
            fCreateTime -= Time.deltaTime;
            if(fCreateTime < 0.0f)
            {
                CreateEnemy();
                fCreateTime = 1.0f;
            }
        }

        // 時間に応じて敵のレベルアップ
        fLevelUpCount -= Time.deltaTime;
        if(fLevelUpCount < 0.0f)
        {
            // 初期化
            fLevelUpCount = fLevelUpTime;

            // 敵のレベルを上げる
            nEnemyLevel++;

            Debug.Log(nEnemyLevel);
        }

    }

    //---------------
    // 敵を生成
    //---------------
    private void CreateEnemy()
    {
        // 敵情報取得
        enemy = Instantiate(EnemyList[Random.Range(0, EnemyList.Count)], CreatePos(), Quaternion.identity);

        // 敵レベルセット
        enemy.GetComponent<StatusComponent>().Level = nEnemyLevel;

        enemy.GetComponent<EnemyBase>().SetManager(gameObject.GetComponent<EnemyManager>());
        enemy.GetComponent<EnemyBase>().SetPlayer(player);
        NowEnemyList.Add(enemy);
    }

    //---------------
    // 座標計算
    //---------------
    private Vector3 CreatePos()
    {
        // 出現位置をランダムに計算
        Vector3 vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), 0.5f, Random.Range(-InstantiateZ, InstantiateZ));

        // プレイヤーとの距離を計算
        Vector3 vCreatePos = vPos - player.transform.position;

        // 画面外でなければ、もう一度計算
        while ((vCreatePos.x < vDistance.x && vCreatePos.x > -vDistance.x) && (vCreatePos.y < vDistance.y && vCreatePos.y > -vDistance.y))
        {
            vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), 0.5f, Random.Range(-InstantiateZ, InstantiateZ));
            vCreatePos = vPos - player.transform.position;

            // 強制終了(無限ループに入らないように)
            debug++;
            if(debug > 100)
            {
                Debug.Log("適生成エラー");
                debug = 0;
                return vPos;
            }
        }

        return vPos;
    }
}
