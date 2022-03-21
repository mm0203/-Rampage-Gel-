//======================================================================
// CameraShaker.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：奥田達磨 カメラの揺れ作成
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

    void Start()
    {
        VOriginPosition = ShakeObject.localPosition;
        QOriginRotation = ShakeObject.localRotation;
    }

    protected void Update()
    {
        // キーボード移動 
        if (Input.GetMouseButton(0))
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
        float shakeIntensity = fShakeIntensity;
        while (shakeIntensity > 0)
        {
            ShakeObject.localPosition = VOriginPosition + Random.insideUnitSphere * shakeIntensity;
            ShakeObject.localRotation = new Quaternion(
                QOriginRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                QOriginRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                QOriginRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                QOriginRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount);
            shakeIntensity -= fShakeDecay;
            yield return false;
        }
        ShakeObject.localPosition = VOriginPosition;
        ShakeObject.localRotation = QOriginRotation;
    }
}