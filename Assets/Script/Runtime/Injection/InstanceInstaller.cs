using UnityEngine;
using Zenject;

public class InstanceInstaller : MonoInstaller
{
    [SerializeField] private DialogDatabase database;
    [SerializeField] private DialogManager dialogueManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private BaliseGetter baliseGetter;
    [SerializeField] private EndingManager endingManager;

    public override void InstallBindings()
    {
        Container.Bind<DialogDatabase>().FromScriptableObject(database).AsSingle();
        Container.Bind<DialogManager>().FromInstance(dialogueManager).AsSingle();
        Container.Bind<BaliseGetter>().FromInstance(baliseGetter).AsSingle();
        Container.Bind<EndingManager>().FromInstance(endingManager).AsSingle();
        Container.Bind<IInputManager>().To<InputManager>().FromInstance(inputManager).AsSingle();
    }
}