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
        ImageList[0].value = (float)status.HP / (float)status.MaxHP;
        ImageList[1].value = (float)status.Stamina / (float)status.MaxStamina;
        ImageList[2].value = (float)status.Exp / (float)status.MaxExp;
    }
}
