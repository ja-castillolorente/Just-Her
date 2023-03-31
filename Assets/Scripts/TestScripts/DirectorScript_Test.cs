using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;
public class DirectorScript_Test : MonoBehaviour
{
    public static DirectorScript_Test directorSingleton;
    public IScriptPlayer scriptPlayer;
    void Start()
    {
        directorSingleton = this;
        scriptPlayer = Engine.GetService<IScriptPlayer>();
    }
}
