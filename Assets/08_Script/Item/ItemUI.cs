//======================================================================
// ItemUI.cs
//======================================================================
// 開発履歴
//
// 2022/04/09 author：畠山大輝 実装開始
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] GameObject itemImage;

    [SerializeField] GameObject canvas;

    private int nUINum = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateItemUI()
    {
        GameObject prefab = (GameObject)Instantiate(itemImage, new Vector3(-740.0f + 90.0f * nUINum, 365.0f, 0.0f), Quaternion.identity);
        prefab.transform.SetParent(canvas.transform, false);

        nUINum++;
    }
}
