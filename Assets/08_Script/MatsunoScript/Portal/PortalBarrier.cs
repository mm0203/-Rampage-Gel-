using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBarrier : MonoBehaviour
{
    // ポータルデータ
    public PortalData PortalData;
    int nHp = 2;

    void Start()
    {
    }

    void Update()
    {
       Debug.Log("あと" + nHp + "回で割れる");
    }

    // バリアの当たり判定
    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーと接触
        if (collision.transform.tag == "Player")
        {
            // バリア残量減らす
            PortalData.Hp--;
            nHp--;

            if (nHp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
