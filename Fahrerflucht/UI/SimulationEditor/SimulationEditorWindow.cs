using Simulation;
using Fahrerflucht.UI.IMGUI;
using Fahrerflucht.UI.SimulationPreview;
using ImGuiNET;
using Raylib_cs;
using System.Numerics;
using Utils;

namespace Fahrerflucht.UI.SimulationEditor
{
    internal class SimulationEditorWindow : WindowBase
    {
        public SimulationEditorWindow(RunInstance runInstance, SimulationPreviewWindow simPreview)
        {
            // Simulation preview
            _simPreview = simPreview;
            _simulationVP = new SimulationVPDTO { Zoom = 1.75f, XOffset = 515, YOffset = 412, RenderCheckPoints = false, RenderCarImages = true, RenderDeadAgents = false, RenderSensors = false, BackgroundColor = ThemeConstants.DarkThemeBg, RenderMapBG = true};
            _simPreview.ChangeViewSettings(_simulationVP);

            // Simulation settings
            _runInstance = runInstance;
            _curSimulation = null;
            _agentSettings = new AgentSettingsDTO { XSpawn = 150, YSpawn = 40};

            // UI Theme
            _isDarkThemed = true;
        }

        private bool IsSimulationRunning()
        {
            return _curSimulation != null;
        }

        public override void Show(bool firstOpen)
        {
            if (ImGui.Begin("Simulation Editor", ref Open))
            {
                if (firstOpen)
                {
                    ImGui.SetWindowPos(new Vector2(0, 0));
                    ImGui.SetWindowSize(new Vector2(Raylib.GetScreenWidth() * 0.2f, Raylib.GetScreenHeight()));
                }

                if (firstOpen)
                    ImGui.SetNextItemOpen(true);
                if (ImGui.TreeNode("Simulation Viewport"))
                {  
                    ImGui.Text("Virtual Camera Settings");
                    ImGui.SliderFloat("Zoom", ref _simulationVP.Zoom, 0, 4, _simulationVP.Zoom.ToString("0.000"));
                    ImGui.SliderInt("X Offset", ref _simulationVP.XOffset, -2000, 2000, _simulationVP.XOffset.ToString());
                    ImGui.SliderInt("Y Offset", ref _simulationVP.YOffset, -2000, 2000, _simulationVP.YOffset.ToString());
                    ImGui.Checkbox("Render Checkpoints", ref _simulationVP.RenderCheckPoints);
                    ImGui.Checkbox("Render Car Images", ref _simulationVP.RenderCarImages);
                    ImGui.Checkbox("Render Dead Agents", ref _simulationVP.RenderDeadAgents);
                    ImGui.Checkbox("Render Sensors", ref _simulationVP.RenderSensors);
                    ImGui.Checkbox("Render Map BG", ref _simulationVP.RenderMapBG);
                    _simPreview.ChangeViewSettings(_simulationVP);
                    if (ImGui.Button("Change Theme (Dark/White)"))
                    {
                        if (_isDarkThemed)
                        {
                            ImGui.StyleColorsLight();
                            _simulationVP.BackgroundColor = ThemeConstants.WhiteThemeBg;
                            _isDarkThemed = false;
                        }
                        else
                        {
                            ImGui.StyleColorsDark();
                            _simulationVP.BackgroundColor = ThemeConstants.DarkThemeBg;
                            _isDarkThemed = true;
                        }
                    }
                    ImGui.TreePop();
                }

                if (firstOpen)
                    ImGui.SetNextItemOpen(true);
                if (ImGui.TreeNode("Simulation Settings"))
                {
                    ImGui.Text("Agent Spawn(!Unused)");
                    ImGui.InputInt("Spawn X", ref _agentSettings.XSpawn);
                    ImGui.InputInt("Spawn Y", ref _agentSettings.YSpawn);
                    _simPreview.ChangeAgentSettings(_agentSettings);

                    if (ImGui.Button("Start Simulation"))
                    {
                        if (!IsSimulationRunning())
                        {
                            System.Console.WriteLine("Starting simulation");
                            _curSimulation = new Simulation.Simulation(_runInstance);
                            _curSimulation.StartDetached();
                            _simPreview._curSimulation = _curSimulation;
                        }
                        else
                        {
                            System.Console.WriteLine("Simulation is already running!");
                        }
                    }

                    ImGui.Button("Start Team Game");
                    ImGui.TreePop();
                }

                ImGui.End();
            }
        }

        // Simulation preview
        private SimulationPreviewWindow _simPreview;
        private SimulationVPDTO _simulationVP;

        // Simulation
        private RunInstance _runInstance;
        private Simulation.Simulation _curSimulation;
        private Simulation.AgentSettingsDTO _agentSettings;

        // Theme
        private bool _isDarkThemed;
    }
}
