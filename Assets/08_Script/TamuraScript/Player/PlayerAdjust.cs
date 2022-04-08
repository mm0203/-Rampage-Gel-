using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdjust : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(Mathf.Cos(parent.transform.localEulerAngles.y * Mathf.Deg2Rad) * 20.0f, 
            0, 
            Mathf.Sin(parent.transform.localEulerAngles.y * Mathf.Deg2Rad) * 20.0f);
        Debug.Log(Mathf.Cos(parent.transform.localEulerAngles.y * Mathf.Deg2Rad));
    }
}
