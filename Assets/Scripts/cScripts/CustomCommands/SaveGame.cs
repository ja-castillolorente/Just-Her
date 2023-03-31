using Naninovel;
using Naninovel.Commands;
using UniRx.Async;
using UnityEngine;
[CommandAlias("save")]
public class SaveGame : Command
{
    public override UniTask ExecuteAsync(CancellationToken cancellationToken = default)
    {
        DirectorScript.directorSingleton.stateManager.QuickSaveAsync();
        return UniTask.CompletedTask;
    }
}
