namespace SeaStrike.GameCore.Root;

public class StringStorage
{
    //General strings
    public string contentPath = "Content";
    public string pcFontPath = "Content/Font/Tektur.ttf";
    public string androidFontPath = "Font/Tektur.ttf";

    //Main menu screen strings
    public string gameTitle = "SEA STRIKE";
    public string gameModeLabel = "Choose game mode :";
    public string singlePlayerButtonLabel = "Single player";
    public string multiplayerButonLabel = "Local multiplayer";

    //Deployment phase screen strings
    public string deploymentPhaseScreenTitle = "Deployment phase";
    public string[] dpHelpWindowContent = new[]
    {
        "Place all 5 ships on the grid.\n",
        "Choose and click on the tile you wish to place your ship to.\n",
        "Click on already placed ship to remove it from the grid.\n",
        "You can start game once all 5 ships has been placed."
    };
    public string startGameButtonLabel = "Start game";

    //Grid buttons strings
    public string randomizeButtonString = "Randomize";
    public string clearButtonString = "Clear grid";

    //Ship addition window strings
    public string shipAdditionWindowTitle = "Select ship properties : ";
    public string selectedPositionLabel = "Selected position : ";
    public string shipTypeLabel = "Ship type : ";
    public string shipWidthLabel = "Width : ";
    public string shipOrientationLabel = "Ship orientation : ";
    public string[] orientationBoxItems = new[]
    {
        "Horizontal",
        "Vertical"
    };
    public string createShipButtonLabel = "Create new ship";

    //Battle phase screen strings
    public string battlePhaseScreenTitle = "Battle phase";
    public string[] bpHelpWindowContent = new[]
    {
        "Sink your opponent ships to win the game.\n",
        "Choose and click the tile on your opponent's grid you wish to shoot at."
    };
    public string playerOceanGridLabel = "Your ocean grid : ";
    public string opponentOceanGridLabel = "Opponent ocean grid : ";

    //Game window strings
    public string errorWindowTitle = "Error";
    public string helpWindowTitle = "Help";
    public string victoryScreenTitle = "You won!";
    public string loseScreenTitle = "You lost!";
    public string exitButtonLabel = "Exit to main menu";

    //Game button strings
    public string backButtonLabel = "<=";
    public string helpButtonLabel = "?";

    //Lobby screen strings
    public string lobbyScreenLabel = "Lobby screen";
    public string createLobbyButtonLabel = "Create new lobby";
    public string connectToLobbyButtonLabel = "Connect to existing lobby";
    public string createdLobbyLabel = "Created new lobby.\nWaiting for another player to connect...";
    public string cannotDiscoverAnyLobbyLabel = "Cannot discover any lobbies in your local network.\nCreate one or try again later.";

    //Multiplayer deployment phase screen
    public string readyWindowTitle = "Ships deployed!";
    public string readyWindowContentLabel = "Waiting for other player...";

    //Multiplayer battle plhase screen
    public string yourTurnLabel = "Your turn.";
    public string opponentsTurnLabel = "Opponent's turn.";

    //Lobby disconnection warning window
    public string disconnectionWarningWindowTitle = "Disconnection warning";
    public string disconnectionWarningLabel = "This action will close lobby you are currently in.\nAll connected players will be disconnected.\nAre you sure you want to proceed?";
    public string confirmButtonLabel = "Confirm";
}