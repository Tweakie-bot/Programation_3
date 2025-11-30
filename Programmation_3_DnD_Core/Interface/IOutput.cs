
namespace Programation_3_DnD_Core
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
