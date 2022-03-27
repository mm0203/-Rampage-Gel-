//======================================================================
// PlayerStatus.cs
//======================================================================
// 開発履歴
//
// 2022/03/22 author：田村敏基 制作開始 statuscomponentと変わる可能性あり
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // 基本ステータス(最大)
    private int nMaxLevel = 1;
    private int nMaxHp = 20;
    private int nMaxStamina = 300;
    private int nMaxAttack = 10;
    private int nMaxExp = 10;
    private float fMaxBurstPower = 100;
    private float fMaxBurstRadius = 100;

    // 基本ステータス(現在)
    private int nLevel = 1;
    private int nHp = 20;
    private int nStamina = 300;
    private int nAttack = 10;
    private float fSpeed = 1.0f;
    private int nExp = 0;
    private float fBurstPower = 100;
    private float fBurstRadius = 100;

    // レベルアップ時のステータス上昇値
    private int nUpHP = 1;
    private int nUpStamina = 10;
    private int nUpExp = 20;
    private int nUpAttack = 1;

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
