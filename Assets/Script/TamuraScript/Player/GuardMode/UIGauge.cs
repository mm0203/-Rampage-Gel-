//======================================================================
// UIGauge.cs
//======================================================================
// 開発履歴
//
// 2022/03/11 author：田村敏基 アタッチされたImageの塗り潰し量を変えるだけのスクリプト
//                                                        
//                             
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UIGauge : MonoBehaviour
{
    [SerializeField] private Image image;
    private GuardMode guard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Refresh(float max,float current)
    {
        //image.fillAmount = current / max;
    }
}
