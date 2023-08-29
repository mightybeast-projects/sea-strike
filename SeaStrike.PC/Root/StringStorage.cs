namespace SeaStrike.PC.Root;

public class StringStorage
{
    //General strings
    public string contentPath = "Content";
    public string fontPath = "SeaStrike.PC/Content/Font/Tektur.ttf";
    public string errorWindowTitle = "Error";

    //Main menu screen strings
    public string gameTitle = "SEA STRIKE";
    public string gameModeLabel = "Choose game mode :";
    public string singlePlayerButtonLabel = "Single player";
    public string multiplayerButonLabel = "Local multiplayer";

    //Deployment phase screen strings
    public string backButtonLabel = "<=";
    public string deploymentPhaseScreenTitle = "Deployment phase";
    public string[] dpHelpWindowContent = new[]
    {
        "Place all 5 ships on the grid.\n",
        "Choose and click on the tile you wish to place your ship to.\n",
        "Click on already placed ship to remove it from the grid.\n",
        "You can start game once all 5 ships has been placed."
    };
    public string startGameButtonLabel = "Start game";

    //Battle phase screen strings
    public string battlePhaseScreenTitle = "Battle phase";
    public string helpButtonLabel = "?";
    public string[] bpHelpWindowContent = new[]
    {
        "Sink your opponent ships to win the game.\n",
        "Choose and click on the tile of your opponent's grid you wish to shoot at."
    };
    public string playerOceanGridLabel = "Your's ocean grid : ";
    public string opponentOceanGridLabel = "Opponent's ocean grid : ";
    public string victoryScreenTitle = "You won!";
    public string loseScreenTitle = "You lost!";
}