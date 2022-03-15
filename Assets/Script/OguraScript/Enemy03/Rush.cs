using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }


    void Start()
    {
        
    }

    void Update()
    {
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        enemy.transform.position += enemy.transform.forward * (Time.deltaTime * 5.0f);

        // ìGÇ∆àÍèèÇ…ìÆÇ≠
        transform.position = enemy.transform.position;

        Destroy(gameObject, 1.0f);
    }

    
}
