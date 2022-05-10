//======================================================================
// PlayerStatus.cs
//======================================================================
// 開発履歴
//
// 2022/03/22 author：田村敏基 制作開始 statuscomponentと変わる可能性あり
// 2022/03/28 suthor：竹尾　応急　nMaxStamina fMaxBurstPower fMaxBurstRadius nStamina fBurstPower fBurstRadius 値変更
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // 基本ステータス(最大)
    private int nMaxLevel = 1;
    private int nMaxHp = 40;
    private int nMaxStamina = 450;
    private int nMaxAttack = 22;
    private int nMaxExp = 10;
    private float fMaxBurstPower = 5;
    private float fMaxBurstRadius = 1;

    // 基本ステータス(現在)
    private int nLevel = 1;
    private int nHp = 40;
    private int nStamina = 450;
    private int nAttack = 22;
    private float fSpeed = 1.0f;
    private int nExp = 0;
    private float fBurstStock = 5.0f;
    private float fBurstDamage = 1.0f;
    private float fBurstPower = 5;
    private float fBurstRadius = 1;

    // ガードペナルティ時間
    public float fGuardPenalty { get; set; } = 1.0f;

    // レベルアップ時のステータス上昇値
    private int nUpHP = 10;
    private int nUpStamina = 10;
    private int nUpExp = 20;
    private int nUpAttack = 6;

    // 移動に関するデータ
    public float fBreakTime { get; set; } = 0.0f;

    // 無敵フラグ
    public bool bArmor { get; set; } = false;
    public float fArmorTime { get; set; } = 0.0f;

    // 最大ステータスを参照
    public int MaxLevel { get { return nMaxLevel; } set { nMaxLevel = value; } }
    public int MaxHP { get { return nMaxHp; } set { nMaxHp = value; } }
    public int MaxStamina { get { return nMaxStamina; } set { nMaxStamina = value; } }
    public int MaxAttack { get { return nMaxAttack; } set { nMaxAttack = value; } }
    public int MaxExp { get { return nMaxExp; } set { nMaxExp = value; } }
    public float MaxBurstPower { get { return fMaxBurstPower; } set { fMaxBurstPower = value; } }
    public float MaxBurstRadisu { get { return fMaxBurstRadius; } set { fMaxBurstRadius = value; } }

    // 現在ステータスを参照
    public int Level { get { return nLevel; } set { nLevel = value; } }
    public int HP { get { return nHp; } set { nHp = value; } }
    public int Stamina { get { return nStamina; } set { nStamina = value; } }
    public int Attack { get { return nAttack; } set { nAttack = value; } }
    public float Speed { get { return fSpeed; } set { fSpeed = value; } }
    public int Exp { get { return nExp; } set { nExp = value; } }
    public float BurstStock { get { return fBurstStock; } set { fBurstStock = value; } }
    public float BurstDamage { get { return fBurstDamage; } set { fBurstDamage = value; } }
    public float BurstPower { get { return fBurstPower; } set { fBurstPower = value; } }
    public float BurstRadisu { get { return fBurstRadius; } set { fBurstRadius = value; } }

    // レベルアップステータスを参照
    public int UpHP { get { return nUpHP; } set { nUpHP = value; } }
    public int UpExp { get { return nUpExp; } set { nExp = value; } }
    public int UpAttack { get { return nUpAttack; } set { nUpAttack = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
