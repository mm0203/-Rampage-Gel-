using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    // オブジェクトを映すカメラ
    [SerializeField] private Camera TargetCamera;

    // UIを表示させる対象オブジェクト
    [SerializeField] private Transform TargetObject;

    // 表示するUI
    [SerializeField] private Transform ViewUI;

    // オブジェクト位置のオフセット
    [SerializeField] private Vector3 Offset;

    private RectTransform ParentUI;


    //---------------------
    // 初期化
    //---------------------
    private void Awake()
    {
        // カメラが指定されていなければメインカメラにする
        if (TargetCamera == null)
            TargetCamera = Camera.main;

        // 親UIのRectTransformを取得
        ParentUI = ViewUI.parent.GetComponent<RectTransform>();
    }

    //---------------------
    // 更新
    //---------------------
    private void Update()
    {
        // UIの位置を毎フレーム更新
        OnUpdatePosition();
    }

    //---------------------
    // UIの位置を更新する
    //---------------------
    private void OnUpdatePosition()
    {
        Transform cameraTransform = TargetCamera.transform;

        // カメラの向きベクトル
        Vector3 cameraDir = cameraTransform.forward;

        // オブジェクトの位置
        Vector3 targetWorldPos = TargetObject.position + Offset;

        // カメラからターゲットへのベクトル
        Vector3 targetDir = targetWorldPos - cameraTransform.position;

        // カメラの前方か
        bool bFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // カメラ前方ならUI表示、後方なら非表示
        ViewUI.gameObject.SetActive(bFront);
        if (!bFront) return;

        // オブジェクトのワールド座標から、スクリーン座標へ変換
        Vector3 ScreenPos = TargetCamera.WorldToScreenPoint(targetWorldPos);

        // スクリーン座標から、UIローカル座標へ変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(ParentUI, ScreenPos, null,out Vector2 uiLocalPos);

        // RectTransformのローカル座標を更新
        ViewUI.localPosition = uiLocalPos;
    }
}