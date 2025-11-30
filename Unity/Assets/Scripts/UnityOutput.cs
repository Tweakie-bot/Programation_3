using Programation_3_DnD_Core;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class UnityOutput : MonoBehaviour, IOutput
{
    public UnityEngine.GameObject _mainMenuCanvas;
    public UnityEngine.GameObject _inGameCanvas;
    public UnityEngine.GameObject _pauseMenuCanvas;

    public UnityEngine.GameObject _inventoryCanvas;

    public UnityEngine.GameObject _tradingCanvas;

    public UnityEngine.GameObject _sellingCanvas;

    public UnityEngine.GameObject _buyingCanvas;

    public TextMeshProUGUI _inventoryText;

    public TextMeshProUGUI _timeText;

    public TextMeshProUGUI _consoleText;

    public TextMeshProUGUI _currentLocationText;
    public TextMeshProUGUI _destinationsText;

    public TextMeshProUGUI _tradingText;

    public TextMeshProUGUI _sellingText;

    public TextMeshProUGUI _buyingText;

    private string _currentFrameText = "";

    public UnityGameRunner _gameRunner;

    private LocationComposant _currentLocationComposant;
    private LocationComposant _previousLocationComposant;
    private List<LocationComposant> _locationListComposant;

    private InventoryComposant _inventoryComposant;

    private InventoryComposant _merchantInventoryComposant;

    void Start()
    {

    }

    void Update()
    {
        switch (_gameRunner.GetCurrentState())
        {
            case MainMenuState :

                _mainMenuCanvas.SetActive(true);

                _inGameCanvas.SetActive(false);

                _inventoryCanvas.SetActive(false);

                _pauseMenuCanvas.SetActive(false);

                _tradingCanvas.SetActive(false);

                _sellingCanvas.SetActive(false);

                _buyingCanvas.SetActive(false);

                break;

            case InGameState :

                _inGameCanvas.SetActive(true);

                _mainMenuCanvas.SetActive(false);

                _inventoryCanvas.SetActive(false);

                _pauseMenuCanvas.SetActive(false);

                _tradingCanvas.SetActive(false);

                _sellingCanvas.SetActive(false);

                _buyingCanvas.SetActive(false);

                break;

            case TradingState :

                _tradingCanvas.SetActive(true);

                _inGameCanvas.SetActive(false);

                _inventoryCanvas.SetActive(false);

                _pauseMenuCanvas.SetActive(false);

                _mainMenuCanvas.SetActive(false);

                _sellingCanvas.SetActive(false);

                _buyingCanvas.SetActive(false);

                break;
            case SellingState :

                _sellingCanvas.SetActive(true);

                _buyingCanvas.SetActive(false);

                _tradingCanvas.SetActive(false);

                _inGameCanvas.SetActive(false);

                _inventoryCanvas.SetActive(false);

                _pauseMenuCanvas.SetActive(false);

                _mainMenuCanvas.SetActive(false);

                break;

            case BuyingState :

                _buyingCanvas.SetActive(true);   // 👈 AJOUTER TON CANVAS BUYING

                _sellingCanvas.SetActive(false);

                _tradingCanvas.SetActive(false);

                _inGameCanvas.SetActive(false);

                _inventoryCanvas.SetActive(false);

                _pauseMenuCanvas.SetActive(false);

                _mainMenuCanvas.SetActive(false);

                break;

            case PauseMenuState :

                _pauseMenuCanvas.SetActive(true);

                _mainMenuCanvas.SetActive(false);

                _inGameCanvas.SetActive(false);

                _inventoryCanvas.SetActive(false);

                _tradingCanvas.SetActive(false);

                _sellingCanvas.SetActive(false);

                _buyingCanvas.SetActive(false);

                break;

            case InventoryState :

                _inventoryCanvas.SetActive(true);

                _mainMenuCanvas.SetActive(false);

                _inGameCanvas.SetActive(false);

                _pauseMenuCanvas.SetActive(false);

                _tradingCanvas.SetActive(false);

                _sellingCanvas.SetActive(false);

                _buyingCanvas.SetActive(false);

                break;
        }
    }

    public void WriteLine(string message)
    {
        _currentFrameText += message + "\n";
    }

    public void PassLine()
    {
        _currentFrameText += "\n";
    }

    public void Clear()
    {
        // Ne rien faire, Render va écraser le texte.
    }

    public void BeginFrame()
    {
        // On reset juste le texte de la frame.
        _currentFrameText = "";
    }

    public void EndFrame()
    {
        // On pousse le texte dans le TMP.
        _consoleText.text = _currentFrameText;
    }

    public void SetInventory(Composant composant) 
    {
        _inventoryComposant = composant as InventoryComposant;
    }

    public void SetMerchantTrading(InventoryComposant composant)
    {
        _merchantInventoryComposant = composant;
    }
    public void SetLocation(LocationComposant location) { }

    public void RenderTradingState(TradingState state)
    {
        int selected = state.GetSelected();

        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"=== Trading with merchant ===");
        sb.AppendLine("↑ ↓ pour naviguer | ENTER pour sélectionner\n");

        string[] options = { "Buy", "Sell", "Quit" };

        for (int i = 0; i < options.Length; i++)
        {
            string cursor = (i == selected) ? "> " : "  ";
            sb.AppendLine($"{cursor}{options[i]}");
        }

        _tradingText.text = sb.ToString();
    }


    public void RenderBuyingState(BuyingState state)
    {
        var sb = new StringBuilder();

        sb.AppendLine("=== BUY ===");
        sb.AppendLine("↑ ↓ pour naviguer | ENTER pour acheter | ESC pour revenir\n");

        int count = _merchantInventoryComposant.GetItemCount();

        if (count == 0)
        {
            sb.AppendLine("Le marchand n'a rien à vendre.");
            _buyingText.text = sb.ToString();
            return;
        }

        for (int i = 0; i < count; i++)
        {
            var item = _merchantInventoryComposant.GetItemByIndex(i);
            int qty = _merchantInventoryComposant.GetCountByIndex(i);

            string cursor = (i == state.GetSelected()) ? "> " : "  ";

            sb.AppendLine($"{cursor}{item.GetName()}  x{qty}  | Prix : {item.GetPrice()}g");
        }

        _buyingText.text = sb.ToString();
    }

    public void RenderSellingState(SellingState state)
    {
        var sb = new StringBuilder();

        sb.AppendLine("=== SELL ===");
        sb.AppendLine("↑ ↓ pour naviguer | ENTER pour vendre | ESC pour revenir\n");

        int count = _inventoryComposant.GetItemCount();

        if (count == 0)
        {
            sb.AppendLine("Vous n'avez rien à vendre.");
            _sellingText.text = sb.ToString();
            return;
        }

        for (int i = 0; i < count; i++)
        {
            var item = _inventoryComposant.GetItemByIndex(i);
            int qty = _inventoryComposant.GetCountByIndex(i);

            string cursor = (i == state.GetSelected()) ? "> " : "  ";

            sb.AppendLine($"{cursor}{item.GetName()}  x{qty}  | Valeur : {item.GetPrice()}g");
        }

        _sellingText.text = sb.ToString();
    }

    public void RenderInGameState(InGameState state)
    {
        _timeText.text = $"TIME {_gameRunner.GetTime()}";
        // ---- PARTIE 1 : LOCATION ACTUELLE ----
        string name = _currentLocationComposant.GetName();
        string desc = _currentLocationComposant.GetDescription();

        _currentLocationText.text = $"{name}\n\n{desc}";

        // ---- PARTIE 2 : DESTINATIONS ----
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < _currentLocationComposant.GetCount(); i++)
        {
            LocationComposant next = _currentLocationComposant.GetLocationAtIndex(i);
            sb.AppendLine($"{i + 1} - {next.GetName()}");
        }

        // Ajoute la possibilité de revenir en arrière
        if (_previousLocationComposant != null)
        {
            sb.AppendLine("ESC - Revenir");
        }

        bool merchant_found = false;

        Programation_3_DnD_Core.GameObject merchant = null;

        foreach (Programation_3_DnD_Core.GameObject character in _currentLocationComposant.GetCopyOfCharacterTable())
        {
            if (character.GetComposant<InventoryComposant>() != null)
            {
                merchant_found = true;
                merchant = character;
                break;
            }
            merchant_found = false;
        }
        if (merchant_found)
        {
            float time = _gameRunner.GetTime();

            if (time >= 7f && time < 19f)
            {
                sb.AppendLine($"T - Trade with {merchant.GetComposant<IDComposant>().GetName()}");
                sb.AppendLine($"W - Work with {merchant.GetComposant<IDComposant>().GetName()}");
            }
            else
            {
                sb.AppendLine("Revenez plus tard");
            }
        }

        _destinationsText.text = sb.ToString();
    }

    public void RenderInventoryState(InventoryState state)
    {
        if (_inventoryComposant == null)
        {
            _inventoryText.text = "Inventaire vide.";
            return;
        }

        StringBuilder sb = new StringBuilder();

        int count = _inventoryComposant.GetItemCount();

        for (int i = 0; i < count; i++)
        {
            var item = _inventoryComposant.GetItemByIndex(i);
            int qty = _inventoryComposant.GetCountByIndex(i);

            sb.AppendLine($"{item.GetName()} x{qty}");
        }

        _inventoryText.text = sb.ToString();
    }

    public void RenderMainMenuState(MainMenuState state) { }

    public void RenderPauseMenuState(PauseMenuState state) { }

    public void SetListOfLocations(List<LocationComposant> list)
    {
        _locationListComposant = list;
    }

    public void SetPreviousLocation(LocationComposant location)
    {
        _previousLocationComposant = location;
    }

    public void SetCurrentLocation(LocationComposant location)
    {
        _currentLocationComposant = location;
    }
}
