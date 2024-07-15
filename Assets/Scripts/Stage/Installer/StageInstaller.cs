using UnityEngine;
using Zenject;

public class StageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<GameStateManager>()
            .AsCached();
    }
}