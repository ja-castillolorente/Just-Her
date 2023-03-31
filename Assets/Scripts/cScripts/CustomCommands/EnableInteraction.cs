using Naninovel;
using Naninovel.Commands;
using UniRx.Async;
using UnityEngine;
[CommandAlias("enableInteraction")]
public class EnableInteraction : Command
{
    public override UniTask ExecuteAsync(CancellationToken cancellationToken = default)
    {
        DirectorScript.directorSingleton.EnableObjectInteraction();
        return UniTask.CompletedTask;
    }
}