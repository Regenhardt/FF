using System.Linq;
using Simulation;
using Simulation.Model.Agent;
using Fahrerflucht.UI.IMGUI;
using ImGuiNET;
using Raylib_cs;
using System.Numerics;
using Utils;

namespace Fahrerflucht.UI.SimulationPreview
{
    internal class SimulationPreviewWindow : WindowBase
    {
        public SimulationPreviewWindow(RunInstance runInstance)
        {
            _camera = new Camera2D();
            _lastSize = new Vector2(0, 0);
            _agentSpawn = new Vector2(0, 0);
            _viewTexture = Raylib.LoadRenderTexture(0, 0);
            _runInstance = runInstance;
            _curSimulation = null;

            _carTexture = Raylib.LoadTexture(ThemeConstants.CarImage);
            _carTextureRect = new Rectangle(0, 0, _carTexture.width, _carTexture.height);
            if (_runInstance.Config.MapBG != null)
            {
                _trackTexture = Raylib.LoadTexture(_runInstance.Config.MapBG);
            }
        }

        public override void Shutdown()
        {
            Raylib.UnloadRenderTexture(_viewTexture);
            Raylib.UnloadTexture(_carTexture);
        }

        public void ChangeViewSettings(SimulationVpDto simulationVpDto)
        {
            _camera.target = new Vector2(_viewTexture.texture.width / 2.0f, _viewTexture.texture.height / 2.0f);
            _camera.offset = new Vector2(_viewTexture.texture.width / 2.0f + simulationVpDto.XOffset, _viewTexture.texture.height / 2.0f + simulationVpDto.YOffset);
            _camera.zoom = simulationVpDto.Zoom;
            _camera.rotation = 0.0f;
            _settings = simulationVpDto;
        }

        public void ChangeAgentSettings(AgentSettingsDTO agentSettings)
        {
            _agentSpawn = new Vector2(agentSettings.XSpawn, agentSettings.YSpawn);
        }

        public void SetSimulation(Simulation.Simulation simulation)
        {
            _curSimulation = simulation;
        }

        public override void Show(bool firstOpen)
        {
            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
            ImGui.SetNextWindowSizeConstraints(new Vector2(400, 400), new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()));

            if (ImGui.Begin("Simulation Preview", ref Open))
            {
                if (firstOpen)
                {
                    ImGui.SetWindowPos(new Vector2(Raylib.GetScreenWidth() * 0.2f, 0));
                    ImGui.SetWindowSize(new Vector2(Raylib.GetScreenWidth() * 0.8f, Raylib.GetScreenHeight()));
                }

                Focused = ImGui.IsWindowFocused(ImGuiFocusedFlags.ChildWindows);
                

                var size = ImGui.GetContentRegionAvail();
               
                if (_lastSize.X != size.X || _lastSize.Y != size.Y)
                {
                    Raylib.UnloadRenderTexture(_viewTexture);
                    //Vector2 sizeUÍ = ImGui.GetWindowSize();
                    _viewTexture = Raylib.LoadRenderTexture((int)size.X, (int)size.Y);
                    _lastSize = size;
                }

                // draw the view
                var viewRect = new Rectangle(_viewTexture.texture.width, _viewTexture.texture.height, size.X, -size.Y);
                rlImGui.ImageRect(_viewTexture.texture, (int)size.X, (int)size.Y, viewRect);
                Update();
                ImGui.End();
            }

            ImGui.PopStyleVar();
        }

        private void RenderTrack()
        {
            if (_settings.RenderMapBg && _trackTexture.width != 0 && _trackTexture.height != 0)
            {
               Raylib.DrawTexture(_trackTexture, 0, 0, Color.RAYWHITE);
            } 
            else
            {
                foreach (var feature in _runInstance.Track.Features)
                {
                    foreach (var geoAry in feature.Geometry.Coordinates)
                    {
                        var startVector = new Vector2(geoAry.First()[0], geoAry.First()[1]);
                        foreach (var endVector in geoAry.Skip(1).Select(geo => new Vector2(geo[0], geo[1])))
                        {
                            Raylib.DrawLineEx(startVector, endVector, 3, ThemeConstants.TrackOutline);
                            startVector = endVector;
                        }
                    }
                }
            }

            if (_settings.RenderCheckPoints)
            {
                foreach (var cp in _runInstance.CheckPoints)
                {
                    Raylib.DrawCircle(cp[0], cp[1], 3, ThemeConstants.CheckPoint);
                }
            }
        }
        
        private void RenderAgent(AgentSnapshotDTO agentDTO)
        {
            if (_settings.RenderCarImages)
            {
                var rec = new Rectangle(agentDTO.Position.X, agentDTO.Position.Y, _carTextureRect.width, _carTextureRect.height);
                Raylib.DrawTexturePro(_carTexture, _carTextureRect, rec, new Vector2(_carTextureRect.width/2, _carTextureRect.height/2), MathUtils.GetAngle(agentDTO.MovingVec, 1, 0), agentDTO.Color);
            }

            Raylib.DrawCircle((int)agentDTO.Position.X, (int)agentDTO.Position.Y, 2, agentDTO.Color);

            if (_settings.RenderSensors)
            {
                foreach (var sensor in agentDTO.SensorData)
                {
                    var sensorPos = agentDTO.Position + sensor.SensorVec;
                    Raylib.DrawCircle((int)sensorPos.X, (int)sensorPos.Y, 2, ThemeConstants.Sensor);
                    Raylib.DrawLine((int)agentDTO.Position.X, (int)agentDTO.Position.Y, (int)sensorPos.X, (int)sensorPos.Y, new Color(ThemeConstants.Sensor.r, ThemeConstants.Sensor.g, ThemeConstants.Sensor.b, (int)(sensor.LastSensorRead * ThemeConstants.Sensor.a)));
                }
            }
        }

        private void RenderAgentSpawn()
        {
            if (_settings.RenderCarImages)
            {
                Raylib.DrawTexture(_carTexture, (int)(_agentSpawn.X - (_carTextureRect.width/2)), (int)(_agentSpawn.Y - (_carTextureRect.height / 2)), new Color(250, 12, 12, 255));
            }
            else
            {
                Raylib.DrawCircle((int)_agentSpawn.X, (int)_agentSpawn.Y, 10, ThemeConstants.AgentSpawn);
            }

            Raylib.DrawText("Agent Spawn", (int)_agentSpawn.X + (int)(10 * 1.4f), (int)_agentSpawn.Y - 7, 14, ThemeConstants.AgentSpawn);
        }
        
        public virtual void Update()
        {
            Raylib.BeginTextureMode(_viewTexture);
            Raylib.ClearBackground(_settings.BackgroundColor);
            Raylib.BeginMode2D(_camera);

            if (_runInstance != null)
            {
                RenderTrack();

                if (_curSimulation != null && _curSimulation.AgentsReady())
                {
                    foreach (var snapshot in _curSimulation.GetAgentSnapshots().Where(snapshot => snapshot.IsAlive || _settings.RenderDeadAgents))
                    {
                        RenderAgent(snapshot);
                    }
                }
                else
                {
                    RenderAgentSpawn();
                }
            }
            
            Raylib.EndMode2D();
            Raylib.EndTextureMode();
        }

        // View settings
        private Camera2D _camera;
        private SimulationVpDto _settings;

        // Render textures
        private readonly Texture2D _carTexture;
        private readonly Rectangle _carTextureRect;
        private readonly Texture2D _trackTexture;

        // Simulation context
        private Simulation.Simulation _curSimulation;
        private Vector2 _lastSize;
        private Vector2 _agentSpawn;
        private readonly RunInstance _runInstance;
    }
}