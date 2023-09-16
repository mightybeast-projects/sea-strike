using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets.BattleGrid;

public class PlayerBattleGridPanel : BattleGridPanel
{
    protected override Board playerBoard => player.board;

    public PlayerBattleGridPanel(SeaStrikePlayer player) : base(player)
    {
        gridLabel = SeaStrikeGame.stringStorage.playerOceanGridLabel;
        showShips = true;

        Initialize();
    }
}