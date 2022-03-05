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
        // “–‚½‚è”»’è‚ğ“§–¾‚É
        mr = GetComponent<MeshRenderer>();
        mr.material.color = new Color32(0, 0, 0, 0);
    }

    void Update()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {           
            // ƒ_ƒ[ƒWˆ—
            //player.GetComponent<StatusComponent>().HP -= enemy.GetComponent<StatusComponent>().Attack;
            //Debug.Log(player.GetComponent<StatusComponent>().HP);
        }

       Destroy(gameObject);
    }
}
