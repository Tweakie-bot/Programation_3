
using System;

namespace Programation_3_DnD_Core
{
    public class TradingState : IState
    {
        private GameStateMachine _machine;
        private IOutput _renderer;
        private GameObject _player;
        private GameObject _merchant;
        private InventoryComposant _playerInv;
        private InventoryComposant _merchantInv;

        private int _selected;

        //
        public TradingState(GameStateMachine machine, IOutput renderer, GameObject player, GameObject merchant)
        {
            _machine = machine;
            _renderer = renderer;
            _player = player;
            _merchant = merchant;

            _playerInv = _player.GetComposant<InventoryComposant>();
            _merchantInv = _merchant.GetComposant<InventoryComposant>();

            _selected = 0;
        }

        //
        public GameObject GetPlayer()
        {
            return _player;
        }
        public GameObject GetMerchant()
        {
            return _merchant;
        }
        public int GetSelected()
        {
            return _selected;
        }

        //
        public void Enter() { }

        public void Exit() { }

        //
        public void TreatInput(IInput input_manager)
        {
            if (input_manager.IsKeyUp())
            {
                _selected--;
                if (_selected < 0) _selected = 2;
            }
            else if (input_manager.IsKeyDown())
            {
                _selected++;
                if (_selected > 2) _selected = 0;
            }
            else if (input_manager.IsKeyValidate())
            {
                if (_selected == 0)
                {
                    _machine.SetState(
                        new BuyingState(_machine, _renderer, _player, _merchant)
                    );
                }
                else if (_selected == 1)
                {
                    _machine.SetState(
                        new SellingState(_machine, _renderer, _player, _merchant)
                    );
                }
                else if (_selected == 2)
                {
                    _machine.SetState(_machine.GetState(typeof(InGameState)));
                }
            }
        }
        public void Update() { }
        public void FixedUpdate(float delta) { }
        public void Render()
        {
            _renderer.RenderTradingState(this);
        }
    }
}
