using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel.UI;
public class ConfirmationUIScript : MonoBehaviour
{
    ConfirmationPanel confirmationPanelScript;
    void Start()
    {
        confirmationPanelScript = gameObject.GetComponent<ConfirmationPanel>();
        confirmationPanelScript.onShow.AddListener(DirectorScript.directorSingleton.DisableObjectInteraction);
        confirmationPanelScript.onHide.AddListener(DirectorScript.directorSingleton.EnableObjectInteraction);
    }
}