//======================================================================
// EnemyData.cs
//======================================================================
// 開発履歴
//
// 2022/04/08 author：松野将之 敵のデータ
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreateEnemyData")]

public class EnemyData : ScriptableObject
{

    // public List<EnemyStatus> EnemyStatusList = new List<EnemyStatus>();

    // 基本ステータス
    public int nLevel { get; set; } = 1;
    public int nHp { get; set; } = 20;
    public int nAttack { get; set; } = 10;
    public float fSpeed { get; set; } = 1.0f;

    // レベルアップ時のステータス上昇値
    public int nUpHP { get; set; } = 1;
    public int nUpAttack { get; set; } = 1;


}
