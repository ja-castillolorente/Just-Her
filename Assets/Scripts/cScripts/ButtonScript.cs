using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;
public class ButtonScript : MonoBehaviour
{
    public Script buttonScript;
    public void PlayScript()
    {
        DirectorScript.directorSingleton.scriptPlayer.PreloadAndPlayAsync(buttonScript.Name);
    }
}
