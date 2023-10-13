using UnityEngine;
using Zenject;

public class InstanceInstaller : MonoInstaller
{
    [SerializeField] private DialogueDatabase database;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private InputManager inputManager;

    public override void InstallBindings()
    {
        Container.Bind<DialogueDatabase>().FromScriptableObject(database).AsSingle();
        Container.Bind<DialogueManager>().FromInstance(dialogueManager).AsSingle();
        Container.Bind<IInputManager>().To<InputManager>().FromInstance(inputManager).AsSingle();
    }
}