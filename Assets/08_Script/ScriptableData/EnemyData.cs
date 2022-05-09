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
    [Tooltip("ボスのレベル")]  public int BossLevel = 1;
    [Tooltip("ボスのHP")]      public int BossHp = 200;
    [Tooltip("ボスの攻撃力")]  public int BossAttack = 10;
    [Tooltip("ボスの速度")]    public float BossSpeed = 4.0f;

    // レベルアップ時のステータス上昇値
    public int nUpHP { get; set; } = 1;
    public int nUpAttack { get; set; } = 1;


}
