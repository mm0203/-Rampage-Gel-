//======================================================================
// EnemyEffect.cs
//======================================================================
// 開発履歴
//
// 2022/03/30 author：小椋　エフェクト生成処理追加
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    [Header("エフェクトデータ")]
    [SerializeField] EnemyEffectData EffectData;

    // エフェクトリスト
    List<GameObject> EffectList;

    public enum eEffect
    {
        eFireBall = 0,
        eFlame,
        eScratch,
        eFirePiller,
        eIcePiller,

        eMax_Effect
    }

    //--------------------------------------------------
    // エフェクト生成
    // 引数：エフェクト番号, 敵オブジェクト, 消滅時間
    //--------------------------------------------------
    public GameObject CreateEffect(eEffect num, GameObject obj, float time = 5.0f)
    {
        GameObject Effect = Instantiate(EffectData.GetEffectList[(int)num], obj.transform.position, obj.transform.rotation);
        Destroy(Effect, time);

        return Effect;
    }

    //--------------------------------
    // エフェクト生成（Pos変更Ver）
    //--------------------------------
    public GameObject CreateEffect(eEffect num, Vector3 pos ,GameObject obj, float time = 5.0f)
    {
        GameObject Effect = Instantiate(EffectData.GetEffectList[(int)num], pos, obj.transform.rotation);
        Destroy(Effect, time);

        return Effect;
    }
}
