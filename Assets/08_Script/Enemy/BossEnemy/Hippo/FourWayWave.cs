using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourWayWave : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5.0f);   
    }

    private void Update()
    {
        gameObject.transform.position += new Vector3(gameObject.transform.forward.x * 3.0f * Time.deltaTime, gameObject.transform.forward.y, gameObject.transform.forward.z * 3.0f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // É_ÉÅÅ[ÉWèàóù
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack / 2);

            player.GetComponent<Rigidbody>().AddForce(this.transform.forward * 2500);
        }
    }
}
