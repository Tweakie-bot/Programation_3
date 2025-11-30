using Programation_3_DnD_Core;

namespace Programation_3_DnD_Console
{
    public class OutputManagerForTests : IOutput
    {
        public void WriteLine(string message) { }

        public void PassLine() { }

        public void Clear() { }

        public void BeginFrame() { }

        public void EndFrame() { }
        public void SetInventory(Composant composant) { }

        public void SetLocation(LocationComposant composant) { }
        public void RenderTradingState(TradingState state) { }

        public void RenderBuyingState(BuyingState state) { }

        public void RenderSellingState(SellingState state) { }

        public void RenderInGameState(InGameState state) { }

        public void RenderInventoryState(InventoryState state) { }

        public void RenderMainMenuState(MainMenuState state) { }

        public void RenderPauseMenuState(PauseMenuState state) { }
    }
}
