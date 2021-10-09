namespace UnrealBuildTool.Rules
{
    using System;
    using System.IO;
    using System.Collections.Generic;

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
                    "InputCore",
                    "Slate",
                    "SlateCore"
                });

            PrivateIncludePathModuleNames.AddRange(
                new string[]
                {
                    "InputDevice",
                });

            PrivateDependencyModuleNames.AddRange(
                new string[]
                {
                    "ApplicationCore",
                    "Projects"
                });


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

            if (Target.Type == TargetRules.TargetType.Editor)
            {
                PrivateIncludePathModuleNames.AddRange(
                    new string[]
                    {
                        "PropertyEditor",
                        "ActorPickerMode",
                        "DetailCustomizations",
                    });

                PrivateDependencyModuleNames.AddRange(
                    new string[]
                    {
                        "PropertyEditor",
                        "DetailCustomizations",
						// ... add private dependencies that you statically link with here ...
					});
            }
        }
    }
}
