using System;
using UnityEngine;
using UnityEngine.UI;   // 追加

#pragma warning disable 618
namespace UnityStandardAssets.Utility
{
<<<<<<< HEAD
    //public class SimpleActivatorMenu : MonoBehaviour
    //{
    //    // An incredibly simple menu which, when given references
    //    // to gameobjects in the scene
    //    public GUIText camSwitchButton;
    //    public GameObject[] objects;
=======
    public class SimpleActivatorMenu : MonoBehaviour
    {
        // An incredibly simple menu which, when given references
        // to gameobjects in the scene
        public Text camSwitchButton; // GUITextをTextに変更
        public GameObject[] objects;
>>>>>>> main


    //    private int m_CurrentActiveObject;


    //    private void OnEnable()
    //    {
    //        // active object starts from first in array
    //        m_CurrentActiveObject = 0;
    //        camSwitchButton.text = objects[m_CurrentActiveObject].name;
    //    }


    //    public void NextCamera()
    //    {
    //        int nextactiveobject = m_CurrentActiveObject + 1 >= objects.Length ? 0 : m_CurrentActiveObject + 1;

    //        for (int i = 0; i < objects.Length; i++)
    //        {
    //            objects[i].SetActive(i == nextactiveobject);
    //        }

    //        m_CurrentActiveObject = nextactiveobject;
    //        camSwitchButton.text = objects[m_CurrentActiveObject].name;
    //    }
    //}
}
