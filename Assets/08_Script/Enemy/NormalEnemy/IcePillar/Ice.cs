//======================================================================
// Ice.cs
//======================================================================
// 開発履歴
//
// 2022/04/21 author：小椋駿 製作開始　氷柱処理
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    // 氷柱の時間(IcePillar.csで設定)
    public float fLifeTime { get; set; }

    // 攻撃サークルが出てから氷柱が出る時間
    float fAttackStart = 1.0f;

    // 氷柱が出てきているか
    bool bAttackStart = false;

    // エフェクト関連
    EnemyEffect enemyEffect;
    GameObject ObjEffect;

    public GameObject player { get; set; }
    public GameObject enemy { get; set; }
    GameObject AttackCircle, TimeCircle;

    // サークル拡大量
    float fScale;

    public void SetCircle(GameObject obj) { AttackCircle = obj; }


    //----------------------------------
    // 初期化
    //----------------------------------
    private void Start()
    {
        // 広がるサークル生成
        TimeCircle = Instantiate(AttackCircle, new Vector3(player.transform.position.x, 0.1f, player.transform.position.z), AttackCircle.transform.rotation);

        // 大きさはゼロ
        TimeCircle.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        // サークルの大きさ拡大
        fScale = AttackCircle.transform.localScale.x / (fAttackStart * 50.0f);

        // 攻撃サークル生成
        AttackCircle = Instantiate(AttackCircle, new Vector3(player.transform.position.x, 0.1f, player.transform.position.z), AttackCircle.transform.rotation);

        // サークルの透明度を下げる
        AttackCircle.GetComponent<SpriteRenderer>().color -= new Color32(0, 0, 0, 125);

        // ザコ用
        enemyEffect = enemy.GetComponent<EnemyBase>().GetEffect;
    }

    //----------------------------------
    // 更新
    //----------------------------------
    void Update()
    {
        // サークルが消えた & まだ氷柱が出ていない
        if (AttackCircle == null && !bAttackStart)
        {
            bAttackStart = true;

            gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
            gameObject.GetComponent<MeshRenderer>().enabled = true;

            // エフェクト生成
            //ObjEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFirePiller, gameObject, fLifeTime - fAttackStart);
        }

        Destroy(gameObject, fLifeTime);

        // 一定時間後、予測サークル消滅
        Destroy(AttackCircle, fAttackStart);
        Destroy(TimeCircle, fAttackStart);
    }

    private void FixedUpdate()
    {
        // サークルサイズ拡大
        if (TimeCircle != null)
            TimeCircle.transform.localScale = new Vector3(TimeCircle.transform.localScale.x + fScale, TimeCircle.transform.localScale.y + fScale, 1.0f);
    }




    //----------------------------------
    // 当たり判定
    //----------------------------------
    // 氷柱に入った瞬間ダメージ
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && bAttackStart)
        {
            // ダメージ処理
            //player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack);
        }
    }
}
