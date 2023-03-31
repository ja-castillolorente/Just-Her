using UnityEngine;
using System.Collections.Generic;
using Naninovel;
using Naninovel.Commands;
using UniRx.Async;
using UnityEngine.SceneManagement;
[CommandAlias("enableObject")]
public class EnableObject : Command
{
    public StringParameter objectName;
    GameObject gameObject;
    //Renderer parentRenderer;
    //Renderer[] childRenderers;
    //List<Renderer> renderersList;
    SaveManager saveManager;
    public override UniTask ExecuteAsync(CancellationToken cancellationToken = default)
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        int currentSceneInfoIndex = saveManager.scenesInfoList.FindIndex(savedSceneInfo => savedSceneInfo.sceneName == SceneManager.GetActiveScene().name);
        SceneInfo currentSceneInfo = saveManager.scenesInfoList[currentSceneInfoIndex];
        gameObject = GameObject.Find(objectName);
        gameObject.SetActive(true);
        //renderersList = new List<Renderer>();
        //if (gameObject.GetComponent<Renderer>() == true)
        //{
        //    parentRenderer = gameObject.GetComponent<Renderer>();
        //    renderersList.Add(parentRenderer);
        //}
        //if (gameObject.transform.childCount > 0)
        //{
        //    childRenderers = gameObject.GetComponentsInChildren<Renderer>();
        //    foreach (Renderer renderer in childRenderers)
        //    {
        //        renderersList.Add(renderer);
        //    }
        //}
        //if (renderersList.Count > 0)
        //{
        //    foreach (Renderer renderer in renderersList)
        //    {
        //        renderer.enabled = true;
        //    }
        //}
        string objectTag = gameObject.tag;
        if (objectTag == "InteractiveObject")
        {
            List<InteractiveObjectInfo> currentObjectInfoList = currentSceneInfo.interactiveObjects;
            int objectIndex = currentObjectInfoList.FindIndex(savedObjectInfo => savedObjectInfo.objectName == gameObject.name);
            InteractiveObjectInfo currentObjectInfo = currentObjectInfoList[objectIndex];
            currentObjectInfo.objectActivationState = gameObject.activeSelf;
        }
        else if (objectTag == "NonInteractiveObject")
        {
            List<NonInteractiveObjectInfo> currentObjectInfoList = currentSceneInfo.nonInteractiveObjects;
            int objectIndex = currentObjectInfoList.FindIndex(savedObjectInfo => savedObjectInfo.objectName == gameObject.name);
            NonInteractiveObjectInfo currentObjectInfo = currentObjectInfoList[objectIndex];
            currentObjectInfo.objectActivationState = gameObject.activeSelf;
        }
        return UniTask.CompletedTask;
    }
}