//======================================================================
// PlayerHP.cs
//======================================================================
// 開発履歴
//
// 2022/03/25 author：田村敏基 作成開始
// 2022/03/27 author：田村敏基 hardモードなら無効化する機能追加
// 2022/03/28 author：竹尾　応急 エフェクト発生組み込み
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    PlayerStatus status;
    PlayerState state;

    //*応急* エフェクトスクリプト
    [SerializeField] AID_PlayerEffect effect;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
        state = GetComponent<PlayerState>();
        StartCoroutine("AutoHeal");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnDamage(10);
        }
    }

    // ダメージコールバック関数
    public void OnDamage(int damage)
    {
        // 0以下なら死んでるためリターン
        if (state.IsDie) return;
        if (status.bArmor) return;
        // ハードモードなら無効
        if (state.IsHard)
        {
            //*応急*
            effect.StartEffect(6, this.gameObject, 0.5f);
            // damageをストックする
            this.GetComponent<GuardMode>().AddStockExplode(status.BurstStock);
            return;
        }

        // ダメージを与える
        status.HP -= damage;
        effect.StartEffect(8, this.gameObject, 0.5f);
        if (status.HP <= 0)
        {
            state.GotoDieState();
        }
    }

    IEnumerator AutoHeal()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            if(status.HP <= status.MaxHP)
            {
                if (state.IsDie) yield break;
                // 回復↓(回復量)
                status.HP += status.UpHP;
            }
            //　次のフレームに飛ばす
            yield return null;
        }
    }
}
