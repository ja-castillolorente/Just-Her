using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UniRx.Async;
using Naninovel;
using Naninovel.Commands;
public class SaveManager : MonoBehaviour
{
    GameObject managerInstance;
    // Arrays para la carga y descarga de información
    string lastSceneName;
    GameObject[] interactiveObjectsArray;
    GameObject[] nonInteractiveObjectsArray;
    // Variables a guardar
    SceneInfo currentSceneInfo;
    List<InteractiveObjectInfo> interactiveObjectInfoList;
    List<NonInteractiveObjectInfo> nonInteractiveObjectInfoList;
    GameObject audioController;
    AudioSource audioSource;
    // Lista de escenas con su información (lo que se guardará en el archivo de guardado)
    public List<SceneInfo> scenesInfoList;
    //public List<string> objectNames;
    //public List<bool> objectActivationStates;
    //public List<bool> colliderActivationStates;
    // Variables para el sistema de guardado
    GameState savedState;
    GameState loadedState;
    public Script loadScript;
    [System.Serializable]
    public class GameState
    {
        public string savedSceneName;
        public List<SceneInfo> savedScenesInfo;
    }
    void Start()
    {
        if (managerInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            managerInstance = gameObject;
            interactiveObjectInfoList = new List<InteractiveObjectInfo>();
            nonInteractiveObjectInfoList = new List<NonInteractiveObjectInfo>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // 1. Se crea la lista de información de la escena actual
    public void CreateSceneInfo()
    {
        // Se guarda la información de cada objeto interactivo en la lista temporal correspondiente
        interactiveObjectsArray = GameObject.FindGameObjectsWithTag("InteractiveObject");
        foreach (GameObject gameObject in interactiveObjectsArray)
        {
            InteractiveObjectInfo interactiveObjectInfo = new InteractiveObjectInfo()
            {
                objectName = gameObject.name,
                objectActivationState = gameObject.activeSelf,
                colliderActivationState = gameObject.GetComponent<Collider>().enabled
            };
            interactiveObjectInfoList.Add(interactiveObjectInfo);
        }
        // Se guarda la información de cada objeto no interactivo en la lista temporal correspondiente
        nonInteractiveObjectsArray = GameObject.FindGameObjectsWithTag("NonInteractiveObject");
        foreach (GameObject gameObject in nonInteractiveObjectsArray)
        {
            NonInteractiveObjectInfo nonInteractiveObjectInfo = new NonInteractiveObjectInfo()
            {
                objectName = gameObject.name,
                objectActivationState = gameObject.activeSelf
            };
            nonInteractiveObjectInfoList.Add(nonInteractiveObjectInfo);
        }
        // Se guarda el resto de variables
        audioController = GameObject.Find("AudioController");
        audioSource = audioController.GetComponent<AudioSource>();
        // Una vez se ha recabado la información necesaria, se guarda la información de la escena en un objeto SceneInfo
        currentSceneInfo = new SceneInfo()
        {
            sceneName = SceneManager.GetActiveScene().name,
            interactiveObjects = interactiveObjectInfoList,
            nonInteractiveObjects = nonInteractiveObjectInfoList,
            bgmName = audioSource.clip.name
        };
        // Se guarda el objeto SceneInfo en la lista de información de escenas
        SaveSceneInfo();
    }
    // 2. Se añade la lista de información a la lista de información de escenas
    public void SaveSceneInfo()
    {
        // Primero debemos comprobar si la escena ya está guardada. En caso de que ya lo esté, se guarda información de escena en el lugar que le corresponda en la lista
        if (scenesInfoList.Count != 0)
        {
            if (scenesInfoList.Exists(savedSceneInfo => savedSceneInfo.sceneName == currentSceneInfo.sceneName) == true)
            {
                int sceneInfoIndex = scenesInfoList.FindIndex(savedSceneInfo => savedSceneInfo.sceneName == currentSceneInfo.sceneName);
                scenesInfoList[sceneInfoIndex] = currentSceneInfo;
            }
            // En caso contrario, se guarda la información directamente
            else
            {
                scenesInfoList.Add(currentSceneInfo);
            }
        }
        else
        {
            scenesInfoList.Add(currentSceneInfo);
        }
    }
    public void SerializeState(GameStateMap gameStateMap)
    {
        savedState = new GameState()
        {
            savedSceneName = SceneManager.GetActiveScene().name, 
            savedScenesInfo = scenesInfoList
        };
        gameStateMap.SetState(savedState);
    }
    public UniTask DeserializeState(GameStateMap gameStateMap)
    {
        loadedState = gameStateMap.GetState<GameState>();
        // Si no hay estado de guardado, no se carga nada
        if (savedState == null)
        {
            return UniTask.CompletedTask;
        }
        scenesInfoList = loadedState.savedScenesInfo;
        lastSceneName = loadedState.savedSceneName;
        // Se busca el SceneInfo que corresponde a la escena actual
        int sceneInfoIndex = scenesInfoList.FindIndex(savedSceneInfo => savedSceneInfo.sceneName == lastSceneName);
        currentSceneInfo = scenesInfoList[sceneInfoIndex];
        DirectorScript.directorSingleton.variableManager.SetVariableValue("savedScene", lastSceneName);
        DirectorScript.directorSingleton.variableManager.SetVariableValue("savedBGM", currentSceneInfo.bgmName);
        DirectorScript.directorSingleton.scriptPlayer.PreloadAndPlayAsync(loadScript.Name);
        return UniTask.CompletedTask;
    }
    // La carga de los objetos activables se hace desde el script porque primero debe cargarse la escena para que se puedan modificar, de ahí que LoadObjects se carga desde LoadScript
    public void LoadObjects()
    {
        interactiveObjectsArray = GameObject.FindGameObjectsWithTag("InteractiveObject");
        foreach (GameObject interactiveObject in interactiveObjectsArray)
        {
            int objectIndex = currentSceneInfo.interactiveObjects.FindIndex(savedObject => interactiveObject.name == savedObject.objectName);
            InteractiveObjectInfo savedInteractiveObjectInfo = currentSceneInfo.interactiveObjects[objectIndex];
            interactiveObject.SetActive(savedInteractiveObjectInfo.objectActivationState);
        }
        nonInteractiveObjectsArray = GameObject.FindGameObjectsWithTag("NonInteractiveObject");
        foreach (GameObject nonInteractiveObject in nonInteractiveObjectsArray)
        {
            int objectIndex = currentSceneInfo.nonInteractiveObjects.FindIndex(savedObject => nonInteractiveObject.name == savedObject.objectName);
            NonInteractiveObjectInfo savedNonInteractiveObjectInfo = currentSceneInfo.nonInteractiveObjects[objectIndex];
            nonInteractiveObject.SetActive(savedNonInteractiveObjectInfo.objectActivationState);
        }
    }
}
