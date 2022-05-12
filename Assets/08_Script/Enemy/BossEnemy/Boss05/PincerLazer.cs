//======================================================================
// Bullet.cs
//======================================================================
// 開発履歴
//
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PincerLazer : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    [SerializeField] GameObject LazerCollider;

    Vector3 vAttacksize = new Vector3(50.0f, 1.0f, 2.0f);

    public float fLockOnTime = 5.0f;
    public int nLazerTime = 120;
    public bool bHit = false;
    public int nSetAttack;
    bool bStart = false;

    public void SetEnemy(GameObject obj) { enemy = obj; }



    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        bHit = false;
        bStart = false;

    }

    private void Update()
    {
        fLockOnTime -= Time.deltaTime;

        if (fLockOnTime >= 1)
        {
            this.transform.position = player.transform.position;
        }
        else
        {
            if(bStart == false)
            {
                // レーザー発射（っぽく見せてる）
                StartCoroutine(FireLazer());
                bStart = true;
            }

            if(bHit == true)
            {
                nSetAttack = enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack;
                player.GetComponent<PlayerHP>().OnDamage(nSetAttack / 10);
            }
        }

        
    }

    IEnumerator FireLazer()
    {
        GameObject obj;
        obj = Instantiate(LazerCollider, this.transform.position, Quaternion.identity);
        

        for (int i = 0; i < nLazerTime; i++)
        {
            yield return null;
            bHit = obj.GetComponent<HitLazer>().bInArea;
        }

        Destroy(obj);
        Destroy(this.gameObject);
    }
}
