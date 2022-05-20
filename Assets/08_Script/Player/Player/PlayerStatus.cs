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
    [SerializeField] private ItemManager itemManager;

    // 基本ステータス(最大)
    private int nMaxLevel = 1;
    private int nMaxHp = 100;
    private int nMaxStamina = 450;
    private int nMaxAttack = 2200;
    private int nMaxExp = 100;
    private float fMaxBurstPower = 3000;
    private float fMaxBurstRadius = 5;

    // 基本ステータス(現在)
    private int nLevel = 1;
    private int nHp = 100;
    private int nStamina = 450;
    private int nAttack = 2200;
    private float fSpeed = 100.0f;
    private int nExp = 0;
    private float fBurstStock = 5.0f;
    private float fBurstDamage = 1.0f;
    private float fBurstPower = 5;
    private float fBurstRadius = 1;

    // 追加
    private int nStaminaRecovery { get; set; } = 2;
    private int nStaminaCost { get; set; } = 1;

    // ガードペナルティ時間
    public float fGuardPenalty { get; set; } = 1.0f;

    // アイテムのステータス上昇値
    private int nUpHP = 1;
    private int nUpStamina = 10;
    private float fUpSpeed = 5.0f;
    private int nUpExp = 50;
    private int nHeal = 1;
    private int nUpAttack = 6;

    // 移動に関するデータ
    public float fBreakTime { get; set; } = 0.0f;

    // 無敵フラグ
    public bool bArmor { get; set; } = false;
    public float fArmorTime { get; set; } = 0.0f;

    // 最大ステータスを参照
    public int MaxLevel { get { return nMaxLevel; } set { nMaxLevel = value; } }
    // 最大体力増やす
    public int MaxHP { get { return nMaxHp + (itemManager.CountList[(int)ItemManager.eItem.eHp] * nUpHP); } set { nMaxHp = value; } }
    // 最大スタミナ増やす
    public int MaxStamina { get { return nMaxStamina + (itemManager.CountList[(int)ItemManager.eItem.eStamina] * nUpStamina); } set { nMaxStamina = value; } }
    public int MaxAttack { get { return nMaxAttack; } set { nMaxAttack = value; } }
    public int MaxExp { get { return nMaxExp; } set { nMaxExp = value; } }
    public float MaxBurstPower { get { return fMaxBurstPower; } set { fMaxBurstPower = value; } }
    public float MaxBurstRadisu { get { return fMaxBurstRadius; } set { fMaxBurstRadius = value; } }

    // 現在ステータスを参照
    public int Level { get { return nLevel; } set { nLevel = value; } }
    public int HP { get { return nHp; } set { nHp = value; } }
    public int Stamina { get { return nStamina; } set { nStamina = value; } }
    // 攻撃力を増やす
    public int Attack { get { return nAttack + (itemManager.CountList[(int)ItemManager.eItem.eAttack] * nUpAttack); } set { nAttack = value; } }
    // スピードを速くする
    public float Speed { get { return fSpeed + (itemManager.CountList[(int)ItemManager.eItem.eSpeed] * fUpSpeed); } set { fSpeed = value; } }
    public int Exp { get { return nExp; } set { nExp = value; } }
    public float BurstStock { get { return fBurstStock; } set { fBurstStock = value; } }
    // バーストダメージを増やす
    public float BurstDamage { get { return fBurstDamage + (itemManager.CountList[(int)ItemManager.eItem.eBurstDamage] * fBurstDamage); } set { fBurstDamage = value; } }
    public float BurstPower { get { return fBurstPower; } set { fBurstPower = value; } }
    // バースト範囲を増やす
    public float BurstRadisu { get { return fBurstRadius + (itemManager.CountList[(int)ItemManager.eItem.eBurstRange] * fBurstRadius); } set { fBurstRadius = value; } }
    public int StaminaRecovery { get { return nStaminaRecovery + (itemManager.CountList[(int)ItemManager.eItem.eStaminaHeal] * nStaminaRecovery); } }
    public int StaminaCost { get { return nStaminaCost; } }

    // レベルアップステータスを参照
    public int UpHP { get { return nUpHP + (nUpHP * itemManager.CountList[(int)ItemManager.eItem.eAutoHeal]); } set { nUpHP = value; } }
    public int UpExp { get { return nUpExp; } set { nExp = value; } }
    public int UpAttack { get { return nUpAttack; } set { nUpAttack = value; } }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            // 1以上なら持っている
            if(itemManager.CountList[(int)ItemManager.eItem.eFire] >= 1)
            {
                for(int i = 0;i <= itemManager.CountList[(int)ItemManager.eItem.eFire]; i++)
                {
                    other.gameObject.AddComponent<BurningAttribute>();                    
                }
                for(int i = 0; i <= itemManager.CountList[(int)ItemManager.eItem.eDrain]; i++)
                {
                    Debug.Log("aaa");
                    HP += 2;
                }
            }
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // 現在シーン
        //Scene scene = SceneManager.GetSceneByName("Stage1-1");
        //SceneManager.SetActiveScene(scene);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
