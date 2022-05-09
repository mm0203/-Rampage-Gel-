//======================================================================
// GenerateEnemyData.cs
//======================================================================
// 開発履歴
//
// 2022/04/23 author:竹尾晃史郎　レベルデザインが面倒なので制作
//                   　　　　　　Element0とか変えたい
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(menuName = "Scriptable/CreateGenerateEnemyData")]

public class GenerateEnemyData : ScriptableObject
{
    [Serializable] public class StageData 
    {
        [Header("-最大敵数-")]
        [SerializeField] public int MaxEnemy = 10;

        [Header("-敵種類-")]
        [SerializeField] public List<GameObject> EnemyList = new List<GameObject>();

        [Header("-ボス-")]
        [SerializeField] public GameObject BossEnemy;

        
    }

    private void Awake()
    {
        
    }

    [Header("<各ステージ敵生成リスト>")]
    [Header("※各リスト[5]固定")]
    [SerializeField] public StageData[] Planet1 = new StageData[5];

    [SerializeField] public StageData[] Planet2 = new StageData[5];

    [SerializeField] public StageData[] Planet3 = new StageData[5];

    [SerializeField] public StageData[] Planet4 = new StageData[5];

    [SerializeField] public StageData[] Planet5 = new StageData[5];

    [SerializeField] public StageData[] Planet6 = new StageData[5];

    [SerializeField] public StageData[] Planet7 = new StageData[5];



    // 呼び出し方
    // generateEnemy.Planet1[1].MaxEnemy; <1-1> の　最大敵数取得
    // generateEnemy.Planet3[5].EnemyList; <3-5> の　出現敵種類取得
}


