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
    private Vignette Vignette;
    private ChromaticAberration chromaticAberration;

    void Start()
    {
        //ポストプロセスをゲット
        postProcessVolume = this.GetComponent<PostProcessVolume>();
        Vignette = this.GetComponent<Vignette>();
        chromaticAberration = this.GetComponent<ChromaticAberration>();
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
        //Vキーを押したら
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Vignette.active)
            {
                SetVignette(false);
            }
            else
            {
                SetVignette(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (chromaticAberration.active)
            {
                SetChromatic(false);
            }
            else
            {
                SetChromatic(true);
            }
        }
    }

    //ゲーム上でエフェクト効果をオンにするかどうか
    void SetIsGroval(bool b)
    {
        postProcessVolume.isGlobal = b;
    }
    //ダメージ表現効果をオンにするかどうか
    void SetVignette(bool b)
    {
        Vignette.active = b;
    }
    //ガード表現をオンにするかどうか
    void SetChromatic(bool b)
    {
        chromaticAberration.active = b;
    }
}
