using System.IO;

namespace UnrealBuildTool.Rules
{
    public class JoystickPlugin : ModuleRules
    {
        public JoystickPlugin(ReadOnlyTargetRules Target) : base(Target)
        {
            PublicDependencyModuleNames.AddRange(
                new string[]
                {
                    "Core",
                    "CoreUObject",
                    "Engine",
                    "ApplicationCore",
                    "InputCore",
                    "SlateCore",
                    "Slate"
                });

            PrivateDependencyModuleNames.AddRange(
                new string[]
                {
                    "Projects",
                    "InputDevice"
                });
            
#if UE_4_26_OR_LATER
            PrivateDependencyModuleNames.Add("DeveloperSettings");
#endif
            
			var SDLDirectory = Path.Combine(PluginDirectory, "Source", "ThirdParty", "SDL2");
			var SDL2IncPath = Path.Combine(SDLDirectory, "include");

            PublicIncludePaths.Add(SDL2IncPath);

            var SDLPlatformDir = Path.Combine(SDLDirectory, "lib", "x64");

            if (Target.Platform == UnrealTargetPlatform.Win64)
            {
                RuntimeDependencies.Add(Path.Combine(SDLPlatformDir, "SDL2.dll"));
                PublicAdditionalLibraries.Add(Path.Combine(SDLPlatformDir, "SDL2.lib"));

                PublicDelayLoadDLLs.Add("SDL2.dll");
            }
        }
    }
}
