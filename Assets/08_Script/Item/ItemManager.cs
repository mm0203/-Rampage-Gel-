//======================================================================
// ItemManager.cs
//======================================================================
// J­ð
//
// 2022/03/09 authorF©RåP ÀJn(íÞ,¶¬Ì¼À)
// 2022/03/15                  ACeÇÁªÌÀ(ªòðâ·¾¯)
// 2022/04/21 authorF|öWjY nItemCountC³A¼ÀpÉ²®
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    // ACeÌíÞ
    public enum eItem
    {
        eHp = 0,     // nHp
        eStamina,    // nStamina
        eAttack,     // nAttack
        eSpeed,      // fSpeed
        eBurstRange, // o[XgÍÍ
        eBurstDamage,// o[Xg_[W
        //eGuard,      // ¦¢gp
        //eHighBound,  // ¦¢gp
        eFire,       // Î®«
        eWater,      // ®«
        eThunder,    // ®«
        eDivision,   // ªô
        eAutoHeal,   // I[gñ
        eStaminaHeal,// X^~iñ¬x
        //eCombo,      // ¦¢gp
        eDrain,      // HPzû
        eReborn,     // c@
        //eWall,       // ¦¢gp
        //eAura,       // ¦¢gp
        eSonicBoom,  // ³GÔ

        eMax
    }

    [Header("ACeÌo»ÀWÍÍ")] [SerializeField, Range(1.0f, 100.0f)] float InstantiateX = 5.0f;
    [SerializeField, Range(1.0f, 100.0f)] float InstantiateZ = 5.0f;

    public List<GameObject> ItemObjectList;
    public List<int> CountList;
    public List<int> DrawItemList;
    public int nStatus;

    // ACeæf[^i|j
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

    // VKIuWFNgÌ¶¬Ö
    public void CreateItem()
    {
        Vector3 vPos;

        vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), transform.position.y, Random.Range(-InstantiateZ, InstantiateZ));
        nStatus = Random.Range(0, (int)eItem.eMax - 1);
        Instantiate(ItemObjectList[nStatus], vPos, Quaternion.identity);
    }

    // IuWFNgÌæ¾ÂðJEg
    public int nItemCount(int itemID)
    {
        CountList[itemID]++;
        //Debug.Log(itemID + "ÔACeF" + CountList[itemID] + "Â");

        if (CountList[itemID] == 1)
        {
            DrawItemList.Add(itemID);
            imageObject.GetComponent<Image>().sprite = itemImage.ItemImageList[itemID];
            GetComponent<ItemUI>().CreateItemUI(DrawItemList.Count, imageObject);
        }
        //Debug.Log(CountList[itemID]);
        return CountList[itemID];
    }

    /* ÈºAxAbvpÖ */

    // IuWFNgÌACeÔnµ
    public int nStatusNum()
    {
        // _ÈÔð¶¬µA»êðÔ·
        int num = Random.Range(0, (int)eItem.eMax - 1);
        return num;
    }

    // IuWFNgÌæ¾ÂÌJEgAbv
    // øFæ¾µ½ACeÌACeÔ
    public void nCountup(int nItemStatus)
    {
        CountList[nItemStatus]++;
    }

    // ACeIi|öj
    public int nItemGacha()
    {
        int nItemID = 0;
        nItemID = Random.Range(0, (int)eItem.eMax - 1);
        return nItemID;
    }

    // ACeænµi|j
    public Sprite setItemIcon(int itemID)
    {
        Sprite image;
        image = itemImage.ItemImageList[itemID];
        return image;
    }
}
