//======================================================================
// EnemyManager.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵生成処理追加
// 2022/03/18 author：小椋駿 画面外に敵が生成するように
// 2022/03/28 author：小椋駿 敵のレベルアップ処理追加
// 2022/04/21 author：小椋駿 レベルをEnemyDataから取得するように変更
// 2022/04/26 author：小椋駿 画面外生成処理を変更
// 2022/04/28 author：小椋駿 GenerateDataからデータを取得するように変更
//                           Planet1にしか対応していないため、直す必要あり
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 重複禁止
[DisallowMultipleComponent]

public class EnemyManager : MonoBehaviour
{
    // 敵の最大数
    [Header("敵の数 MIN,MAX")]
    [SerializeField] Vector2Int vEnemyNum = new Vector2Int(0, 20);

    // 敵の最大数
    float fChangeNumCount = 0.0f;

    // 敵の最大数保存用
    int nTmpMaxNum;

    [Header("生成距離")]
    [SerializeField] float fDistance = 20.0f;

    [Header("敵のレベルアップ速度(秒)")]
    [SerializeField] float fLevelUpTime = 20.0f;

    // 敵の種類
    [SerializeField] List<GameObject> EnemyList;

    // 出現している敵のリスト
    public List<GameObject> NowEnemyList;

    // レベルアップカウント
    float fLevelUpCount;

    // 現在の敵のレベル
    public int nEnemyLevel = 1;

    // ボス出現中か
    public bool bBoss { get; set; } = false; 

    GameObject player;
    GameObject enemy;
    public GameObject Boss;

    [Header("ジェネレートデータ")]
    [SerializeField] GenerateEnemyData GenerateEnemyData;

    //---------------
    // 初期化
    //---------------
    void Start()
    {
        player = GameObject.Find("Player");
        fLevelUpCount = fLevelUpTime;
        

        // 敵初期化
        InitEnemy();

        // シーン切替検知
        SceneManager.activeSceneChanged += SceneChanged;


        DontDestroyOnLoad(this.gameObject);
    }

    //---------------
    // 更新
    //---------------
    void Update()
    {

        // 敵の最大数の増減
        ChangeNum();

        // 敵レベルアップ
        LevelUp();

        

        // 減ったら新しく生成
        if (NowEnemyList.Count < vEnemyNum.y)
        {
            CreateEnemy();
        }

        RemoveNullData();
    }

    //---------------
    // 敵を生成
    //---------------
    private void CreateEnemy()
    {
        if (bBoss) return;

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
        // 1/2の確率で負の値にする
        int random = Random.Range(0, 1);
        if (random == 0) fDistance *= -1;

        // 画面外の座標取得
        Vector3 vPos = Camera.main.ViewportToWorldPoint(new Vector3(fDistance, 0.0f, Camera.main.nearClipPlane));

        // Z座標をずらす
        vPos.z = Random.Range(player.transform.position.z - 5.0f, player.transform.position.z + 5.0f);

        return vPos;
    }

