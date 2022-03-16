using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

//======================================================================
// PostProcessControl.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：奥田達磨 ダメージ表現オンオフ実装
//
//======================================================================

public class PostProcessControl : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume postProcessVolume;


    void Start()
    {
        //ポストプロセスをゲット
        postProcessVolume = this.GetComponent<PostProcessVolume>();
    
    }

    void Update()
    {
        //リターンキー押したら
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //エフェクト効果そのもののオンオフ切り替え
            if(postProcessVolume.isGlobal)
            {
                SetIsGroval(false);
            }
            else
            {
                SetIsGroval(true);
            }  
        }
    }
    

    //ゲーム上でエフェクト効果をオンにするかどうか
    void SetIsGroval(bool b)
    {
        postProcessVolume.isGlobal = b;
    }
 
}
