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
        // íeÇê∂ê¨ÇµÇƒîÚÇŒÇ∑
        spher = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        spher.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        spher.transform.rotation = this.transform.rotation;
        spher.transform.position = new Vector3(transform.position.x + transform.forward.x, transform.position.y, transform.position.z + transform.forward.z);
        spher.AddComponent<Bullet>();
        spher.GetComponent<Bullet>().Speed = 3.0f;
        spher.GetComponent<Bullet>().SetPlayer(enemyBase.GetComponent<EnemyBase>().GetPlayer);
        spher.GetComponent<Bullet>().SetEnemy(this.gameObject);
        spher.AddComponent<Rigidbody>();
        spher.GetComponent<Rigidbody>().useGravity = false;
        spher.GetComponent<Rigidbody>().isKinematic = true;
    }
}
