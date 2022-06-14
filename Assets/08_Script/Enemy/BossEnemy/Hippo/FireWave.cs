using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : MonoBehaviour
{

    // ダメージを与える間隔
    float fInterval = 0.5f;
    float fTime;

    GameObject player;
    GameObject enemy;
    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    private void Start()
    {
        // 5秒で消滅
        Destroy(gameObject, 5.0f);
    }

    // Start is called before the first frame update
    void Update()
    {
        // 敵死亡時、当たり判定用のキューブも消える
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // ダメージ処理
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack);

            //player.GetComponent<Rigidbody>().AddForce(this.transform.forward * 1500);
        }
    }

    // 火柱に当たり続けているとき
    private void OnTriggerStay(Collider other)
    {
        // プレイヤーに当たった時
        if (other.tag == "Player")
        {
            fTime -= Time.deltaTime;

            // 設定インターバル毎にダメージを与える(0.5秒)
            if (fTime < 0.0f)
            {
                // ダメージ処理
                player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack / 10);

                fTime = fInterval;
            }

        }
    }
}
