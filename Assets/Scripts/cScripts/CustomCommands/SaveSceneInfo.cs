using Naninovel;
using Naninovel.Commands;
using UniRx.Async;
using UnityEngine;
[CommandAlias("saveSceneInfo")]
public class SaveSceneInfo : Command
{
    SaveManager saveManager;
    public override UniTask ExecuteAsync(CancellationToken cancellationToken = default)
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        saveManager.CreateSceneInfo();
        return UniTask.CompletedTask;
    }
}
