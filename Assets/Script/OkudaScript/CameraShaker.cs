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

    private Vector3 VOriginPosition;
    private Quaternion QOriginRotation;

    // 追加_揺らす前のカメラ座標
    private Vector3 VoldPos;
    private Quaternion QoldQuater;

    void Start()
    {
        VOriginPosition = ShakeObject.localPosition;
        QOriginRotation = ShakeObject.localRotation;
    }

    protected void Update()
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
        VoldPos = ShakeObject.localPosition;
        QoldQuater = ShakeObject.localRotation;

        float shakeIntensity = fShakeIntensity;
        while (shakeIntensity > 0)
        {
            // 揺らしている最中のカメラ座標取得
            VOriginPosition = ShakeObject.localPosition;
            QOriginRotation = ShakeObject.localRotation;

            ShakeObject.localPosition = VOriginPosition + Random.insideUnitSphere * shakeIntensity;
            ShakeObject.localRotation = new Quaternion(
                QOriginRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                QOriginRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                QOriginRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                QOriginRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount);
            shakeIntensity -= fShakeDecay;
            yield return false;
        }

        // 揺らす前のカメラ座標に戻す
        ShakeObject.localPosition = VoldPos;
        ShakeObject.localRotation = QoldQuater;
    }
}