using System.Linq;
using Myra.Graphics2D.UI;
using SeaStrike.GameCore.Root.Network;

namespace SeaStrike.GameCore.Root.Widgets.BattleGrid.Multiplayer;

public class NetPlayerBattleGridPanel : PlayerBattleGridPanel
{
    private new NetPlayer player;

    public NetPlayerBattleGridPanel(NetPlayer player) : base(player)
    {
        this.player = player;

        Widgets.Add(CurrentPlayerTurnLabel);
    }

    public void Update() => ((CurrentPlayerTurnLabel)Widgets.Last()).Update();

    private Label CurrentPlayerTurnLabel => new CurrentPlayerTurnLabel(player)
    {
        GridRow = 1,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Bottom
    };
}