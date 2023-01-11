using Fahrerflucht.UI.IMGUI;
using Fahrerflucht.UI.SimulationPreview;
using Fahrerflucht.UI.SimulationEditor;
using Raylib_cs;
using System.Collections.Generic;
using System;
using Fahrerflucht.UI;
using Utils;

namespace Fahrerflucht
{
    /// <summary>
    /// Main application class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Application Entry-point.
        /// </summary>
        /// <param name="args">
        /// - None                              | Start with the UI and the default config "run.json"
        /// - {Config(string)}                  | Start with the supplied supplied config and the UI
        /// - {Config(string)} { Visible(bool)} | Start with the supplied config and the UI if 'Visible' is true, if not the simulation runs without ui automatically
        /// </param>
        public static void Main(string[] args)
        {
            var config = args.Length > 0 ? args[0] : "run.json";
            Console.WriteLine($"Using config {config}");
            var runInstance = RunInstance.FromFile(config);
            
            if (args.Length == 2 && !Convert.ToBoolean(args[1])/*Visible*/)
            {
                // Simulation run without UI
                new Simulation.Simulation(runInstance).Start();
            }
            else
            {
                // Simulation with UI
                Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
                Raylib.InitWindow(1600, 900, "Fahrerflucht");
                rlImGui.Setup();

                var windowStack = new Stack<WindowBase>();
                var preview = new SimulationPreviewWindow(runInstance);
                windowStack.Push(new SimulationEditorWindow(runInstance, preview));
                windowStack.Push(preview);

                while (!Raylib.WindowShouldClose())
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(ThemeConstants.ClearBg);
                    rlImGui.Begin();
                    foreach (var window in windowStack)
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