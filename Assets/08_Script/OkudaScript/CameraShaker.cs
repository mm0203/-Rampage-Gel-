//======================================================================
// CameraShaker.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：奥田達磨 カメラの揺れ作成
// 2022/03/24 author：竹尾晃史郎 カメラ飛び修正
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShaker : MonoBehaviour
{
    [Header("Shake")]
    // ここにカメラオブジェクトを設定する
    public Transform ShakeObject = null;
    // カメラの揺れの強さ
    public float fShakeIntensity = 0.02f;
    // 揺れの減算値
    public float fShakeDecay = 0.002f;
    // 揺れの強さ係数
    public float fShakeAmount = 0.2f;

    private Vector3 vOriginPosition;
    private Quaternion qOriginRotation;

    // 追加_揺らす前のカメラ座標
    private Vector3 vOldPos;
    private Quaternion qOldQuater;

    void Start()
    {
        vOriginPosition = ShakeObject.localPosition;
        qOriginRotation = ShakeObject.localRotation;
    }

    void Update()
    {
        // キーボード移動 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Do();
        }
    }

    public void Do()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    public IEnumerator Shake()
    {
        // 揺らす前のカメラ座標取得
        vOldPos = ShakeObject.localPosition;
        qOldQuater = ShakeObject.localRotation;

        float shakeIntensity = fShakeIntensity;
        while (shakeIntensity > 0)
        {
            // 揺らしている最中のカメラ座標取得
            vOriginPosition = ShakeObject.localPosition;
            qOriginRotation = ShakeObject.localRotation;

            ShakeObject.localPosition = vOriginPosition + Random.insideUnitSphere * shakeIntensity;
            ShakeObject.localRotation = new Quaternion(
                qOriginRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                qOriginRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                qOriginRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                qOriginRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount);
            shakeIntensity -= fShakeDecay;
            yield return false;
        }

        // 揺らす前のカメラ座標に戻す
        ShakeObject.localPosition = vOldPos;
        ShakeObject.localRotation = qOldQuater;
    }
}