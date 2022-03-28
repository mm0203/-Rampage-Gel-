using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] List<Slider> ImageList;
    PlayerStatus status;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        ImageList[0].value = status.MaxHP / status.HP;
        ImageList[1].value = status.MaxGuard / status.Guard;
        ImageList[2].value = status.MaxExp / status.Exp;
    }
}
