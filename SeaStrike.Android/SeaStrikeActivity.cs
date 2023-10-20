using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;
using SeaStrike.GameCore.Root;

namespace SeaStrike.Android;

[Activity(
    Label = "@string/app_name",
    MainLauncher = true,
    Icon = "@drawable/icon",
    AlwaysRetainTaskState = true,
    LaunchMode = LaunchMode.SingleInstance,
    ScreenOrientation = ScreenOrientation.Landscape,
    ConfigurationChanges = ConfigChanges.Orientation |
                            ConfigChanges.Keyboard |
                            ConfigChanges.KeyboardHidden |
                            ConfigChanges.ScreenSize
)]
public class SeaStrikeActivity : AndroidGameActivity
{
    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);

        SeaStrikeGame game = new SeaStrikeGame();
        View view = game.Services.GetService(typeof(View)) as View;

        SetContentView(view);
        game.Run();
    }
}
