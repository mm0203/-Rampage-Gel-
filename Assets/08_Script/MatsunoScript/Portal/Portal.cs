using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public SceneObject m_nextScene;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            FadeManager.Instance.LoadScene("TesrScene", 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            FadeManager.Instance.LoadScene("TesrScene", 1.0f);
        }
    }
}
