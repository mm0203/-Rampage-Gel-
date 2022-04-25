//======================================================================
// BossDirection.cs
//======================================================================
// 開発履歴
//
// 2022/04/24 author:竹尾晃史郎　大ボス登場演出制作
// 2022/04/25                    Effect密結合中
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDirection : MonoBehaviour
{
    // 必要オブジェクトデータ
    GameObject Cameraobj = null;
    GameObject Bossobj = null;
    GameObject UIobj = null;
    EffectPlayer effectPlayer = null;
    [SerializeField] AnimationCurve Zoomcurve;
    [SerializeField] AnimationCurve Returncurve;

    Vector3 oldCameraPos = new Vector3();
    Vector3 BossPos = new Vector3();

    int nToFlame = 60;
    int nZoomTime = 3;
    int nShowTime = 2;
    int nReturnTime = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            StartDirection();
        }
    }

    public void StartDirection()
    {
        // シーンから必要なデータを取得
        Cameraobj = GameObject.FindWithTag("MainCamera");
        Bossobj = GameObject.FindWithTag("Boss");
        UIobj = GameObject.FindWithTag("UI");
        effectPlayer = Cameraobj.GetComponent<EffectPlayer>();

        nZoomTime = nZoomTime * nToFlame;
        nShowTime = nShowTime * nToFlame;
        nReturnTime = nReturnTime * nToFlame;

        oldCameraPos = Cameraobj.transform.position; // カメラの位置を保存     
        BossPos.x = Bossobj.transform.position.x;     // ボスの位置[X]を取得
        BossPos.y = oldCameraPos.y;                   // 高さ[Y]は固定
        BossPos.z = Bossobj.transform.position.z;     // ボスの位置[Z]を取得
        Cameraobj.GetComponent<CameraController>().bOnDirection = true; // プレイヤーへのカメラ追従無効化
        StartCoroutine(effectPlayer.BlackFogAnimaion(0));               // 黒い霧発生
        UIobj.SetActive(false);                                         // 演出に集中させるためUIを非表示に

        StartCoroutine(CameraMan());
    }

    IEnumerator CameraMan()
    {
        float percentPos;
        // 近寄る ==============================================
        for (int i = 0; i <= nZoomTime; i++)
        {
            yield return null;
            percentPos = Zoomcurve.Evaluate((float)i / nZoomTime);
            Cameraobj.transform.position = Vector3.Lerp(oldCameraPos, BossPos, percentPos);
        }
        // 止まる ==============================================
        StartCoroutine(effectPlayer.BlackFogAnimaion(1));
        for (int n = 0; n <= nShowTime; n++)
        {
            yield return null;
            
        }
        // 戻る ================================================
        for (int t = 0; t <= nReturnTime; t++)
        {
            yield return null;
            percentPos = Returncurve.Evaluate((float)t / nReturnTime);
            Cameraobj.transform.position = Vector3.Lerp(BossPos, oldCameraPos, percentPos);
        }
        Cameraobj.GetComponent<CameraController>().bOnDirection = false; // プレイヤーへのカメラ追従有効化
        UIobj.SetActive(true);
    }
}







