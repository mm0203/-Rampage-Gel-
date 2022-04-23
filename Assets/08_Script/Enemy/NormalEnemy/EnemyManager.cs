//======================================================================
// EnemyManager.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵生成処理追加
// 2022/03/18 author：小椋駿 画面外に敵が生成するように
// 2022/03/28 author：小椋駿 敵のレベルアップ処理追加
// 2022/04/21 author：小椋駿 レベルをEnemyDataから取得するように変更
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

    // プレイヤーとどれだけ離れて生成するか
    [Header("生成距離")] [SerializeField] Vector2 vDistance = new Vector2(15.0f, 8.0f);
    Vector2 vInstantePos;

    // 敵の種類
    [SerializeField] List<GameObject> EnemyList;

    // 出現している敵のリスト
    public List<GameObject> NowEnemyList;

    // 敵のレベルアップ関連
    [Header("敵のレベルアップ秒数")] [SerializeField] float fLevelUpTime = 20.0f;
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

        // 出現範囲を設定
        vInstantePos = new Vector2(vDistance.x * 1.5f, vDistance.y * 1.5f);

        // 敵生成
        for (int i = 0; i < MaxEnemy; i++)
        {
            CreateEnemy();
        }

        
        //DontDestroyOnLoad(this.gameObject);
    }

    //---------------
    // 更新
    //---------------
    void Update()
    {
        // 減ったら新しく生成
        if (NowEnemyList.Count < MaxEnemy)
        {
            CreateEnemy();
        }

        // 時間に応じて敵のレベルアップ
        fLevelUpCount -= Time.deltaTime;
        if (fLevelUpCount < 0.0f)
        {
            // 初期化
            fLevelUpCount = fLevelUpTime;

            // 敵のレベルを上げる
            nEnemyLevel++;
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
        enemy.GetComponent<EnemyBase>().GetEnemyData.nLevel = nEnemyLevel;

        // マネージャ情報セット
        enemy.GetComponent<EnemyBase>().manager = gameObject.GetComponent<EnemyManager>();

        // プレイヤー情報セット
        enemy.GetComponent<EnemyBase>().player = player;

        // リストに追加
        NowEnemyList.Add(enemy);
    }

    //---------------
    // 座標計算
    //---------------
    private Vector3 CreatePos()
    {
        // プレイヤーの左端の位置を求める
        Vector2 tmpPos = new Vector2(player.transform.position.x - vInstantePos.x, player.transform.position.z - vInstantePos.y);

        // 出現位置をランダムに計算（プレイヤーの左端から右端の間で生成）
        Vector3 vPos = new Vector3(Random.Range(tmpPos.x, tmpPos.x + (vInstantePos.x * 2)), 0.5f, Random.Range(tmpPos.y, tmpPos.y + (vInstantePos.y * 2)));

        // プレイヤーとの距離を計算
        Vector3 vCreatePos = vPos - player.transform.position;

        // 画面外でなければ、もう一度計算  （TODO:あまりよくない。改善の余地あり）
        while ((vCreatePos.x < vDistance.x && vCreatePos.x > -vDistance.x) && (vCreatePos.y < vDistance.y && vCreatePos.y > -vDistance.y))
        {
            vPos = new Vector3(Random.Range(tmpPos.x, tmpPos.x + (vInstantePos.x * 2)), 0.5f, Random.Range(tmpPos.y, tmpPos.y + (vInstantePos.y * 2)));
            vCreatePos = vPos - player.transform.position;

            // 強制終了(無限ループに入らないように)
            debug++;
            if (debug > 100)
            {
                Debug.Log("適生成エラー");
                debug = 0;
                return vPos;
            }
        }

        return vPos;
    }
}
