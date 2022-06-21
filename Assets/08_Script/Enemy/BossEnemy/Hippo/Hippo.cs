//======================================================================
// Hippo.cs
//======================================================================
// 開発履歴
//
// 2022/06/14 author：小椋駿 製作開始　
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hippo : MonoBehaviour
{
    // 攻撃の種類
    enum eHippoAttack
    {
        eFireWave = 0,
        eWaterWave,
        eWay,

        eMax
    }

    // 攻撃関数リスト
    private List<Action> AttackList;

    // エフェクト関連
    EnemyEffect enemyEffect;

    GameObject Cube;
    GameObject[] Way;
    int WayNum = 4;
    EnemyBase enemyBase;

    float attackTime = 6.0f;

    // 攻撃種類数
    int AttackNum;

    int HP;
    int NowHP;
    bool AddFlag;

    void Start()
    {
        // エフェクト取得
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;

        // エネミーベース情報取得
        enemyBase = this.GetComponent<EnemyBase>();

        // 攻撃の種類をリストに追加
        AttackList = new List<Action>();
        AttackList.Add(FireWaveAttack);
        AttackList.Add(WaterWaveAttack);

        // HP最大値保存
        HP = gameObject.GetComponent<EnemyBase>().GetEnemyData.nHp;
        AddFlag = false;

        Way = new GameObject[WayNum];
        AttackNum = 2;
    }

    private void Update()
    {
        // 攻撃終了
        if(enemyBase.bAttack)
        {
            attackTime -= Time.deltaTime;
            if(attackTime < 0)
            {
                enemyBase.bAttack = false;
                attackTime = 6.0f;
            }
        }

        NowHP = gameObject.GetComponent<EnemyBase>().GetEnemyData.nHp;
        // HPが半分以下になった時、攻撃種類追加
        if (NowHP <= HP / 2 && !AddFlag)
        {
            AttackList.Add(FourWaveAttack);
            AddFlag = true;
            AttackNum = 3;
            Debug.Log("Y");
        }
        
    }

    // 攻撃の関数をランダムに呼ぶ
    void HippoAttack()
    {
        int range = UnityEngine.Random.Range(0, AttackNum);
        AttackList[range]();

    }


    // ファイヤーウェーブ
    private void FireWaveAttack()
    {
        // 当たり判定用キューブ生成
        Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // サイズ、座標、角度設定
        Cube.transform.localScale = new Vector3(8.0f, 1.0f, 5.0f);
        Cube.transform.rotation = this.transform.rotation;
        Cube.transform.position = new Vector3(transform.position.x + transform.forward.x * Cube.transform.localScale.x,
                                        transform.position.y,
                                        transform.position.z + transform.forward.z * Cube.transform.localScale.z);    // ボス前方へ生成

        // コンポーネント調整
        Cube.AddComponent<FireWave>();
        Cube.GetComponent<FireWave>().SetPlayer(enemyBase.GetComponent<EnemyBase>().player);
        Cube.GetComponent<FireWave>().SetEnemy(this.gameObject);
        Cube.GetComponent<BoxCollider>().isTrigger = true;
        enemyBase.bAttack = true;

        Cube.AddComponent<Rigidbody>();
        Cube.GetComponent<Rigidbody>().useGravity = false;
        Cube.GetComponent<Rigidbody>().isKinematic = true;

        Cube.GetComponent<MeshRenderer>().enabled = false;

        // エフェクト生成
        Vector3 pos = new Vector3(gameObject.transform.position.x + gameObject.transform.forward.x * 2.0f, 
                                  gameObject.transform.position.y, 
                                  gameObject.transform.position.z + gameObject.transform.forward.z * 2.0f);
        enemyEffect.CreateEffect(EnemyEffect.eEffect.eWave02, pos, gameObject, 5.0f);
    }

    // ウォータウェーブ
    private void WaterWaveAttack()
    {
        // 当たり判定用キューブ生成
        Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // サイズ、座標、角度設定
        Cube.transform.localScale = new Vector3(8.0f, 1.0f, 5.0f);
        Cube.transform.rotation = this.transform.rotation;
        Cube.transform.position = new Vector3(transform.position.x + transform.forward.x * Cube.transform.localScale.x,
                                        transform.position.y,
                                        transform.position.z + transform.forward.z * Cube.transform.localScale.z);    // ボス前方へ生成

        // コンポーネント調整
        Cube.AddComponent<WaterWave>();
        Cube.GetComponent<WaterWave>().SetPlayer(enemyBase.GetComponent<EnemyBase>().player);
        Cube.GetComponent<WaterWave>().SetEnemy(this.gameObject);
        Cube.GetComponent<BoxCollider>().isTrigger = true;
        enemyBase.bAttack = true;

        Cube.AddComponent<Rigidbody>();
        Cube.GetComponent<Rigidbody>().useGravity = false;
        Cube.GetComponent<Rigidbody>().isKinematic = true;

        Cube.GetComponent<MeshRenderer>().enabled = false;

        // エフェクト生成
        Vector3 pos = new Vector3(gameObject.transform.position.x + gameObject.transform.forward.x * 3.0f,
                                  gameObject.transform.position.y,
                                  gameObject.transform.position.z + gameObject.transform.forward.z * 3.0f);
        enemyEffect.CreateEffect(EnemyEffect.eEffect.eWave01, pos, gameObject, 5.0f);
    }


    private void FourWaveAttack()
    {
        for(int i = 0; i < WayNum; i++)
        {
            // 当たり判定用キューブ生成
            Way[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);

            // サイズ、座標、角度設定
            Way[i].transform.localScale = new Vector3(2.0f, 1.0f, 1.0f);
            Way[i].transform.Rotate(0, transform.rotation.y + (90.0f * (i + 1)),0);
            Way[i].transform.position = transform.position;

            // コンポーネント調整
            Way[i].AddComponent<FourWayWave>();
            Way[i].GetComponent<FourWayWave>().SetPlayer(enemyBase.GetComponent<EnemyBase>().player);
            Way[i].GetComponent<FourWayWave>().SetEnemy(this.gameObject);
            Way[i].GetComponent<BoxCollider>().isTrigger = true;
            enemyBase.bAttack = true;
        }
    }
}
