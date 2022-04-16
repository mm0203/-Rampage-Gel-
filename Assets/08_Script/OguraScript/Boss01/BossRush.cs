using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRush : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    Rigidbody rb;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // É_ÉÅÅ[ÉWèàóù
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<StatusComponent>().Attack);

            //Vector3 vec = new Vector3(Mathf.Abs(enemy.transform.forward.x * 10), 0.0f, Mathf.Abs(enemy.transform.forward.z * 10));
            //Debug.Log(vec);

            //rb.AddForce(0,0,100);

            Debug.Log("kore");

        }
    }
}
