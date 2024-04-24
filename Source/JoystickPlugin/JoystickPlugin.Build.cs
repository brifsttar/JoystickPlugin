using System.IO;
using UnrealBuildTool;

public class JoystickPlugin : ModuleRules
{
	public JoystickPlugin(ReadOnlyTargetRules Target) : base(Target)
	{
		PublicDependencyModuleNames.AddRange(
		new[]
		{
			"Core",
			"CoreUObject",
			"Engine",
			"SlateCore",
			"Slate",
			"ApplicationCore",
			"InputCore",
			"InputDevice",
			"UMG",
			"Projects"
		});

		var SDLDirectory = Path.Combine(PluginDirectory, "Source", "ThirdParty", "SDL2");
		var Sdl2IncludePath = Path.Combine(SDLDirectory, "include");

		PublicIncludePaths.Add(Sdl2IncludePath);

        var SdlPlatformDirectory = Path.Combine(SDLDirectory, "lib", "x64");

		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
			RuntimeDependencies.Add(Path.Combine(SdlPlatformDirectory, "SDL2.dll"));
			PublicAdditionalLibraries.Add(Path.Combine(SdlPlatformDirectory, "SDL2.lib"));

			PublicDelayLoadDLLs.Add("SDL2.dll");
		}
		else if (Target.Platform == UnrealTargetPlatform.Linux)
		{
			//SDL should be loaded as part of the engine
		}
	}
}
