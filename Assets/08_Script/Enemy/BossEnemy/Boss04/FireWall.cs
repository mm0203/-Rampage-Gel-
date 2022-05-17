//======================================================================
// FireWall.cs
//======================================================================
// 開発履歴
//
// 2022/05/11 author 竹尾：火の壁処理
//                         アニメーションイベントに起因して起こす
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{

    public bool bInArea = false;

    private void Start()
    {
        bInArea = false;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("範囲内");
            bInArea = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("範囲外");
            bInArea = false;
        }
    }


}
