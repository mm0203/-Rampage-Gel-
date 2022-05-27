//======================================================================
// Boss04.cs
//======================================================================
// 開発履歴
//
// 2022/05/11 author 竹尾：スカルドラゴン制作
//                         アニメーションイベントに起因して起こす
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss04 : MonoBehaviour
{
    // プレイヤー
    private GameObject player;
    // ボスの基底クラス
    private EnemyBase BossBase;
    // 射出口
    [SerializeField] GameObject Mazzle;
    // FireWallの範囲オブジェクト
    public GameObject WallCircle;
    // エフェクト関連
    EnemyEffect enemyEffect;
    GameObject objEffect;

    [Header("攻撃頻度")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 10.0f;

    [Header("火球速度")]
    [SerializeField] float fSpeed = 5.0f;

    [Header("火炎放射の距離")]
    [SerializeField] float fDistance = 3.0f;

    [Header("火炎放射の時間")] //消えるかも
    [SerializeField] float fFlameTime = 1.0f;

    private float fAttackCount;
    private int nSetAttack;
    private int nFireDamageCol = 10;
    private int nWallTime = 180;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        BossBase = GetComponent<EnemyBase>();

        // エフェクト取得
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;
        WallCircle.SetActive(false);
    }



    void Update()
    {
        FireWallDamage();

        if(Input.GetKeyDown(KeyCode.N))
        {
            StartFireWall();
        }
    }

    // 火球連弾 ************************************************
    // アニメーションイベントをつけまくる
    void FireBullet()
    {
        // 弾を生成
        GameObject Sphere;
        Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // 弾のサイズ、座標、角度設定
        Sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        Sphere.transform.rotation = Mazzle.transform.rotation;
        Sphere.transform.position = new Vector3(Mazzle.transform.position.x + Mazzle.transform.forward.x, Mazzle.transform.position.y, Mazzle.transform.position.z + Mazzle.transform.forward.z);

        // 弾にコンポーネント追加
        Sphere.AddComponent<Bullet>();

        // 弾のコンポーネントに情報をセット
        Bullet bullet = Sphere.GetComponent<Bullet>();
        bullet.Speed = fSpeed;
        bullet.SetPlayer(BossBase.GetComponent<EnemyBase>().player);
        bullet.SetEnemy(gameObject);

        // エフェクト生成
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFireBall, gameObject);
        bullet.SetEffect(objEffect);

        // すり抜けるように
        Sphere.GetComponent<SphereCollider>().isTrigger = true;
    }
    //**********************************************************

    // 薙ぎ払いブレス *****************************************
    // タイミングよくアニメーションイベントをつける
    void FireBreth()
    {
        // 当たり判定用のキューブ生成
        GameObject cube;
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // サイズ、座標、角度設定
        cube.transform.localScale = new Vector3(1.0f, 1.0f, 5.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * fDistance, transform.position.y, transform.position.z + transform.forward.z * fDistance);

        // 火炎放射のコンポーネントを追加
        cube.AddComponent<Flamethrower>();

        // 敵セット
        cube.GetComponent<Flamethrower>().enemy = gameObject;

        // プレイヤーセット
        cube.GetComponent<Flamethrower>().player = BossBase.player;

        // 火炎放射距離セット
        cube.GetComponent<Flamethrower>().fDis = fDistance;

        // エフェクト生成
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFlame, gameObject);

        // エフェクトセット
        cube.GetComponent<Flamethrower>().effect = objEffect;

        // すり抜ける判定に
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // 攻撃フラグをON（敵が動かなくなる）
        BossBase.bAttack = true;

        // 当たり判定キューブを非表示
        cube.GetComponent<MeshRenderer>().enabled = false;

        // 火炎放射時間設定
        cube.GetComponent<Flamethrower>().fLifeTime = fFlameTime;
    }
    //**********************************************************

    // 炎の壁 *************************************************
    // タイミングよくアニメーションつけて、しばらく残す
    void StartFireWall()
    {
        Debug.Log("a");
        StartCoroutine(FireWall());
    }

    void FireWallDamage()
    {
        

        if(WallCircle.GetComponent<FireWall>().bInArea == true)
        {
            nSetAttack = BossBase.GetComponent<EnemyBase>().GetEnemyData.nAttack;
            player.GetComponent<PlayerHP>().OnDamage(nSetAttack / nFireDamageCol);
        }
    }

    private IEnumerator FireWall()
    {
        for(int i = 0; i <= nWallTime; i++)
        {
            yield return null;
            WallCircle.SetActive(true);
        }
        WallCircle.GetComponent<FireWall>().bInArea = false;
        WallCircle.SetActive(false);
    }

    //**********************************************************
}
