using Programation_3_DnD.Composants;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Event
{
    public class WorkEvent : Event
    {
        //
        private GameEngine _gameEngine;
        private GameObject _playerGameObject;
        private int _moneyGained;
        private IOutput _renderer;
        private EventManager _eventManager;

        //
        public WorkEvent(GameEngine game_engine, GameObject player, int money, IOutput renderer, EventManager event_manager)
        {
            _gameEngine = game_engine;
            _playerGameObject = player;
            _moneyGained = money;
            _renderer = renderer;
            _eventManager = event_manager;
        }

        //
        public override void Update()
        {
            _gameEngine.Work();

            InventoryComposant inventory = _playerGameObject.GetComposant<InventoryComposant>();
            WorkForceComposant force = _playerGameObject.GetComposant<WorkForceComposant>();

            if (inventory == null || force == null)
            {
                _renderer.WriteLine("WorkEvent failed: missing component.");
                _isCompleted = true;
                return;
            }

            int new_force = force.ApplyWorkForceGain(5);
            int total_gain = _moneyGained + new_force;

            inventory.AddByName("Gold", total_gain);

            //_eventManager.RegisterEvent(new RenderEvent(_renderer, $"You gained {totalGain} gold (WorkForce: {newForce})"));
            _gameEngine.PushUIMessage($"You gained {total_gain} gold (WorkForce: {new_force})");

            _isCompleted = true;
        }
    }
}
