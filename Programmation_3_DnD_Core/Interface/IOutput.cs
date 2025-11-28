using Programation_3_DnD.State;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programation_3_DnD.Composants;

namespace Programation_3_DnD.Interface
{
    public interface IOutput
    {
        public void WriteLine(string text);
        public void BeginFrame();
        public void EndFrame();
        public void PassLine();
        public void Clear();
        public void SetInventory(Composant composant);
        public void SetLocation(LocationComposant location);
        void RenderTradingState(TradingState state);
        void RenderBuyingState(BuyingState state);
        void RenderSellingState(SellingState state);
        void RenderInGameState(InGameState state);
        void RenderInventoryState(InventoryState state);
        void RenderMainMenuState(MainMenuState state);
        void RenderPauseMenuState(PauseMenuState state);
    }
}
