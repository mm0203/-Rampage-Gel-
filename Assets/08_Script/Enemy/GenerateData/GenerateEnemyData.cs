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
        [SerializeField] int MaxEnemy = 10;

        [Header("-敵種類-")]
        [SerializeField] List<GameObject> EnemyList = new List<GameObject>();

        [Header("-ボス-")]
        [SerializeField] GameObject BossEnemy = new GameObject();
    }

    [Header("<各ステージ敵生成リスト>")]
    [Header("※各リスト[5]固定")]
    [SerializeField] StageData[] Planet1 = new StageData[5];

    [SerializeField] StageData[] Planet2 = new StageData[5];

    [SerializeField] StageData[] Planet3 = new StageData[5];

    [SerializeField] StageData[] Planet4 = new StageData[5];

    [SerializeField] StageData[] Planet5 = new StageData[5];

    [SerializeField] StageData[] Planet6 = new StageData[5];

    [SerializeField] StageData[] Planet7 = new StageData[5];

    
}


