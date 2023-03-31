using Naninovel;
using Naninovel.Commands;
using UniRx.Async;
using UnityEngine;
[CommandAlias("loadObjects")]
public class LoadObjects : Command
{
    SaveManager saveManager;
    public override UniTask ExecuteAsync(CancellationToken cancellationToken = default)
    {
        saveManager = Object.FindObjectOfType<SaveManager>();
        saveManager.LoadObjects();
        return UniTask.CompletedTask;
    }
}
