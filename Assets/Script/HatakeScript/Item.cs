//======================================================================
// Item.cs
//======================================================================
// 開発履歴
//
// 2022/03/09 author：畠山大輝 実装開始(種類,生成の仮実装)
// 2022/03/15                  アイテム追加分の実装(分岐を増やすだけ)
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // アイテムの種類
    enum eItem
    {
        eHp = 0,
        eStamina,
        eAttack,
        eSpeed,
        eTelescopic,
        eBurst,
        eGuard,
        eHighBound,
        eFire,
        eWater,
        eThunder,
        eDivision,
        eAutoHeal,
        eStaminaHeal,
        eCombo,
        eDrain,
        eReborn,
        eWall,
        eAura,
        eSonicBoom,

        eMax
    }

    [Header("アイテムの出現座標範囲")] [SerializeField, Range(1.0f, 100.0f)] float InstantiateX = 5.0f;
    [SerializeField, Range(1.0f, 100.0f)] float InstantiateZ = 5.0f;

    public List<GameObject> ItemObjectList;
    public List<int> CountList;
    public int nStatus;

    // Start is called before the first frame update
    void Start()
    {
        // 仮
        nStatus = Random.Range(0, (int)eItem.eMax - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            switch(nStatus)
            {
                // TODO:プレイヤーのステータス上昇値、確定したら
                case (int)eItem.eHp:
                    Debug.Log("HPアイテム取得");
                    break;

                case (int)eItem.eStamina:
                    Debug.Log("スタミナアイテム取得");
                    break;

                case (int)eItem.eAttack:
                    Debug.Log("攻撃アイテム取得");
                    break;

                case (int)eItem.eSpeed:
                    Debug.Log("スピードアイテム取得");
                    break;

                case (int)eItem.eTelescopic:
                    Debug.Log("伸縮アイテム取得");
                    break;

                case (int)eItem.eBurst:
                    Debug.Log("バーストアイテム取得");
                    break;

                case (int)eItem.eGuard:
                    Debug.Log("防御アイテム取得");
                    break;

                case (int)eItem.eHighBound:
                    Debug.Log("ハイバウンドアイテム取得");
                    break;

                case (int)eItem.eFire:
                    Debug.Log("火アイテム取得");
                    break;

                case (int)eItem.eWater:
                    Debug.Log("水アイテム取得");
                    break;

                case (int)eItem.eThunder:
                    Debug.Log("雷アイテム取得");
                    break;

                case (int)eItem.eDivision:
                    Debug.Log("分裂アイテム取得");
                    break;

                case (int)eItem.eAutoHeal:
                    Debug.Log("オート回復アイテム取得");
                    break;

                case (int)eItem.eStaminaHeal:
                    Debug.Log("スタミナ回復アイテム取得");
                    break;

                case (int)eItem.eCombo:
                    Debug.Log("コンボアイテム取得");
                    break;

                case (int)eItem.eDrain:
                    Debug.Log("ドレインアイテム取得");
                    break;

                case (int)eItem.eReborn:
                    Debug.Log("リボーンアイテム取得");
                    break;

                case (int)eItem.eWall:
                    Debug.Log("壁アイテム取得");
                    break;

                case (int)eItem.eAura:
                    Debug.Log("バーストオーラアイテム取得");
                    break;

                case (int)eItem.eSonicBoom:
                    Debug.Log("ソニックブームアイテム取得");
                    break;

                default:
                    Debug.Log("不明アイテム");
                    break;
            }
            CountList[nStatus]++;
            Debug.Log(nStatus + "番目のオブジェクトは" + CountList[nStatus] + "個取得しています");

            Destroy(this.gameObject);
            CreateItem();
        }
    }

    // 新規オブジェクトの生成関数
    public void CreateItem()
    {
        Vector3 vPos;

        vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), transform.position.y, Random.Range(-InstantiateZ, InstantiateZ));
        nStatus = Random.Range(0, (int)eItem.eMax - 1);
        Instantiate(ItemObjectList[nStatus], vPos, Quaternion.identity);
        Debug.Log(nStatus);
    }
}
