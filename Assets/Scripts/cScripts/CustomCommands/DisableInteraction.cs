using Naninovel;
using Naninovel.Commands;
using UniRx.Async;
using UnityEngine;
[CommandAlias("disableInteraction")]
public class DisableInteraction : Command
{
    public override UniTask ExecuteAsync (CancellationToken cancellationToken = default)
    {
        DirectorScript.directorSingleton.DisableObjectInteraction();
        return UniTask.CompletedTask;
    }
}
