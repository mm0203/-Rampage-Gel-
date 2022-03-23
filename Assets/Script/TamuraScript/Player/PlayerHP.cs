using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    PlayerStatus status;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ダメージコールバック関数
    public void OnDamage(int damage)
    {
        // 0以下なら死んでるためリターン
        if (status.HP <= 0) return;

        // ダメージを与える
        status.HP -= damage;
    }
}
