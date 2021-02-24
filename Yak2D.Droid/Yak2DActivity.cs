using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veldrid;

namespace Yak2D.Droid
{
    public class Yak2DActivity: AppCompatActivity
    {
        private Yak2DSurfaceView _view;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            bool debug = false;
#if DEBUG
            debug = true;
#endif

            GraphicsDeviceOptions options = new GraphicsDeviceOptions(
                debug,
                PixelFormat.R16_UNorm,
                false,
                ResourceBindingModel.Improved,
                true,
                true);
            GraphicsBackend backend = GraphicsDevice.IsBackendSupported(GraphicsBackend.Vulkan)
                ? GraphicsBackend.Vulkan
                : GraphicsBackend.OpenGLES;
            _view = new Yak2DSurfaceView(this, backend, options);
            DroidWindow window = new DroidWindow(this, _view);
            window.GraphicsDeviceCreated += (g, r, s) =>
            {
                //window.Run();
                Launcher.Run(Yak2DApplication, g, new NullMessenger());
            };
            SetContentView(_view);
        }

        protected virtual IApplication Yak2DApplication { get; }

        protected override void OnPause()
        {
            base.OnPause();
            _view.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _view.OnResume();
        }
    }

    public class NullMessenger : IFrameworkMessenger
    {
        public NullMessenger() { }

        public void Report(string message)
        {

        }

        public void Shutdown()
        {

        }
    }
}