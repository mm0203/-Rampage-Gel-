//======================================================================
// EnemyData.cs
//======================================================================
// 開発履歴
//
// 2022/04/08 author：松野将之 雑魚敵のステータス
// 2022/04/17 author：松野将之 ボスのステータス追加
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

    // ボス
    public int nBossLevel { get; set; } = 1;
    public int nBossHp { get; set; } = 200;
    public int nBossAttack { get; set; } = 10;
    public float fBossSpeed { get; set; } = 4.0f;

    // レベルアップ時のステータス上昇値
    public int nUpHP { get; set; } = 1;
    public int nUpAttack { get; set; } = 1;


}
