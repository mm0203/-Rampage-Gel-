//======================================================================
// ItemImage.cs
//======================================================================
// 開発履歴
//
// 2022/04/21 author：竹尾晃史郎　作成、アイテム画像データ
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreateItemImageData")]

public class ItemImage : ScriptableObject
{
    [Header("アイテム画像")] // ゲーム中表示するアイテム画像
    public List<Sprite> ItemImageList = new List<Sprite>();
}
