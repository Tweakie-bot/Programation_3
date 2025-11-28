using Programation_3_DnD.Manager;
using Programation_3_DnD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Objects;
using Spectre.Console;
using Programation_3_DnD.Composants;
using Spectre.Console.Rendering;

namespace Programation_3_DnD.State
{
    public class InGameState : IState
    {
        private GameEngine _gameEngine;
        private IOutput _renderer;
        GameObject _player;
        GameStateMachine _gameStateMachine;
        private float _totalTime;

        //
        public InGameState(GameEngine game_engine, GameStateMachine state_machine, IOutput renderer, GameObject player)
        {
            _gameEngine = game_engine;
            _renderer = renderer;
            _gameStateMachine = state_machine;
            _player = player;
        }

        //
        public GameObject GetPlayer() { return _player; }
        public GameEngine GetGameEngine() { return _gameEngine; }
        public float GetTime() { return _totalTime; }

        //
        public void Enter() { }
        public void Exit() { }
        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.P) 
            {
                _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(PauseMenuState)));
            }
        }
        public void Update() { }
        public void FixedUpdate(float d_t)
        {
            _totalTime = d_t;
        }
        public void Render()
        {
            _renderer.RenderInGameState(this);
        }

    }
}
