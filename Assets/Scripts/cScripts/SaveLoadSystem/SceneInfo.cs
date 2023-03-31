using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class SceneInfo
{
    public string sceneName;
    public List<InteractiveObjectInfo> interactiveObjects;
    public List<NonInteractiveObjectInfo> nonInteractiveObjects;
    public string bgmName;
}
