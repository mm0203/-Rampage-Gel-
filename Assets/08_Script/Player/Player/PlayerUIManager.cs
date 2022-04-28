using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] List<Slider> ImageList;
    [SerializeField] List<Volume> VolumeList;
    PlayerStatus status;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        // HP
        ImageList[0].value = (float)status.HP / (float)status.MaxHP;
        VolumeList[0].weight = 1 - ImageList[0].value;
        // Stamina
        ImageList[1].value = (float)status.Stamina / (float)status.MaxStamina;
        //VolumeList[1].weight = 1 - ImageList[1].value;
        // Exp
        ImageList[2].value = (float)status.Exp / (float)status.MaxExp;
        //VolumeList[2].weight = 1 - ImageList[2].value;
    }
}
