using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yak2D;

namespace TestApp
{
    public class App : AppBase
    {
        public override bool CreateResources(IServices services)
        {

            return true;
        }

        public override void Drawing(IDrawing draw, IFps fps, IInput input, ICoordinateTransforms transforms, float timeSinceLastDrawSeconds, float timeSinceLastUpdateSeconds)
        {
            // throw new NotImplementedException();
        }

        public override void OnStartup()
        {
            //throw new NotImplementedException();
        }

        public override void PreDrawing(IServices yak, float timeSinceLastDrawSeconds, float timeSinceLastUpdateSeconds)
        {
            // throw new NotImplementedException();
        }

        public override void Rendering(IRenderQueue q, IRenderTarget windowRenderTarget)
        {
            // throw new NotImplementedException();
        }

        public override string ReturnWindowTitle() => "Test.测试.اختبر.테스트 하 다.テスト.";

        public override void Shutdown()
        {
            //throw new NotImplementedException();
        }

        public override bool Update_(IServices yak, float timeSinceLastUpdateSeconds)
        {
            return true;
        }
    }
}