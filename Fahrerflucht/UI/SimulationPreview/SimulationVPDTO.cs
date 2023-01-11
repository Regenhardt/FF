using Raylib_cs;

namespace Fahrerflucht.UI.SimulationPreview
{
    internal class SimulationVpDto
    {
        public float    Zoom;
        public int      XOffset;
        public int      YOffset;
        public bool     RenderCheckPoints;
        public bool     RenderCarImages;
        public bool     RenderSensors;
        public bool     RenderDeadAgents;
        public bool     RenderMapBg;
        public Color    BackgroundColor;
    }
}
