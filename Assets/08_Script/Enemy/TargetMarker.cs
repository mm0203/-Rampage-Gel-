
//======================================================================
// TargetMaker.cs
//======================================================================
// 開発履歴
//
// 2022/04/22 author：小椋駿　製作開始
//

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]

public class TargetMarker : MonoBehaviour
{
    // マーカーを出すターゲット
    [SerializeField]private Transform target;

    // 矢印画像
    [SerializeField]private Image arrow;

    private Camera mainCamera;
    private RectTransform rectTransform;

    private void Start()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // ターゲットがいないとき更新しない
        if (target == null) return;

        // キャンバスの大きさ取得
        float canvasScale = transform.root.localScale.z;

        // 中央座標取得
        Vector3 center = 0.5f * new Vector3(Screen.width, Screen.height);

        // ターゲットのスクリーン座標を求める
        Vector3 pos = mainCamera.WorldToScreenPoint(target.position) - center;

        // 画面端の表示位置調整
        Vector2 halfSize = 0.5f * canvasScale * rectTransform.sizeDelta;
        float d = Mathf.Max(Mathf.Abs(pos.x / (center.x - halfSize.x)),Mathf.Abs(pos.y / (center.y - halfSize.y)));

        // ターゲットのスクリーン座標が画面外なら、画面端になるよう調整する
        bool isOffscreen = (pos.z < 0f || d > 1f);
        if (isOffscreen)
        {
            pos.x /= d;
            pos.y /= d;
        }
        rectTransform.anchoredPosition = pos / canvasScale;

        // ターゲットが画面外なら、矢印表示
        arrow.enabled = isOffscreen;
        if (isOffscreen)
        {
            arrow.rectTransform.eulerAngles = new Vector3(0.0f, 0.0f,Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg);
        }
    }
}
