using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel.UI;
public class PauseMenuScript : MonoBehaviour
{
    PauseUI pauseUIscript;
    void Start()
    {
        pauseUIscript = gameObject.GetComponent<PauseUI>();
        pauseUIscript.onShow.AddListener(DirectorScript.directorSingleton.DisableObjectInteraction);
        pauseUIscript.onHide.AddListener(DirectorScript.directorSingleton.EnableObjectInteraction);
    }
}