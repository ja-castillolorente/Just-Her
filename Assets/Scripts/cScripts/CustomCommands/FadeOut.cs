using Naninovel;
using Naninovel.Commands;
using UniRx.Async;
using UnityEngine;
[CommandAlias("fadeOut")]
public class FadeOut : Command
{
    public StringParameter objectName;
    Animator screenAnimator;
    public override UniTask ExecuteAsync(CancellationToken cancellationToken = default)
    {
        screenAnimator = GameObject.Find(objectName).GetComponent<Animator>();
        screenAnimator.Play("FadeOut");
        return UniTask.CompletedTask;
    }
}
