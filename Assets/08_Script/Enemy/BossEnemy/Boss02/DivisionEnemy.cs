//======================================================================
// DivisionEnemy.cs
//======================================================================
// ŠJ”­—š—ğ
//
// 2022/04/26 authorF¼–ì«”V •ª—ôŒã‚ÌÀ‘•@’Ç](ƒKƒoƒKƒo‚È‚Ì‚Å—vC³)
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DivisionEnemy : MonoBehaviour
{
    private GameObject player;

    private NavMeshAgent myAgent;

    private bool bStop = false;

    private float fTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        // NavMeshAgent‚ğ•Û‚µ‚Ä‚¨‚­
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        fTime -= Time.deltaTime;


        if (fTime <= 0)
        {
            bStop = true;
        }

        Move();
    }

    private void Move()
    {
        if (!bStop)
        {
            // Ÿ‚ÌêŠ‚ğŒvZ
            Vector3 nextPoint = myAgent.steeringTarget;
            Vector3 targetDir = nextPoint - transform.position;

            // ‰ñ“]
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

            // ƒvƒŒƒCƒ„[‚ğ’Ç‚¢‚©‚¯‚é
            myAgent.SetDestination(player.transform.position);
        }

        //if(myAgent.stoppingDistance <= 4.0f)
        //{
        //    bStop = true;
        //}
    }
}
