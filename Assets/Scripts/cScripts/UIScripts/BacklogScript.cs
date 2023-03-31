using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel.UI;
public class BacklogScript : MonoBehaviour
{
    BacklogPanel backlogPanelScript;
    void Start()
    {
        backlogPanelScript = gameObject.GetComponent<BacklogPanel>();
        backlogPanelScript.onShow.AddListener(DirectorScript.directorSingleton.DisableObjectInteraction);
        backlogPanelScript.onHide.AddListener(DirectorScript.directorSingleton.EnableObjectInteraction);
    }
}