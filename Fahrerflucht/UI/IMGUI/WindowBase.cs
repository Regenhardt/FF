using Raylib_cs;

namespace Fahrerflucht.UI.IMGUI
{
    internal class WindowBase
    {
        public bool Open = true;

        public bool FirstOpen = true;

        public bool Focused = false;

        protected RenderTexture2D _viewTexture;
        public virtual void Show() { if (Open) { Show(FirstOpen); FirstOpen = false; } }
        public virtual void Show(bool firstOpen) { }
        public virtual void Shutdown() { }
    }
}
