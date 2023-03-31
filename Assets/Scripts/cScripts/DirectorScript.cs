using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;
using Naninovel.UI;
using Naninovel.Commands;
using System;
using UnityEngine.SceneManagement;
public class DirectorScript : MonoBehaviour
{
    static GameObject directorInstance;
    public static DirectorScript directorSingleton;
    public IScriptPlayer scriptPlayer;
    public IStateManager stateManager;
    public ICustomVariableManager variableManager;
    public IAudioManager audioManager;
    [HideInInspector] public string scriptName;
    public GameObject[] interactiveObjectsArray;
    CameraMovement cameraScript;
    FadingScreenScript blackScreenScript;
    FadingScreenScript whiteScreenScript;
    SaveManager saveManager;
    void Start()
    {
        if (directorInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            blackScreenScript = GameObject.Find("BlackScreenCanvas").GetComponentInChildren<FadingScreenScript>();
            whiteScreenScript = GameObject.Find("WhiteScreenCanvas").GetComponentInChildren<FadingScreenScript>();
            variableManager = Engine.GetService<ICustomVariableManager>();
            stateManager = Engine.GetService<IStateManager>();
            audioManager = Engine.GetService<IAudioManager>();
            saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
            stateManager.AddOnGameSerializeTask(saveManager.SerializeState);
            stateManager.AddOnGameDeserializeTask(saveManager.DeserializeState);
            SceneManager.activeSceneChanged += SearchScripts;
            directorSingleton = this;
            directorInstance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (cameraScript != null)
        {
            if (cameraScript.hasZoom_InEnded == true)
            {
                scriptPlayer.PreloadAndPlayAsync(scriptName);
                cameraScript.hasZoom_InEnded = false;
            }
        }
    }
    void SearchScripts (Scene currentScene, Scene newScene)
    {
        if (GameObject.Find("Main Camera") == true)
        {
            cameraScript = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        }
        interactiveObjectsArray = GameObject.FindGameObjectsWithTag("InteractiveObject");
        // Busca los servicios de Naninovel
        scriptPlayer = Engine.GetService<IScriptPlayer>();
        stateManager = Engine.GetService<IStateManager>();
        variableManager = Engine.GetService<ICustomVariableManager>();
        audioManager = Engine.GetService<IAudioManager>();
        // Encuentra la cámara asiganda al fundido
        blackScreenScript.screenCanvas.worldCamera = GameObject.Find("NewUICamera(Clone)").GetComponent<Camera>();
        whiteScreenScript.screenCanvas.worldCamera = GameObject.Find("NewUICamera(Clone)").GetComponent<Camera>();
    }
    public void EnableObjectInteraction()
    {
        foreach (GameObject interactiveObject in interactiveObjectsArray)
        {
            Collider collider = interactiveObject.GetComponent<Collider>();
            collider.enabled = true;
        }
    }
    public void DisableObjectInteraction()
    {
        foreach (GameObject interactiveObject in interactiveObjectsArray)
        {
            Collider collider = interactiveObject.GetComponent<Collider>();
            collider.enabled = false;
        }
    }
    public void StartZoomOut()
    {
        if (cameraScript != null)
        {
            cameraScript.StartCoroutine("ZoomOut");
        }
    }
}
