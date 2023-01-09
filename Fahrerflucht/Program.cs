using Fahrerflucht.UI.IMGUI;
using Fahrerflucht.UI.SimulationPreview;
using Fahrerflucht.UI.SimulationEditor;
using Raylib_cs;
using System.Collections.Generic;
using System;
using Utils;

namespace Fahrerflucht
{
    static class Program
    {
        /* Params:
         - None                             | Start with the UI
         - {Config(string)}                 | Start with the supplied supplied config and the UI
         - {Config(string)} {Visible(bool)} | Start with the supplied config and the UI if 'Visible' is true, if not the simulation runs without ui automatically
        */
        public static void Main(string[] args)
        {
            RunInstance runInstance = RunInstance.FromFile(args.Length > 0 ? args[0] : "run.json");
            if (args.Length == 2 && !Convert.ToBoolean(args[1])/*Visible*/)
            {
                // Simulation run without UI
                // args[0] = Config
            }
            else
            {
                // Simulation with UI
                Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
                Raylib.InitWindow(1600, 900, "Fahrerflucht");
                rlImGui.Setup(true);

                Stack<WindowBase> windowStack = new Stack<WindowBase>();
                SimulationPreviewWindow preview = new SimulationPreviewWindow(runInstance);
                windowStack.Push(new SimulationEditorWindow(runInstance, preview));
                windowStack.Push(preview);

                while (!Raylib.WindowShouldClose())
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(ThemeConstants.ClearBG);
                    rlImGui.Begin();
                    foreach (WindowBase window in windowStack)
                        window.Show();
                    rlImGui.End();
                    Raylib.EndDrawing();
                }

                rlImGui.Shutdown();
                Raylib.CloseWindow();
            }
        }
    }
}