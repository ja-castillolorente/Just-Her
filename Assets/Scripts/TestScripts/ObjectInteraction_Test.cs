using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectInteraction_Test : MonoBehaviour
{
    public string scriptName;
    public void OnMouseDown()
    {
        DirectorScript_Test.directorSingleton.scriptPlayer.PreloadAndPlayAsync(scriptName);
    }
}
