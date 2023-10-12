using UnityEngine;
using Zenject;

public class InstanceInstaller : MonoInstaller
{
    [SerializeField] private DialogueDatabase database;

    public override void InstallBindings()
    {
        Container.Bind<DialogueDatabase>().FromScriptableObject(database).AsSingle();
    }
}