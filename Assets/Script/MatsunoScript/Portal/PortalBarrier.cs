using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBarrier : MonoBehaviour
{
    // ポータルデータ
    public PortalData PortalData;

    void Start()
    {
    }

    void Update()
    {
       // Debug.Log(PortalData.Hp);
    }

    // バリアの当たり判定
    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーと接触
        if (collision.transform.tag == "Player")
        {
            // バリア残量減らす
            PortalData.Hp--;

            if (PortalData.Hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
