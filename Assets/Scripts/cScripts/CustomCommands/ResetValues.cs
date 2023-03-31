using Naninovel;
using Naninovel.Commands;
using UniRx.Async;
using UnityEngine;
using System.Collections.Generic;
[CommandAlias("resetValues")]
public class ResetValues : Command
{
    IReadOnlyCollection<CustomVariable> variablesList;
    public override UniTask ExecuteAsync(CancellationToken cancellationToken = default)
    {
        variablesList = DirectorScript.directorSingleton.variableManager.GetAllVariables();
        foreach (CustomVariable customVariable in variablesList)
        {
            DirectorScript.directorSingleton.variableManager.SetVariableValue(customVariable.Name, "false");
        }
        // Algunas variables no tiene el valor "false por defecto"
        DirectorScript.directorSingleton.variableManager.SetVariableValue("Basement_clickedObjects", "6");
        return UniTask.CompletedTask;
    }
}

