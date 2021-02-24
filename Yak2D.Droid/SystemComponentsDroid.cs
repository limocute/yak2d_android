using System;
using Veldrid;
using Yak2D.Internal;
using Yak2D.Utility;
using Veldrid.StartupUtilities;
using Veldrid.Utilities;
using Yak2D.Core;

namespace Yak2D.Droid
{
    public class SystemComponentsDroid : ISystemComponents
    {
        
        public GraphicsApi GraphicsApi { get => Device.BackendType; }
        public bool CurrentlyReinitialisingDevices { get; private set; }

        public IWindow Window { get; private set; }
        public IDevice Device { get; private set; }
        public IFactory Factory { get; private set; }

        public TexturePixelFormat SwapChainFramebufferPixelFormat { get; private set; }
        public Internal.TextureSampleCount SwapChainFramebufferSampleCount { get; private set; }

        private readonly IFrameworkMessenger _frameworkMessenger;
        private readonly IApplicationMessenger _applicationMessenger;

        private readonly StartupConfig _userStartupProperties;

        private readonly GraphicsDevice _device;
        private bool _vsync;

        public SystemComponentsDroid(IStartupPropertiesCache defaultPropertiesCache,
                               IFrameworkMessenger frameworkMessenger,
                               IApplicationMessenger applicationMessenger,
                               GraphicsDevice device)
        {
            _frameworkMessenger = frameworkMessenger;
            _applicationMessenger = applicationMessenger;
            _device = device;
            _frameworkMessenger.Report("Veldrid Components Initialising...");

            _userStartupProperties = defaultPropertiesCache.User;

            _vsync = _userStartupProperties.SyncToVerticalBlank;


            CreateGraphicsDevice();
        }

        private void CreateGraphicsDevice()
        {
            Device = new VeldridDevice(_device);
            Factory = new VeldridFactory(new DisposeCollectorResourceFactory(_device.ResourceFactory));

            RecordSwapChainBackBufferFormats();
            _frameworkMessenger.Report("Graphics API Chosen: " + Device.BackendType.ToString());
          
        }

        private void RecordSwapChainBackBufferFormats()
        {
            SwapChainFramebufferPixelFormat = TexturePixelFormatConverter.ConvertVeldridToYak(Device.SwapchainFramebufferOutputDescription.ColorAttachments[0].Format);
            SwapChainFramebufferSampleCount = TextureSampleCountConverter.ConvertVeldridToYak(Device.SwapchainFramebufferOutputDescription.SampleCount);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        public bool IsGraphicsApiSupported(GraphicsApi api)
        {
            if (api != GraphicsApi.Vulkan)
                return false;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        /// <param name="systemPreAppReinitialisation"></param>
        public void SetGraphicsApi(GraphicsApi api, Action systemPreAppReinitialisation)
        {
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemPreAppReinitialisation"></param>
        public void RecreateDeviceAndReinitialiseAllResources(Action systemPreAppReinitialisation)
        {
            RecreateDeviceAndReinitialiseAllResources(systemPreAppReinitialisation, GraphicsApiConverter.ConvertApiToVeldridGraphicsBackend(GraphicsApi));
        }

        private void RecreateDeviceAndReinitialiseAllResources(Action systemPreAppReinitialisation, GraphicsBackend graphicsBackend)
        {
            CurrentlyReinitialisingDevices = true;

            ReleaseResources();

            _vsync = Device.RawVeldridDevice.SyncToVerticalBlank;

            CreateGraphicsDevice();
            systemPreAppReinitialisation();

            CurrentlyReinitialisingDevices = false;

            _applicationMessenger.QueueMessage(FrameworkMessage.GraphicsDeviceRecreated);
        }
        /// <summary>
        /// 
        /// </summary>
        public void ReleaseResources()
        {
            _frameworkMessenger.Report("Releasing all Veldrid Components...");

            Device.WaitForIdle();
            Factory.DisposeAll();
            Device.Dispose();
        }

    }
}
