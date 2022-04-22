//======================================================================
// Item.cs
//======================================================================
// 開発履歴
//
// 2022/04/09 author：畠山大輝 マネージャとの分岐、アイテム用
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemManager itemManager;
    private ItemUI itemUI;

    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        itemUI = GameObject.Find("ItemManager").GetComponent<ItemUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            // アイテム取得個数をカウント
            if(itemManager.nItemCount(1) == 1) itemUI.CreateItemUI();

            // オブジェクトを削除し、新しいオブジェクトを生成(仮)
            Destroy(this.gameObject);
            itemManager.CreateItem();
        }
    }
}
