//======================================================================
// Stop.cs
//======================================================================
// 開発履歴
//
// 2022/03/1 author：田村敏基 移動速度を0にする
// 2022/03/5 author：田村敏基 プランナー依頼。徐々に止まるように変更                                                          
//                             
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void DoStop(Rigidbody rb)
    {
        // 停止可能なら停止する
        rb.velocity *= 0.9f;
    }
}
