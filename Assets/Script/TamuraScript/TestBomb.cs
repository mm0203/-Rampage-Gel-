using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBomb : MonoBehaviour
{
    public float power;
    public float radius;
    public float upwardsModifier;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<Rigidbody>().AddExplosionForce(power, new Vector3(0f, 0f, 0f), radius, upwardsModifier, ForceMode.Impulse);
        }
    }
}