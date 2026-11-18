using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChartTestFramwork
{
    internal class ModelXML : IModelLocalData
    {

        private XDocument doc;

        private IViewECG viewECG;
        
        private IModelECGDevice modelECGDevice;

        private IControllerECG controllerECG;

        IViewECG IModelLocalData.ViewEKG { set => viewECG = value; }
        IModelECGDevice IModelLocalData.ModelECGDevice { set => modelECGDevice = value; }
        IControllerECG IModelLocalData.ControllerECG { set => controllerECG = value; }

        public ModelXML()
        {
            this.doc = new XDocument(new XElement("EKGDaten"));
        }



        List<double> IModelLocalData.get24hData()
        {
            throw new NotImplementedException();
        }

        void IModelLocalData.saveLiveData(ECGValue aktuellerWert)
        {
            throw new NotImplementedException();
        }

        void IModelLocalData.set24hData(ECGValue aktuellerWert)
        {
            throw new NotImplementedException();
        }
    }
}
