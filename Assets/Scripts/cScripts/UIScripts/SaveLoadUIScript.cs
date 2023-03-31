using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel.UI;
public class SaveLoadUIScript : MonoBehaviour
{
    SaveLoadMenu saveLoadMenuScript;
    void Start()
    {
        saveLoadMenuScript = gameObject.GetComponent<SaveLoadMenu>();
        saveLoadMenuScript.onShow.AddListener(DirectorScript.directorSingleton.DisableObjectInteraction);
        saveLoadMenuScript.onHide.AddListener(DirectorScript.directorSingleton.EnableObjectInteraction);
    }
}