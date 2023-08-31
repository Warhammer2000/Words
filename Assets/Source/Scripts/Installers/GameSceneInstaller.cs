using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private WordLists wordLists;
    [SerializeField] private LetterButtonGenerator letterButtonGenerator;
    public override void InstallBindings()
    {
        Container.Bind<WordLists>().FromInstance(wordLists).AsSingle();
        Container.Bind<LetterButtonGenerator>().FromInstance(letterButtonGenerator).AsSingle();
    }
}