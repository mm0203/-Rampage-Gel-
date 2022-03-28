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

    // 追加_揺らす前のカメラ座標
    private Vector3 VoldPos;
    private Quaternion QoldQuater;

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
<<<<<<< HEAD:Assets/Script/OkudaScript/CameraShaker.cs
        VoldPos = ShakeObject.localPosition;
        QoldQuater = ShakeObject.localRotation;
=======
        vOldPos = ShakeObject.localPosition;
        qOldQuater = ShakeObject.localRotation;
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OkudaScript/CameraShaker.cs

        float shakeIntensity = fShakeIntensity;
        while (shakeIntensity > 0)
        {
            // 揺らしている最中のカメラ座標取得
<<<<<<< HEAD:Assets/Script/OkudaScript/CameraShaker.cs
            VOriginPosition = ShakeObject.localPosition;
            QOriginRotation = ShakeObject.localRotation;

            ShakeObject.localPosition = VOriginPosition + Random.insideUnitSphere * shakeIntensity;
=======
            vOriginPosition = ShakeObject.localPosition;
            qOriginRotation = ShakeObject.localRotation;

            ShakeObject.localPosition = vOriginPosition + Random.insideUnitSphere * shakeIntensity;
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OkudaScript/CameraShaker.cs
            ShakeObject.localRotation = new Quaternion(
                qOriginRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                qOriginRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                qOriginRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount,
                qOriginRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * fShakeAmount);
            shakeIntensity -= fShakeDecay;
            yield return false;
        }

        // 揺らす前のカメラ座標に戻す
<<<<<<< HEAD:Assets/Script/OkudaScript/CameraShaker.cs
        ShakeObject.localPosition = VoldPos;
        ShakeObject.localRotation = QoldQuater;
=======
        ShakeObject.localPosition = vOldPos;
        ShakeObject.localRotation = qOldQuater;
>>>>>>> d2f65eada7be6604d61b693afd0e28d3b8accd2c:Assets/08_Script/OkudaScript/CameraShaker.cs
    }
}