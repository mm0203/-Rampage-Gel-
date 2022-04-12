using System.Collections;
using UnityEngine;

public class Marker : MonoBehaviour
{
    // 敵オブジェクト
    [SerializeField]private GameObject Target;

    private RectTransform Rect;

    // オフセット
    private Vector3 offset = new Vector3(0, 1.5f, 0);

    // 限界距離？（画面の大きさにより変わるため没）
    [SerializeField] private Vector2 vPos = new Vector2(620, 350);
    private float fPos = 40.0f;


    void Start()
    {
        Rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        // ターゲット消滅時、同時に消滅
        if (Target == null) Destroy(gameObject);


        // UI座標の更新
        if(Rect.position.x < vPos.x && Rect.position.x > fPos)
            Rect.position = new Vector2(RectTransformUtility.WorldToScreenPoint(Camera.main, Target.transform.position + offset).x, Rect.position.y);

        if (Rect.position.y < vPos.y && Rect.position.y > fPos)
            Rect.position = new Vector2(Rect.position.x, RectTransformUtility.WorldToScreenPoint(Camera.main, Target.transform.position + offset).y);


        //Rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, Target.position + offset);
    }


}