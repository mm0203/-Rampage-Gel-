//======================================================================
// SceneData.cs
//======================================================================
// 開発履歴
//
// 2022/04/23 author:竹尾晃史郎　制作（シーン管理が面倒なので）
//                               SceneObject.csを消すと成立しないため注意
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreateSceneData")]

public class SceneData : ScriptableObject
{
    [Header("Title")]
    [Header("<SceneList>")]
    [Header("※追加できないときはBuildSeetingにシーンが追加されてるか確認")]
    public SceneObject TitleScene;

    [Header("GameScene")]
    public List<SceneObject> GameScene = new List<SceneObject>();

    [Header("MovieScene")] // ※ 各Planet間のシーン遷移で挟むステージ紹介
    public List<SceneObject> MovieScene = new List<SceneObject>();
}
