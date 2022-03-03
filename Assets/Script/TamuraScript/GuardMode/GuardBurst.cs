using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スフィアコライダーを要求する
[RequireComponent(typeof(SphereCollider))]
public class GuardBurst : MonoBehaviour
{
    [SerializeField] private float radius = 5.0f;
    [SerializeField] private float power = 10.0f;

    SphereCollider col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<SphereCollider>();
        radius = col.radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        // 要素の中身分ループする
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 吹き飛ばす
                rb.AddExplosionForce(power, transform.position, radius);
            }
        }
    }
}
