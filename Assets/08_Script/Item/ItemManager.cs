//======================================================================
// ItemManager.cs
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

public class ItemManager : MonoBehaviour
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
        for (int i = 0; i < (int)eItem.eMax; i++)
        {
            CountList.Add(0);
        }

        // 仮
        nStatus = Random.Range(0, (int)eItem.eMax - 1);
        CreateItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 新規オブジェクトの生成関数
    public void CreateItem()
    {
        Vector3 vPos;

        vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), transform.position.y, Random.Range(-InstantiateZ, InstantiateZ));
        nStatus = Random.Range(0, (int)eItem.eMax - 1);
        Instantiate(ItemObjectList[nStatus], vPos, Quaternion.identity);
    }

    // オブジェクトの取得個数をカウント
    public int nItemCount()
    {
        CountList[nStatus]++;
        Debug.Log(nStatus + "番アイテム：" + CountList[nStatus] + "個");

        return CountList[nStatus];
    }
}
