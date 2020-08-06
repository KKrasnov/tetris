using System;
using System.Collections.Generic;
using UnityEngine;
using Dependencies;
using Game.State;
using Tetris.Gameplay;
using Tetris.Gameplay.Board;
using Tetris.Gameplay.Bricks;
using Tetris.Gameplay.GameLoop;
using Tetris.Gameplay.GameLoop.Components;
using Tetris.InputManagement;
using Tetris.InputManagement.Components;
using Tetris.LevelManagement;
using Tetris.UI;
using Tetris.UI.Game;
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
        
        //View prefabs
        [SerializeField] private GameObject _mainMenuViewPrefab;
        [SerializeField] private GameObject _gameViewPrefab;

        //Prefabs for factories
        [SerializeField] private GameObject _brickPrefab;
        [SerializeField] private GameObject _blockPrefab;

        //dependencies which were easier to create as MonoBehaviours
        [SerializeField] private GameLoopManager _gameLoopManager;
        [SerializeField] private InputManager _inputManager;
        
        //Levels configuration
        [SerializeField] private LevelData[] _levels;

        //explicit binding looks pretty complex and fragile since I didn't use any of packages.
        public void ConfigureDependencies(DependencyResolver dependencies)
        {
            //configure UI manager windows mapping
            var uiManager = new UIManager(_uiContainer, new Dictionary<string, UIWindowConfiguration>()
            {
                {
                    UIWindow.STRING_MAIN_MENU_WINDOW_ID, new UIWindowConfiguration(
                        () => new MainMenuWindowController(),
                        () => new MainMenuWindowModel(
                            dependencies.Resolve<IGameStateManager>(), 
                            dependencies.Resolve<LevelManager>()),
                        _mainMenuViewPrefab
                    )
                },
                {
                    UIWindow.STRING_GAME_WINDOW_ID, new UIWindowConfiguration(
                        () => new GameWindowController(), 
                        () => new GameWindowModel(dependencies.Resolve<IInputManager>()), 
                        _gameViewPrefab
                    )
                }
            });
            
            var boardManager = new BoardManager();
            var blockFactory = new BlockFactory(_blockPrefab);
            var brickFactory = new BrickFactory(_brickPrefab, blockFactory);
            var gameManager = new GameManager(brickFactory, boardManager, ()=>
                dependencies.Resolve<IGameStateManager>().SetState(GameStateType.MainMenu));//horrible solution but there no time left
            LevelManager levelManager = new LevelManager(_levels, 
                new CompositeObserver<LevelData>(brickFactory, _gameLoopManager));
            
            //configure MonoBehaviour services
            _gameLoopManager.Init(new CompositeObserver<TickPassedParams>(gameManager));
            _inputManager.Init(new CompositeObserver<InputReceivedParams>(gameManager, _gameLoopManager));
            
            //configure application state machine
            var gameStateManager = new GameStateManager(new[]
            {
                new Tuple<GameStateType, Func<GameState>>(GameStateType.MainMenu,
                    () => new GameState(() =>
                    {
                        dependencies.Resolve<IUIManager>().OpenWindow(UIWindow.STRING_MAIN_MENU_WINDOW_ID);
                    }, () =>
                    {
                        dependencies.Resolve<IUIManager>().CloseWindow(UIWindow.STRING_MAIN_MENU_WINDOW_ID);
                    })),
                new Tuple<GameStateType, Func<GameState>>(GameStateType.Game,
                    () => new GameState(() =>
                    {
                        dependencies.Resolve<IUIManager>().OpenWindow(UIWindow.STRING_GAME_WINDOW_ID);
                    }, () =>
                    {
                        dependencies.Resolve<IUIManager>().CloseWindow(UIWindow.STRING_GAME_WINDOW_ID);
                    })),
            }, new CompositeObserver<GameStateType>(_gameLoopManager, gameManager));

            //bind dependencies in order to retrieve them later in runtime
            dependencies.Bind<IGameStateManager>(gameStateManager);
            dependencies.Bind<IUIManager>(uiManager);
            dependencies.Bind(levelManager);
            dependencies.Bind<IInputManager>(_inputManager);
        }
    }
}