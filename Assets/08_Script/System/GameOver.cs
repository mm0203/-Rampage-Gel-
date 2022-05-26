using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    //ポーズ切り替え用フラグ
    [SerializeField] bool bGameOver;
    [SerializeField] GameObject gGameOverObj;

    private void Start()
    {
        bGameOver = false;
    }

    private void Update()
    {
        SetGameOverObj(true);
    }
    public void SetGameOverObj(bool b)
    {
        gGameOverObj.SetActive(b);
    }
}
