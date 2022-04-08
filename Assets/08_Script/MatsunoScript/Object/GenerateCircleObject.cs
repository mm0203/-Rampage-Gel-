//======================================================================
// GenerateCircleObject.cs
//======================================================================
// 開発履歴
//
// 2022/04/08 author：松野将之 イベントオブジェクト(円状にオブジェクト配置)の実装
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCircleObject : MonoBehaviour
{
    // 生成するオブジェクト
    public GameObject CircleObject;

    // 生成開始の場所
    public GameObject CenterObject;

    // 生成するオブジェクトの数
    public int ObjecCount = 40;

    // 半径
    public float distance = 5.0f;

<<<<<<< HEAD
=======
    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            GenerateCircle(CircleObject,   // 生成するオブジェクト
                           ObjecCount,     // 生成する数
                           CenterObject,   // 中心のオブジェクト
                           distance,       // 各オブジェクトの距離
                           true            // 中央に向けるかどうか
                           );                                  
        }
    }

>>>>>>> matsuno
    public void GenerateCircle(GameObject prefab, int count, GameObject center, float distance, bool isLookAtCenter)
    {
        for (int i = 0; i < count; i++)
        {
            var position = center.transform.position + (Quaternion.Euler(0f, 360f / count * i, 0f) * center.transform.forward * distance);
            var obj = Instantiate(prefab, position, Quaternion.identity);

            // 中央に向けるかどうか
            if (isLookAtCenter)
            {
                obj.transform.LookAt(center.transform);
            }
        }
    }

    // プレイヤーと接触でシーン遷移
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            GenerateCircle(CircleObject,   // 生成するオブジェクト
                            ObjecCount,     // 生成する数
                            CenterObject,   // 中心のオブジェクト
                            distance,       // 各オブジェクトの距離
                            true            // 中央に向けるかどうか
                            );
        }
    }
}
