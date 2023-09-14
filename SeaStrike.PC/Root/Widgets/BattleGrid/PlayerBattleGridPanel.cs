using SeaStrike.Core.Entity;

namespace SeaStrike.PC.Root.Widgets.BattleGrid;

public class PlayerBattleGridPanel : BattleGridPanel
{
    public PlayerBattleGridPanel(Board playerBoard)
    {
        this.playerBoard = playerBoard;

        gridLabel = SeaStrikeGame.stringStorage.playerOceanGridLabel;
        showShips = true;

        Initialize();
    }
}