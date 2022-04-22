using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusText : MonoBehaviour
{
    [SerializeField] GameObject Ogura;
    [SerializeField] GameObject PlayerObj;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerObj == null)
        PlayerObj = GameObject.FindWithTag("Player");
        Ogura.GetComponent<Text>().text = "HP:"              + PlayerObj.GetComponent<PlayerStatus>().HP +"\n"+
                                          "スタミナ:"        + PlayerObj.GetComponent<PlayerStatus>().Stamina +"\n"+
                                          "攻撃:"            + PlayerObj.GetComponent<PlayerStatus>().Attack +"\n"+
                                          "スピード:"        + PlayerObj.GetComponent<PlayerStatus>().Speed +"\n"+
                                          "バースト範囲:"    + PlayerObj.GetComponent<PlayerStatus>().BurstRadisu +"\n"+
                                          //"オート回復量:"    + PlayerObj.GetComponent<PlayerStatus>(). + "\n" +
                                          //"ドレイン:"        + PlayerObj.GetComponent<PlayerStatus>(). + "\n" +
                                          //"回復補正:"        + PlayerObj.GetComponent<PlayerStatus>(). + "\n" +
                                          //"残機:"            + PlayerObj.GetComponent<PlayerStatus>(). + "\n" +
                                          //"無敵時間:"        + PlayerObj.GetComponent<PlayerStatus>(). + "\n" +
                                          "バースト威力補正:" + PlayerObj.GetComponent<PlayerStatus>().BurstPower + "\n";
    }

    // Update is called once per frame    void Update()
    void Update()
    {
        
    }
}
