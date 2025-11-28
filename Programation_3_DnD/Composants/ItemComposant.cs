using Programation_3_DnD.Composants;
using Programation_3_DnD.Data;

public class ItemComposant : Composant
{
    // Variables
    private string _name;
    private int _valueInGold;

    // Constructeurs
    public ItemComposant(string name, int value)
    {
        _name = name;
        _valueInGold = value;
    }
    public ItemComposant(ItemData item)
    {
        _name = item.GetName();
        _valueInGold = item.GetValueInGold();
    }

    // Getters
    public string GetName()
    {
        return _name;
    }
    public int GetPrice()
    {
        return _valueInGold;
    }

    // Logique
    public override void ProcessInput(ConsoleKey key) { }
    public override void Update() { }
    public override void FixedUpdate(float t) { }
    public override void Render() { }
}
