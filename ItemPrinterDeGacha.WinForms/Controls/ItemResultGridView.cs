using ItemPrinterDeGacha.Core;
using PKHeX.Core;

namespace ItemPrinterDeGacha.WinForms.Controls;

public partial class ItemResultGridView : UserControl
{
    public ItemResultGridView() => InitializeComponent();

    public PrintMode Print(ulong ticks, PrintMode mode, int items)
    {
        if (items is (<1 ) or > 10)
            throw new ArgumentOutOfRangeException(nameof(items), "Items must be between 1 and 10.");

        Span<Item> itemSpan = stackalloc Item[items];
        var finalMode = ItemPrinter.Print(ticks, itemSpan, mode);

        Populate(itemSpan);
        return finalMode;
    }

    private void Populate(ReadOnlySpan<Item> itemSpan)
    {
        var names = GameInfo.Strings.Item;
        var rows = DGV_View.Rows;
        rows.Clear();
        foreach (var item in itemSpan)
        {
            var img = PKHeX.Drawing.PokeSprite.Properties.Resources.ResourceManager
                .GetObject($"aitem_{item.ItemId}");
            rows.Add(item.Count, img, names[item.ItemId]);
        }
    }
}
