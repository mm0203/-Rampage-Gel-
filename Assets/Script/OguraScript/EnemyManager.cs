//======================================================================
// Player.cs
//======================================================================
// 開発履歴
//
// 2022/03/02 author：松野将之 〇〇作成
// 2022/03/03 author：奥田達磨 〇〇の処理追加
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

    // 敵の種類
    [SerializeField] List<GameObject> EnemyList;
    // 出現している敵のリスト
    public List<GameObject> NowEnemyList;


    GameObject player;
    //public GameObject GetPlayer { get { return player; } }

    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        for(int i = 0; i < MaxEnemy;i++)
        {
            CreateEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 減ったら新しく生成
        if (NowEnemyList.Count < MaxEnemy)
        {
            CreateEnemy();
        }
    }

    // 敵を生成
    private void CreateEnemy()
    {
        enemy = Instantiate(EnemyList[Random.Range(0, EnemyList.Count)], CreatePos(), Quaternion.identity);
        enemy.GetComponent<EnemyBase>().SetManager(gameObject.GetComponent<EnemyManager>());
        enemy.GetComponent<EnemyBase>().SetPlayer(player);
        NowEnemyList.Add(enemy);
    }

    private Vector3 CreatePos()
    {
        Vector3 vPos;

        vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), 1.0f, Random.Range(-InstantiateZ, InstantiateZ));
        return vPos;
    }
}
