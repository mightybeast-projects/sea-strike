namespace SeaStrike.PC.Root;

public class StringStorage
{
    //General strings
    public string contentPath = "Content";
    public string fontPath = "SeaStrike.PC/Content/Font/Tektur.ttf";
    public string errorWindowTitle = "Error";


    //Battle phase screen strings
    public string battlePhaseScreenTitle = "Battle phase";
    public string helpButtonLabel = "?";
    public string[] helpWindowContent = new[]
    {
        "Sink your opponent ships to win the game.\n",
        "Choose and click on the tile of your opponent's grid you wish to shoot at."
    };
    public string playerOceanGridLabel = "Your's ocean grid : ";
    public string opponentOceanGridLabel = "Opponent's ocean grid : ";
    public string victoryScreenTitle = "You won!";
    public string loseScreenTitle = "You lost!";
}