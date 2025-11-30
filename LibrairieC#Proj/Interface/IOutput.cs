
using System.Collections.Generic;

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
        public void RenderTradingState(TradingState state);
        public void RenderBuyingState(BuyingState state);
        public void RenderSellingState(SellingState state);
        public void RenderInGameState(InGameState state);
        public void RenderInventoryState(InventoryState state);
        public void RenderMainMenuState(MainMenuState state);
        public void RenderPauseMenuState(PauseMenuState state);
        public void SetListOfLocations(List<LocationComposant> list);
        public void SetPreviousLocation(LocationComposant location);
        public void SetCurrentLocation(LocationComposant location);
        public void SetMerchantTrading(InventoryComposant? merchant);
    }
}
