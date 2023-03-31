using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel.UI;
public class SettingsScript : MonoBehaviour
{
    GameSettingsMenu settingsMenuscript;
    void Start()
    {
        settingsMenuscript = gameObject.GetComponent<GameSettingsMenu>();
        settingsMenuscript.onShow.AddListener(DirectorScript.directorSingleton.DisableObjectInteraction);
        settingsMenuscript.onHide.AddListener(DirectorScript.directorSingleton.EnableObjectInteraction);
    }
}