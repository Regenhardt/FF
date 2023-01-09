using Raylib_cs;

namespace Fahrerflucht.UI.SimulationPreview
{
    internal class SimulationVPDTO
    {
        public float    Zoom;
        public int      XOffset;
        public int      YOffset;
        public bool     RenderCheckPoints;
        public bool     RenderCarImages;
        public bool     RenderSensors;
        public bool     RenderDeadAgents;
        public bool     RenderMapBG;
        public Color    BackgroundColor;
    }
}
