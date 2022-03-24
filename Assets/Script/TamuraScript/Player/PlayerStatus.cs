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
    [SerializeField] private int nMaxLevel = 1;
    [SerializeField] private int nMaxHp = 20;
    [SerializeField] private int nMaxGuard = 2;
    [SerializeField] private int nMaxAttack = 10;
    [SerializeField] private int nMaxExp = 10;

    // 基本ステータス(現在)
    [SerializeField] private int nLevel = 1;
    [SerializeField] private int nHp = 20;
    [SerializeField] private int nGuard = 2;
    [SerializeField] private int nAttack = 10;
    [SerializeField] private float fSpeed = 1.0f;
    [SerializeField] private int nExp = 10;

    // レベルアップ時のステータス上昇値
    [SerializeField] private int nUpHP = 1;
    [SerializeField] private int nUpAttack = 1;

    // 最大ステータスを参照
    public int MaxLevel { get { return nMaxLevel; } set { nMaxLevel = value; } }
    public int MaxHP { get { return nMaxHp; } set { nMaxHp = value; } }
    public int MaxGuard { get { return nMaxGuard; } set { nMaxGuard = value; } }
    public int MaxAttack { get { return nMaxAttack; } set { nMaxAttack = value; } }
    public int MaxExp { get { return nMaxExp; } set { nMaxExp = value; } }

    // 現在ステータスを参照
    public int Level { get { return nLevel; } set { nLevel = value; } }
    public int HP { get { return nHp; } set { nHp = value; } }
    public int Guard { get { return nGuard; } set { nGuard = value; } }
    public int Attack { get { return nAttack; } set { nAttack = value; } }
    public float Speed { get { return fSpeed; } set { fSpeed = value; } }
    public int Exp { get { return nExp; } set { nExp = value; } }
    public int UpHP { get { return nUpHP; } set { nUpHP = value; } }
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
