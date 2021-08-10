namespace UnrealBuildTool.Rules
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    public class JoystickPlugin : ModuleRules
    {
        private void CopySdl(string From, string To, string Filename)
        {
            string Dest = Path.Combine(To, Filename);
            if (File.Exists(Dest)) return;
            Directory.CreateDirectory(To);
            File.Copy(Path.Combine(From, Filename), Dest);
        }

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

            bEnableUndefinedIdentifierWarnings = false;

            PrivateIncludePathModuleNames.Add("SDL2");

            //PrivateIncludePaths.AddRange(
            //	new string[] {
            //			Path.Combine(EngineDirectory, "/Source/ThirdParty/SDL2/SDL-gui-backend/include"),
            //	}
            //);

            AddEngineThirdPartyPrivateStaticDependencies(Target, "SDL2");

            PrivateIncludePathModuleNames.AddRange(
                new string[]
                {
                    "InputDevice",
                });

            if (Target.Platform == UnrealTargetPlatform.Win64)
            {
                // UE doesn't ship SDL lib/dll for Windows, so we do it ourselves
                string SdlSrc = Path.Combine(ModuleDirectory, "..", "ThirdParty", "Win64");
                string SdlLib = Path.Combine(EngineDirectory, "Source", "ThirdParty", "SDL2", "SDL-gui-backend", "lib", "Win64");
                CopySdl(SdlSrc, SdlLib, "SDL2.lib");

                RuntimeDependencies.Add(Path.Combine("$(EngineDir)", "Binaries", "ThirdParty", "SDL2", "Win64/SDL2.dll"));
                PublicDelayLoadDLLs.Add("SDL2.dll");

                // I thought by putting the DLL there UE would find it, but no...
                //string SdlDll = Path.Combine(EngineDirectory, "Binaries", "ThirdParty", "SDL2", "Win64");
                //CopySdl(SdlSrc, SdlDll, "SDL2.dll");
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
