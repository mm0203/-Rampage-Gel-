using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreatePortaDataBase")]

public class PortalDataBase : ScriptableObject
{
    [Header("PortalDataBase")]

    [SerializeField]
    public List<PortalData> PortalList = new List<PortalData>();

    //　ポータルリストを返す
    public List<PortalData> GetPortalList()
    {
        return PortalList;
    }
}
