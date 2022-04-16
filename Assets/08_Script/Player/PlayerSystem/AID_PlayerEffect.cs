//======================================================================
// AID_PlayerEffect.cs
//======================================================================
// 開発履歴
//
// 2022/03/28 author：竹尾　応急　プレイヤーのエフェクト発生
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AID_PlayerEffect : MonoBehaviour
{
    [Header("発生エフェクト")]
    [SerializeField] List<GameObject> EffectList;

    private void Update()
    {
        // テスト
        if(Input.GetKeyDown(KeyCode.E))
        {
            StartEffect(0, this.gameObject, 1.0f);
        }
    }

    public void StartEffect(int listnum, GameObject player, float time)
    {
        GameObject Effect = null;
        Effect = Instantiate(EffectList[listnum], player.transform.position, player.transform.rotation);
        Destroy(Effect, time);
    }
}
