//======================================================================
// Scratch.cs
//======================================================================
// 開発履歴
//
// 2022/03/05 author：小椋駿 製作開始　敵のひっかき攻撃処理
// 2022/03/28 author：竹尾　応急　プレイヤーへのダメージ判定
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    MeshRenderer mr;

    public void SetPlayer (GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }


    private void Start()
    {
        // 当たり判定を透明に
        mr = GetComponent<MeshRenderer>();
        mr.material.color = new Color32(0, 0, 0, 0);
    }

    void Update()
    {
        Destroy(gameObject, 0.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
<<<<<<< HEAD:Assets/08_Script/OguraScript/Enemy01/Scratch.cs
        if (collision.transform.tag == "Player")
=======
        if (other.transform.tag == "Player")
>>>>>>> 8709684d4e54354a91684949987394adf606b0ff:Assets/Script/OguraScript/Enemy01/Scratch.cs
        {
            // ダメージ処理
            //player.GetComponent<StatusComponent>().HP -= enemy.GetComponent<StatusComponent>().Attack;
            //Debug.Log(player.GetComponent<StatusComponent>().HP);
<<<<<<< HEAD:Assets/08_Script/OguraScript/Enemy01/Scratch.cs

            //*応急*
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack);
=======
            Destroy(gameObject);
>>>>>>> 8709684d4e54354a91684949987394adf606b0ff:Assets/Script/OguraScript/Enemy01/Scratch.cs
        }
    }
}
