//======================================================================
// Boss05.cs
//======================================================================
// 開発履歴
//
// 2022/05/12 author 竹尾：サイコアイ制作
//                         
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss05 : MonoBehaviour
{
    // プレイヤー
    private GameObject player;
    // ボスの基底クラス
    private EnemyBase BossBase;
    
    // エフェクト関連
    EnemyEffect enemyEffect;
    GameObject objEffect;

    int nPatarn = 2;
    float fTimer = 0;
    
    [Header("攻撃頻度")]
    [SerializeField, Range(0.0f, 20.0f)] private float fAttackTime = 10.0f;
    

    [Header("レーザー弾速度")]
    [SerializeField] float fSpeed = 5.0f;
    [SerializeField] List<GameObject> Mazzles = new List<GameObject>();
    //　発射間隔
    int nLBulletInterbal = 30; 
    // 発射数
    int nFireCount = 3;

    [Header("出現させるビットンセット")]
    [SerializeField] GameObject BittonSet;

    

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        BossBase = GetComponent<EnemyBase>();

        // エフェクト取得
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;

        nPatarn = 2;
    }



    void Update()
    {
        fTimer += Time.deltaTime;

        if(fTimer > fAttackTime)
        {
            switch(nPatarn)
            {
                case 1:
                    DisarrayedBullet();
                    break;

                case 2:
                    SetBitton();
                    break;
            }

            fTimer = 0;
        }
        
    }



    // ビーム乱弾 ************************************************
    // コルーチン管理
    void DisarrayedBullet()
    {
        StartCoroutine(BeamBullet());
        nPatarn = 2;
    }

    IEnumerator BeamBullet()
    {
        for(int l = 0; l < nFireCount; l++)
        {
            for (int i = 0; i < Mazzles.Count; i++)
            {
                // 弾を生成
                GameObject Sphere;
                Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                // 弾のサイズ、座標、角度設定
                Sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                Sphere.transform.rotation = Mazzles[i].transform.rotation;
                Sphere.transform.position = new Vector3(Mazzles[i].transform.position.x + Mazzles[i].transform.forward.x, Mazzles[i].transform.position.y, Mazzles[i].transform.position.z + Mazzles[i].transform.forward.z);
                // 弾にコンポーネント追加
                Sphere.AddComponent<LazerBullet>();

                // 弾のコンポーネントに情報をセット
                LazerBullet bullet = Sphere.GetComponent<LazerBullet>();
                bullet.Speed = fSpeed;
                bullet.SetPlayer(BossBase.GetComponent<EnemyBase>().player);
                bullet.SetEnemy(gameObject);

                // エフェクト生成
                objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFireBall, gameObject);
                bullet.SetEffect(objEffect);

                // すり抜けるように
                Sphere.GetComponent<SphereCollider>().isTrigger = true;
            }

            for (int n = 0; n < nLBulletInterbal; n++)
            {
                yield return null;
            }
        }        
    }
    //**********************************************************



    // サイドレーザービーム ************************************
    void SetBitton()
    {
        GameObject obj;
        obj = Instantiate(BittonSet, player.transform.position, Quaternion.identity);
        obj.GetComponent<PincerLazer>().SetEnemy(this.gameObject);
        nPatarn = 1;
    }
    //**********************************************************

    
}
