//======================================================================
// GuardBurst.cs
//======================================================================
// 開発履歴
//
// 2022/03/02 author：田村敏基 生成し、触れたオブジェクトを
//                             吹き飛ばすスクリプト作成                              
// 2022/03/11 author：田村敏基 処理が重くなりそうだったため、当たり判定で
//                             吹き飛ばす方法から、半径を指定する方法に変更
// 2022/03/27 author：田村敏基 敵のダメージで威力が変わるよう変動
//                             
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBurst : MonoBehaviour
{
    [SerializeField] private float UpandDown = 10.0f;

    PlayerStatus status;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 激しく吹っ飛ぶよう修正
    public void Explode(int power)
    {
        // 敵を探す
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        
        // 要素の中身分ループする
        foreach (GameObject hit in enemys)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            // 敵の物理演算をONに変更
            rb.isKinematic = false;

            if (rb != null)
            {
                // 要調整 ================================================================================================
                if (hit.GetComponent<EnemyDamageBase>()) //
                {
                    if ((this.gameObject.transform.position - hit.transform.position).magnitude <= status.BurstRadisu * 10)
                    {
                        hit.GetComponent<EnemyDamageBase>().IsDamage((status.Attack * power) / 2);
                    }

                }
                else
                {
                    if ((this.gameObject.transform.position - hit.transform.position).magnitude <= status.BurstRadisu * 10)
                    {
                        // BossBaseを書き換えてバーストでダメージを与えられるように
                        //hit.GetComponent<BossBase>().IsDamage((status.Attack * power) / 2);
                    }
                }

                // 吹き飛ばす               
                rb.AddExplosionForce(power * status.MaxBurstPower, transform.position, status.MaxBurstRadisu, UpandDown);

                //=====================================================================================================
                
                
            }
        }
    }
}
