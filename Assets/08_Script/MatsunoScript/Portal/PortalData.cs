//======================================================================
// SceneObject.cs
//======================================================================
// 開発履歴
//
// 2022/03/28 author：松野将之 ポータルのデータ
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MatsunoScript/MyScriptable/Create PortaData")]

public class PortalData : ScriptableObject
{
    [Header("Portal")]

    // ポータルが割れるまでの回数
    [System.NonSerialized]
    public int Hp = 2;
}
