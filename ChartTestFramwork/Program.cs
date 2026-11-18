using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartTestFramwork
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        /// 

        private static IModelLocalData modelLocaldata;
        private static IModelECGDevice modelEKGDevice;
        private static IViewECG viewEKG;
        private static IControllerECG controllerEKG;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            modelLocaldata = new ModelXML();
            modelEKGDevice=new ModelECGDevice();
            viewEKG=new ViewECG();
            controllerEKG=new ControllerECG();

            modelLocaldata.ControllerECG=controllerEKG;
            modelLocaldata.ViewEKG=viewEKG;
            modelLocaldata.ModelECGDevice=modelEKGDevice;

            modelEKGDevice.ControllerECG=controllerEKG;
            modelEKGDevice.ViewECG = viewEKG;
            modelEKGDevice.ModelLocaldata=modelLocaldata;

            viewEKG.ControllerECG=controllerEKG;
            viewEKG.ModelECGDevice=modelEKGDevice;
            viewEKG.ModelLocalData=modelLocaldata;

            controllerEKG.ModelLocaldata=modelLocaldata ;
            controllerEKG.ModelEKGDevice=modelEKGDevice ;
            controllerEKG.ViewEKG=viewEKG ;


            Application.Run((ViewECG)viewEKG);
        }
    }
}
