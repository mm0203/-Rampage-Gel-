using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBarrier : MonoBehaviour
{
    public PortalData PortalData;

    void Start()
    {
    }

    void Update()
    {
        Debug.Log(PortalData.Hp);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            PortalData.Hp--;

            if (PortalData.Hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
