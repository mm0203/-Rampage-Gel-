//======================================================================
// BossDirection.cs
//======================================================================
// 開発履歴
//
// 2022/04/24 author:竹尾晃史郎　大ボス登場演出制作
// 2022/04/25                    Effect密結合
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDirection : MonoBehaviour
{
    // 必要オブジェクトデータ
    GameObject Cameraobj = null;
    GameObject Bossobj        = null;
    GameObject UIobj          = null;
    EffectPlayer effectPlayer = null;
    [SerializeField] AnimationCurve Gocurve; // Editor用
    [SerializeField] AnimationCurve Backcurve; // Editor用

    Vector3 oldCameraPos = new Vector3();
    Vector3 BossPos = new Vector3();
    
    float fMovetime = 3.0f; // 移動時間
    private float startTime;
    

    

    private void Start()
    {
        StartDirection();
    }

    void Update()
    {
        MoveCamera(oldCameraPos, BossPos);       
    }

    public void StartDirection()
    {
        // シーンから必要なデータを取得
        Cameraobj = GameObject.FindWithTag("MainCamera");
        Bossobj = GameObject.FindWithTag("Boss"); 
        UIobj = GameObject.FindWithTag("UI");   
        effectPlayer = Cameraobj.GetComponent<EffectPlayer>();

        oldCameraPos = Cameraobj.transform.position ; // カメラの位置を保存     
        BossPos.x = Bossobj.transform.position.x;     // ボスの位置[X]を取得
        BossPos.y = oldCameraPos.y;
        BossPos.z = Bossobj.transform.position.z;     // ボスの位置[Z]を取得

        Cameraobj.GetComponent<CameraController>().bOnDirection = true;
        StartCoroutine(effectPlayer.BlackFogAnimaion(0)); // 黒い霧発生
        UIobj.SetActive(false);           // 演出に集中させるためUIを非表示に

    }

    

    void MoveCamera(Vector3 startPos, Vector3 endPos)
    {
        var diff = Time.timeSinceLevelLoad - startTime;
        if (diff > fMovetime) // 時間になったら止まる
        {
            transform.position = endPos;
            StartCoroutine(effectPlayer.BlackFogAnimaion(1));
            
            Cameraobj.GetComponent<CameraController>().bOnDirection = effectPlayer.bCompBlackFog;
            UIobj.SetActive(true);
            enabled = false;
        }

        var rate = diff / fMovetime;
        var pos = Gocurve.Evaluate(rate);

        transform.position = Vector3.Lerp(startPos, endPos, rate);
        transform.position = Vector3.Lerp(startPos, endPos, pos);
    }

    




    // Inspectorのエディター ***********************************
    void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR

        if (!UnityEditor.EditorApplication.isPlaying || enabled == false)
        {
            oldCameraPos = transform.position;
        }

        UnityEditor.Handles.Label(BossPos, BossPos.ToString());
        UnityEditor.Handles.Label(oldCameraPos, oldCameraPos.ToString());
#endif
        Gizmos.DrawSphere(BossPos, 0.1f);
        Gizmos.DrawSphere(oldCameraPos, 0.1f);

        Gizmos.DrawLine(oldCameraPos, BossPos);
    }
    //**********************************************************
}
