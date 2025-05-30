#!/usr/bin/env dotnet run
#:package VYaml@1.1.1

using System.Text;
using System.IO;
using VYaml.Annotations;
using VYaml.Serialization;

var text = File.ReadAllText("src/Monry.Sample20250530/ProjectSettings/ProjectSettings.asset");
var unityPlayerSettings = YamlSerializer.Deserialize<UnityProjectSettings>(Encoding.UTF8.GetBytes(text));
Console.WriteLine(
    "BundleIdentifier: {0}\nBundleVersion: {1}\nShowUnitySplashScreen: {2}",
    unityPlayerSettings.PlayerSettings.ApplicationIdentifier.Standalone,
    unityPlayerSettings.PlayerSettings.BundleVersion,
    unityPlayerSettings.PlayerSettings.ShowUnitySplashScreen == 1
);

[YamlObject]
public partial record UnityProjectSettings(
    [property: YamlMember("PlayerSettings")] UnityPlayerSettings PlayerSettings
);

[YamlObject]
public partial record UnityPlayerSettings(
    string CompanyName,
    string ProductName,
    string BundleVersion,
    [property: YamlMember("m_ShowUnitySplashScreen")]
    int ShowUnitySplashScreen,
    UnityPlayerSettingsApplicationIdentifiers ApplicationIdentifier
);

[YamlObject]
public partial record UnityPlayerSettingsApplicationIdentifiers(
    [property: YamlMember("Standalone")] string Standalone,
    [property: YamlMember("iPhone")] string iOS,
    [property: YamlMember("Android")] string Android
)
{
    public string this[string platform] => platform.ToLower() switch
    {
        "standalone" => Standalone,
        "ios"        => iOS,
        "android"    => Android,
        _ => throw new ArgumentOutOfRangeException(nameof(platform))
    };
};

