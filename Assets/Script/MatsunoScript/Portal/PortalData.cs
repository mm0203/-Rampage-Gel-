using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MatsunoScript/MyScriptable/Create PortaData")]

public class PortalData : ScriptableObject
{
    [Header("Portal")]

    // ƒ|[ƒ^ƒ‹‚ªŠ„‚ê‚é‚Ü‚Å‚Ì‰ñ”
    [System.NonSerialized]
    public int Hp = 2;
}
