
//======================================================================
// boss01_body.cs
//======================================================================
// 開発履歴
//
// 2022/04/06 author：小椋駿 製作開始　ボスの体処理
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01_body : MonoBehaviour
{
    // モデル同士の距離
    float fDistance = 1.2f;

    // 追従モデルの回転速度
    float fRotSpeed = 3.0f;

    // 自身の前にあるオブジェクト（体なら頭、尾なら体）
    GameObject FrontObject;

    // ボスの頭情報
    GameObject HeadObject;

    public void SetBossFront(GameObject obj) { FrontObject = obj; }
    public void SetBossHead(GameObject obj) { HeadObject = obj; }



    void Update()
    {
        DeathHead();

        // 体の座標を調整
        gameObject.transform.position = new Vector3(FrontObject.transform.position.x - FrontObject.transform.forward.x * fDistance, 
                                                    FrontObject.transform.position.y,
                                                    FrontObject.transform.position.z - FrontObject.transform.forward.z * fDistance);


        // 順にゆっくり回転するように
        Quaternion rot = Quaternion.LookRotation(FrontObject.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, fRotSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーとの衝突時ダメージ
        if (other.CompareTag("Player"))
        {
            // ダメージ処理
            HeadObject.GetComponent<EnemyDamageBase>().TailDamage();
        }
    }

    // 死亡判定
    private void DeathHead()
    {
        if (HeadObject.GetComponent<EnemyBase>().nHp <= 0)
            Destroy(this.gameObject);
    }
}
