using System;
using System.Collections.Generic;
using UnityEngine;
using Dependencies;
using Game.State;
using Tetris.LevelManagement;
using Tetris.UI;
using Tetris.UI.MainMenu;
using Tools;
using UI.MVC;

namespace Tetris.Dependencies
{
    /// <summary>
    /// Primitive composition root, didn't bother with loading configuration/UI context objects and so on.
    /// </summary>
    public class TetrisDependencyBinder : MonoBehaviour, IDependencyBinder
    {
        [SerializeField] private Canvas _uiContainer;
        
        [SerializeField] private GameObject _mainMenuViewPrefab;
        [SerializeField] private GameObject _gameViewPrefab;

        [SerializeField] private LevelData[] _levels;

        public void ConfigureDependencies(DependencyResolver dependencies)
        {
            //configure state machine
            GameStateManager gameStateManager = new GameStateManager(new[]
            {
                new Tuple<GameStateType, Func<GameState>>(GameStateType.MainMenu,
                    () => new GameState(() =>
                    {
                        dependencies.Resolve<UIManager>().OpenWindow(UIWindow.STRING_MAIN_MENU_WINDOW_ID);
                    }, () =>
                    {
                        dependencies.Resolve<UIManager>().CloseWindow(UIWindow.STRING_MAIN_MENU_WINDOW_ID);
                    })),
                new Tuple<GameStateType, Func<GameState>>(GameStateType.Game,
                    () => new GameState(() =>
                    {
                        dependencies.Resolve<UIManager>().OpenWindow(UIWindow.STRING_GAME_WINDOW_ID);
                    }, () =>
                    {
                        dependencies.Resolve<UIManager>().CloseWindow(UIWindow.STRING_GAME_WINDOW_ID);
                    })),
            }, new CompositeObserver<GameStateType>());
            
            LevelManager levelManager = new LevelManager(_levels, new CompositeObserver<LevelData>());

            UIManager uiManager = new UIManager(_uiContainer, new Dictionary<string, UIWindowConfiguration>()
            {
                {
                    "MainMenu", new UIWindowConfiguration(
                        () => new MainMenuWindowController(),
                        () => new MainMenuWindowModel(
                            dependencies.Resolve<GameStateManager>(), 
                            dependencies.Resolve<LevelManager>()),
                        _mainMenuViewPrefab
                    )
                },
                {
                    "Game", new UIWindowConfiguration(
                        () => null,
                        () => null,
                        _gameViewPrefab
                    )
                }
            });
            
            dependencies.Bind(gameStateManager);
            dependencies.Bind(uiManager);
            dependencies.Bind(levelManager);
        }
    }
}