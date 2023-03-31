using Naninovel;
using Naninovel.Commands;
using UniRx.Async;
using UnityEngine;
[CommandAlias("fadeIn")]
public class FadeIn : Command
{
    public StringParameter objectName;
    Animator screenAnimator;
    public override UniTask ExecuteAsync(CancellationToken cancellationToken = default)
    {
        screenAnimator = GameObject.Find(objectName).GetComponent<Animator>();
        screenAnimator.Play("FadeIn");
        return UniTask.CompletedTask;
    }
}