    //---------------
    // レベルアップ処理
    //---------------
    void LevelUp()
    {
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


    //-----------------------
    // 敵の数を調整
    //-----------------------
    void ChangeNum()
    {
        // カウント増加
        fChangeNumCount += Time.deltaTime * 0.1f;

        // 敵の最大数の増減
        vEnemyNum.y = (int)Mathf.Abs(nTmpMaxNum * Mathf.Sin(fChangeNumCount));
    }


    //-----------------------
    // シーンごとに初期化
    //-----------------------
    private void InitEnemy()
    {
        bBoss = false;

        // 現在シーンの取得
        Scene scene = SceneManager.GetActiveScene();

        // 現在シーンのビルド番号取得
        int nSceneNo = scene.buildIndex;

        //Debug.Log(nSceneNo);

        //// 敵の最大数をGenerateから取得   *TODO* Planet1しか対応していないため変更必須
        //vEnemyNum.y = GenerateEnemyData.Planet1[nSceneNo].MaxEnemy;

        //// 出現する敵リスト取得   *TODO* Planet1しか対応していないため変更必須
        //EnemyList = GenerateEnemyData.Planet1[nSceneNo].EnemyList;

        switch(nSceneNo)
        {
            case 1:
                vEnemyNum.y = GenerateEnemyData.Planet1[nSceneNo-1].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet1[nSceneNo-1].EnemyList;
                Boss = GenerateEnemyData.Planet1[nSceneNo - 1].BossEnemy;
                break;
            case 2:
                vEnemyNum.y = GenerateEnemyData.Planet1[nSceneNo-1].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet1[nSceneNo-1].EnemyList;
                Boss = GenerateEnemyData.Planet1[nSceneNo - 1].BossEnemy;
                break;
            case 3:
                vEnemyNum.y = GenerateEnemyData.Planet1[nSceneNo-1].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet1[nSceneNo-1].EnemyList;
                Boss = GenerateEnemyData.Planet1[nSceneNo - 1].BossEnemy;
                break;
            case 4:
                vEnemyNum.y = GenerateEnemyData.Planet1[nSceneNo - 1].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet1[nSceneNo - 1].EnemyList;
                Boss = GenerateEnemyData.Planet1[nSceneNo - 1].BossEnemy;
                break;
            case 5:
                vEnemyNum.y = GenerateEnemyData.Planet1[nSceneNo - 1].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet1[nSceneNo - 1].EnemyList;
                Boss = GenerateEnemyData.Planet1[nSceneNo - 1].BossEnemy;
                break;

            case 6:
                vEnemyNum.y = GenerateEnemyData.Planet2[nSceneNo-6].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet2[nSceneNo-6].EnemyList;
                Boss = GenerateEnemyData.Planet2[nSceneNo - 6].BossEnemy;
                break;
            case 7:
                vEnemyNum.y = GenerateEnemyData.Planet2[nSceneNo - 6].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet2[nSceneNo - 6].EnemyList;
                Boss = GenerateEnemyData.Planet2[nSceneNo - 6].BossEnemy;
                break;
            case 8:
                vEnemyNum.y = GenerateEnemyData.Planet2[nSceneNo - 6].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet2[nSceneNo - 6].EnemyList;
                Boss = GenerateEnemyData.Planet2[nSceneNo - 6].BossEnemy;
                break;
            case 9:
                vEnemyNum.y = GenerateEnemyData.Planet2[nSceneNo - 6].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet2[nSceneNo - 6].EnemyList;
                Boss = GenerateEnemyData.Planet2[nSceneNo - 6].BossEnemy;
                break;
            case 10:
                vEnemyNum.y = GenerateEnemyData.Planet2[nSceneNo - 6].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet2[nSceneNo - 6].EnemyList;
                Boss = GenerateEnemyData.Planet2[nSceneNo - 6].BossEnemy;
                break;

            case 11:
                vEnemyNum.y = GenerateEnemyData.Planet3[nSceneNo - 11].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet3[nSceneNo - 11].EnemyList;
                Boss = GenerateEnemyData.Planet3[nSceneNo - 11].BossEnemy;
                break;
            case 12:
                vEnemyNum.y = GenerateEnemyData.Planet3[nSceneNo - 11].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet3[nSceneNo - 11].EnemyList;
                Boss = GenerateEnemyData.Planet3[nSceneNo - 11].BossEnemy;
                break;
            case 13:
                vEnemyNum.y = GenerateEnemyData.Planet3[nSceneNo - 11].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet3[nSceneNo - 11].EnemyList;
                Boss = GenerateEnemyData.Planet3[nSceneNo - 11].BossEnemy;
                break;
            case 14:
                vEnemyNum.y = GenerateEnemyData.Planet3[nSceneNo - 11].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet3[nSceneNo - 11].EnemyList;
                Boss = GenerateEnemyData.Planet3[nSceneNo - 11].BossEnemy;
                break;
            case 15:
                vEnemyNum.y = GenerateEnemyData.Planet3[nSceneNo - 11].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet3[nSceneNo - 11].EnemyList;
                Boss = GenerateEnemyData.Planet3[nSceneNo - 11].BossEnemy;
                break;

            case 16:
                vEnemyNum.y = GenerateEnemyData.Planet4[nSceneNo - 16].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet4[nSceneNo - 16].EnemyList;
                Boss = GenerateEnemyData.Planet4[nSceneNo - 16].BossEnemy;
                break;
            case 17:
                vEnemyNum.y = GenerateEnemyData.Planet4[nSceneNo - 16].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet4[nSceneNo - 16].EnemyList;
                Boss = GenerateEnemyData.Planet4[nSceneNo - 16].BossEnemy;
                break;
            case 18:
                vEnemyNum.y = GenerateEnemyData.Planet4[nSceneNo - 16].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet4[nSceneNo - 16].EnemyList;
                Boss = GenerateEnemyData.Planet4[nSceneNo - 16].BossEnemy;
                break;
            case 19:
                vEnemyNum.y = GenerateEnemyData.Planet4[nSceneNo - 16].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet4[nSceneNo - 16].EnemyList;
                Boss = GenerateEnemyData.Planet4[nSceneNo - 16].BossEnemy;
                break;
            case 20:
                vEnemyNum.y = GenerateEnemyData.Planet4[nSceneNo - 16].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet4[nSceneNo - 16].EnemyList;
                Boss = GenerateEnemyData.Planet4[nSceneNo - 16].BossEnemy;
                break;

            case 21:
                vEnemyNum.y = GenerateEnemyData.Planet5[nSceneNo - 21].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet5[nSceneNo - 21].EnemyList;
                Boss = GenerateEnemyData.Planet5[nSceneNo - 21].BossEnemy;
                break;
            case 22:
                vEnemyNum.y = GenerateEnemyData.Planet5[nSceneNo - 21].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet5[nSceneNo - 21].EnemyList;
                Boss = GenerateEnemyData.Planet5[nSceneNo - 21].BossEnemy;
                break;
            case 23:
                vEnemyNum.y = GenerateEnemyData.Planet5[nSceneNo - 21].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet5[nSceneNo - 21].EnemyList;
                Boss = GenerateEnemyData.Planet5[nSceneNo - 21].BossEnemy;
                break;
            case 24:
                vEnemyNum.y = GenerateEnemyData.Planet5[nSceneNo - 21].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet5[nSceneNo - 21].EnemyList;
                Boss = GenerateEnemyData.Planet5[nSceneNo - 21].BossEnemy;
                break;
            case 25:
                vEnemyNum.y = GenerateEnemyData.Planet5[nSceneNo - 21].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet5[nSceneNo - 21].EnemyList;
                Boss = GenerateEnemyData.Planet5[nSceneNo - 21].BossEnemy;
                break;

            case 26:
                vEnemyNum.y = GenerateEnemyData.Planet6[nSceneNo - 26].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet6[nSceneNo - 26].EnemyList;
                Boss = GenerateEnemyData.Planet6[nSceneNo - 26].BossEnemy;
                break;
            case 27:
                vEnemyNum.y = GenerateEnemyData.Planet6[nSceneNo - 26].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet6[nSceneNo - 26].EnemyList;
                Boss = GenerateEnemyData.Planet6[nSceneNo - 26].BossEnemy;
                break;
            case 28:
                vEnemyNum.y = GenerateEnemyData.Planet6[nSceneNo - 26].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet6[nSceneNo - 26].EnemyList;
                Boss = GenerateEnemyData.Planet6[nSceneNo - 26].BossEnemy;
                break;
            case 29:
                vEnemyNum.y = GenerateEnemyData.Planet6[nSceneNo - 26].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet6[nSceneNo - 26].EnemyList;
                Boss = GenerateEnemyData.Planet6[nSceneNo - 26].BossEnemy;
                break;
            case 30:
                vEnemyNum.y = GenerateEnemyData.Planet6[nSceneNo - 26].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet6[nSceneNo - 26].EnemyList;
                Boss = GenerateEnemyData.Planet6[nSceneNo - 26].BossEnemy;
                break;

            case 31:
                vEnemyNum.y = GenerateEnemyData.Planet7[nSceneNo - 31].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet7[nSceneNo - 31].EnemyList;
                Boss = GenerateEnemyData.Planet7[nSceneNo - 31].BossEnemy;
                break;
            case 32:
                vEnemyNum.y = GenerateEnemyData.Planet7[nSceneNo - 31].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet7[nSceneNo - 31].EnemyList;
                Boss = GenerateEnemyData.Planet7[nSceneNo - 31].BossEnemy;
                break;
            case 33:
                vEnemyNum.y = GenerateEnemyData.Planet7[nSceneNo - 31].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet7[nSceneNo - 31].EnemyList;
                Boss = GenerateEnemyData.Planet7[nSceneNo - 31].BossEnemy;
                break;
            case 34:
                vEnemyNum.y = GenerateEnemyData.Planet7[nSceneNo - 31].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet7[nSceneNo - 31].EnemyList;
                Boss = GenerateEnemyData.Planet7[nSceneNo - 31].BossEnemy;
                break;
            case 35:
                vEnemyNum.y = GenerateEnemyData.Planet7[nSceneNo - 31].MaxEnemy;
                EnemyList = GenerateEnemyData.Planet7[nSceneNo - 31].EnemyList;
                Boss = GenerateEnemyData.Planet7[nSceneNo - 31].BossEnemy;
                break;

            default:
                Debug.LogError("存在しないシーン : " + nSceneNo );
                break;
        }

        // 敵の最大数保存
        nTmpMaxNum = vEnemyNum.y;
    }

    //-----------------------
    // リスト整理
    //-----------------------
    void RemoveNullData()
    {
        if(NowEnemyList.Count > 0)
        {
            for (int i = 0; i < NowEnemyList.Count; i++)
            {
                if (NowEnemyList[i] == null)
                {
                    Debug.Log("リストから削除");
                    NowEnemyList.Remove(NowEnemyList[i]);
                }
            }
        }
        
    }

    //-----------------------
    // シーン切替時、初期化処理
    //-----------------------
    void SceneChanged(Scene thisScene, Scene nextScene)
    {
        InitEnemy();
    }
}
