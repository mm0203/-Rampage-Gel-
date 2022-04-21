//======================================================================
// ItemManager.cs
//======================================================================
// 開発履歴
//
// 2022/03/09 author：畠山大輝 実装開始(種類,生成の仮実装)
// 2022/03/15                  アイテム追加分の実装(分岐を増やすだけ)
// 2022/04/21 author：竹尾晃史郎 nItemCount修正
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // アイテム画像データ（竹）
    public ItemImage itemImage;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < (int)eItem.eMax; i++)
        {
            CountList.Add(0);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            nStatus = Random.Range(0, (int)eItem.eMax - 1);
            nItemCount(nStatus);
        }
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
    public int nItemCount(int itemID)
    {
        CountList[itemID]++;
        Debug.Log(itemID + "番アイテム：" + CountList[itemID] + "個");

        return CountList[nStatus];
    }

    // アイテム抽選（竹尾）
    public int nItemGacha()
    {
        int nItemID = 0;
        nItemID = Random.Range(0, (int)eItem.eMax - 1);
        return nItemID;
    }

    // アイテム画像渡し（竹）
    public Sprite setItemIcon(int itemID)
    {
        Sprite image;
        image =  itemImage.ItemImageList[itemID];
        return image;
    }
}
