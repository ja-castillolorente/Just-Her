using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel.UI;
public class DialogueScript : MonoBehaviour
{
    RevealableTextPrinterPanel dialogueTextScript;
    public bool disableInteractionEvent;
    public bool zoomOutEvent;
    void Start()
    {
        dialogueTextScript = gameObject.GetComponent<RevealableTextPrinterPanel>();
        // Evento por el cual la cámara hace zoom out cuando se esconde el cuadro de diálogo
        if (zoomOutEvent == true)
        {
            dialogueTextScript.onHide.AddListener(DirectorScript.directorSingleton.StartZoomOut);
        }
        // Evento por el cual se desactiva la interacción cuando aparece el cuadro de diálogo
        if (disableInteractionEvent == true)
        {
            dialogueTextScript.onShow.AddListener(DirectorScript.directorSingleton.DisableObjectInteraction);
            dialogueTextScript.onHide.AddListener(DirectorScript.directorSingleton.EnableObjectInteraction);
        }
    }
}
