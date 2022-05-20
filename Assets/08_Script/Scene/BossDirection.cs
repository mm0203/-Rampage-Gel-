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
    public GameObject Bossobj = null;
    GameObject UIobj = null;
    EffectPlayer effectPlayer = null;
    BGMPlayer BGMPlayer = null;
    [SerializeField] AnimationCurve Zoomcurve;
    [SerializeField] AnimationCurve Returncurve;

    Vector3 oldCameraPos = new Vector3();
    Vector3 BossPos = new Vector3();

    int nToFlame = 60;
    int nZoomTime = 3;
    int nShowTime = 2;
    int nReturnTime = 1;

    // 演出中か
    public bool bDirection = false;

    private void Update()
    {
        setBossPosition();
    }

    public void StartDirection(int bgmNumber)
    {
        // 演出開始
        bDirection = true;

        nToFlame = 60;
        nZoomTime = 3;
        nShowTime = 2;
        nReturnTime = 1;

        // シーンから必要なデータを取得
        Cameraobj = GameObject.FindWithTag("MainCamera");
        Bossobj = GameObject.FindWithTag("DirectionPoint");
        UIobj = GameObject.FindWithTag("UI");
        BGMPlayer = GameObject.FindWithTag("SoundPlayer").gameObject.GetComponent<BGMPlayer>();
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

        StartCoroutine(CameraMan(bgmNumber));
    }

    IEnumerator CameraMan(int Number)
    {
        BGMPlayer.StopBGM();
        

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

        // ステージに応じたBGMを鳴らす
        switch(Number)
        {
            case 1:
                BGMPlayer.Stage1_Boss(); ;
                break;
            case 2:
                BGMPlayer.Stage2_Boss(); ;
                break;
            case 3:
                BGMPlayer.Stage3_Boss(); ;
                break;
            case 4:
                BGMPlayer.Stage4_Boss(); ;
                break;
            case 5:
                BGMPlayer.Stage5_Boss(); ;
                break;
            case 6:
                BGMPlayer.Stage6_Boss(); ;
                break;
            case 7:
                BGMPlayer.Stage7_Boss(); ;
                break;

            default:
                Debug.LogError("該当しないBGMが選択されました");
                break;
        }
        

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

        // 演出終了
        bDirection = false;
    }

    // 動くボスをずっと捉えておく
    void setBossPosition()
    {
        if(Bossobj == null)
        {
           
        }
        else
        {
            BossPos.x = Bossobj.transform.position.x;     // ボスの位置[X]を取得
            BossPos.y = oldCameraPos.y;                   // 高さ[Y]は固定
            BossPos.z = Bossobj.transform.position.z;     // ボスの位置[Z]を取得
        }
        
    }
}







