//======================================================================
// ItemManager.cs
//======================================================================
// 開発履歴
//
// 2022/03/09 author：畠山大輝 実装開始(種類,生成の仮実装)
// 2022/03/15                  アイテム追加分の実装(分岐を増やすだけ)
// 2022/04/21 author：竹尾晃史郎 nItemCount修正、他β用に調整
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
        eHp = 0,     // nHp
        eStamina,    // nStamina
        eAttack,     // nAttack
        eSpeed,      // fSpeed
        eBurstRange, // バースト範囲
        eBurstDamage,// バーストダメージ
        //eGuard,      // ※未使用
        //eHighBound,  // ※未使用
        eFire,       // 火属性
        eWater,      // 水属性
        eThunder,    // 雷属性
        eDivision,   // 分裂
        eAutoHeal,   // オート回復
        eStaminaHeal,// スタミナ回復速度
        //eCombo,      // ※未使用
        eDrain,      // HP吸収
        eReborn,     // 残機
        //eWall,       // ※未使用
        //eAura,       // ※未使用
        eSonicBoom,  // 無敵時間

        eMax
    }

    [Header("アイテムの出現座標範囲")] [SerializeField, Range(1.0f, 100.0f)] float InstantiateX = 5.0f;
    [SerializeField, Range(1.0f, 100.0f)] float InstantiateZ = 5.0f;

    public List<GameObject> ItemObjectList;
    public List<int> CountList;
    public List<int> DrawItemList;
    public int nStatus;

    // アイテム画像データ（竹）
    public ItemImage itemImage;
    public GameObject imageObject;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < (int)eItem.eMax + 1; i++)
        {
            CountList.Add(0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
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
        //Debug.Log(itemID + "番アイテム：" + CountList[itemID] + "個");

        if (CountList[itemID] == 1)
        {
            DrawItemList.Add(itemID);
            imageObject.GetComponent<Image>().sprite = itemImage.ItemImageList[itemID];
            GetComponent<ItemUI>().CreateItemUI(DrawItemList.Count, imageObject);
        }
        //Debug.Log(CountList[itemID]);
        return CountList[itemID];
    }

    /* 以下、レベルアップ処理用関数 */

    // オブジェクトのアイテム番号渡し
    public int nStatusNum()
    {
        // ランダムな番号を生成し、それを返す
        int num = Random.Range(0, (int)eItem.eMax - 1);
        return num;
    }

    // オブジェクトの取得個数のカウントアップ
    // 引数：取得したアイテムのアイテム番号
    public void nCountup(int nItemStatus)
    {
        CountList[nItemStatus]++;
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
        image = itemImage.ItemImageList[itemID];
        return image;
    }
}
