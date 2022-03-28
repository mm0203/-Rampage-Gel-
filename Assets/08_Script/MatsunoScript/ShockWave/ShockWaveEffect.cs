//======================================================================
// ShockWaveEffect.cs
//======================================================================
// 開発履歴
//
// 2022/03/15 author：松野将之 ショックウェーブみたいなエフェクト実装
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveEffect : MonoBehaviour
{
    // 再生時間
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float timer = 0.5f;

    // 位置
    [Range(-1.5f, 1.5f)]
    public float posX = 0.5f;
    [Range(-1.5f, 1.5f)]
    public float posY = 0.5f;

    // 波の速度
    [Range(0f, 10f)]
    public float duration = 1f;

    // 光の反射
    [SerializeField]
    [Range(0f, 2f)]
    protected float shine = 0.5f;

    // 波紋のサイズ
    [SerializeField]
    [Range(0f, 1f)]
    protected float distortion = 0.1f;

    // 波紋の幅
    [SerializeField]
    [Range(0f, 10f)]
    protected float width = 0.5f;

    [SerializeField]
    protected Material material;

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        // マテリアルの各情報をセット
        material.SetFloat("_TimeOffset", (4.0f * timer - 2.0f));
        material.SetFloat("_PosX", posX);
        material.SetFloat("_PosY", posY);
        material.SetFloat("_ShineMag", shine);
        material.SetFloat("_DistortionMag", distortion);
        material.SetFloat("_WidthRev", 1.0f / width);
        material.SetFloat("_AspectRatio", sourceTexture.width / (float)sourceTexture.height);
        Graphics.Blit(sourceTexture, destTexture, material);
    }
    void Update()
    {
        // 時間経過で速度を調整
        timer += Time.deltaTime * (1.0f / duration);
        if (timer > 1.0f)
        {
            enabled = false;
        }
    }

    // 呼び出し関数
    public void StartEffect(Vector3 world_pos)
    {
        // カメラの座標を取得
        var camera = GetComponent<Camera>();
        var screen_pos = camera.WorldToScreenPoint(world_pos);
        enabled = true;
        timer = 0.0f;

        // カメラの位置から呼び出す場所を算出
        posX = (screen_pos.x) / camera.pixelWidth;
        posY = (screen_pos.y) / camera.pixelHeight;
    }

    // 最初に呼ばれる用のテスト
    [ContextMenu("startEffectTest")]
    private void startEffectTest()
    {
        StartEffect(Vector3.zero);
    }
}
