//======================================================================
// ObjectData.cs
//======================================================================
// 開発履歴
//
// 2022/03/28 author：松野将之 フィールドオブジェクトのデータ
// 2022/04/09 author：松野将之 オブジェクトの列挙体を定義
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreateObjectData")]

public class ObjectData : ScriptableObject
{
    [Header("Object")]

    // 生成するオブジェクトリスト
    public List<GameObject> FieldObjectList = new List<GameObject>();

    // オブジェクト生成の時間間隔
    public float GenerateTime = 5.0f;

    public enum FieldObject
    {
        eFieldObject1 = 0,
        eFieldObject2,
        eFieldObject3,
        eFieldObject4,
        eFieldObject5,
        eFieldObject6,
        eFieldObjectMax,
    }
}
