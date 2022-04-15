//======================================================================
// GenerateObject.cs
//======================================================================
// 開発履歴
//
// 2022/03/21 author：松野将之 指定範囲内でオブジェクト自動生成
// 2022/04/08 author：松野将之 上下左右4箇所で適用(要リファクタリング)
// 2022/04/09 author：松野将之 生成するオブジェクトをランダムに
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    // オブジェクトデータ
    public ObjectData ObjectData;
    // 生成する範囲A
    [SerializeField]
    private Transform rangeA;
    // 生成する範囲B
    [SerializeField]
    private Transform rangeB;
    // 経過時間
    private float time;

    void Update()
    {
        // 前フレームからの時間を加算
        time = time + Time.deltaTime;

        // 秒数でランダムに生成されるようにする
        if (time > ObjectData.GenerateTime)
        {
            // rangeAとrangeBのx座標の範囲内でランダムな数値を作成
            float x = Random.Range(rangeA.position.x, rangeB.position.x);
            // rangeAとrangeBのy座標の範囲内でランダムな数値を作成
            float y = 0.0f;
            // rangeAとrangeBのz座標の範囲内でランダムな数値を作成
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            // 生成するオブジェクトをランダムで決定
            int OvjectNo = Random.Range((int)ObjectData.FieldObject.eFieldObject1, (int)ObjectData.FieldObject.eFieldObjectMax);

            // GameObjectを上記で決まったランダムな場所に生成
            Instantiate(ObjectData.FieldObjectList[OvjectNo], new Vector3(x, y, z), this.transform.rotation);

            // 経過時間リセット
            time = 0f;
        }
    }
}
