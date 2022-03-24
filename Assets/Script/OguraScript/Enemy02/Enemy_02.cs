//======================================================================
// Enemy_02.cs
//======================================================================
// J­ð
//
// 2022/03/05 authorF¬¸x »ìJn@GÌ£U
//
//======================================================================



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Enemy_02 : MonoBehaviour
{
    GameObject spher;
    EnemyBase enemyBase;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    private void AttackEnemy02()
    {
        // eð¶¬µÄòÎ·
        spher = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // eÌTCYAÀWApxÝè
        spher.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        spher.transform.rotation = this.transform.rotation;
        spher.transform.position = new Vector3(transform.position.x + transform.forward.x * 0.5f, transform.position.y, transform.position.z + transform.forward.z * 0.5f);

        // eÌR|[lg²®
        spher.AddComponent<Bullet>();
        spher.GetComponent<Bullet>().Speed = 3.0f;
        spher.GetComponent<Bullet>().SetPlayer(enemyBase.GetComponent<EnemyBase>().GetPlayer);
        spher.GetComponent<Bullet>().SetEnemy(this.gameObject);
        spher.AddComponent<Rigidbody>();
        spher.GetComponent<Rigidbody>().useGravity = false;
        spher.GetComponent<Rigidbody>().isKinematic = true;
        spher.GetComponent<SphereCollider>().isTrigger = true;
    }
}
