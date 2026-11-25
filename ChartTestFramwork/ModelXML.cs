using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChartTestFramwork
{
    internal class ModelXML : IModelLocalData
    {

        // Path für eine eventuelle anbindung an die View zur Festlegung des XML Pfades
        // @ wird dafür verwendet, den String 1:1 so zu nehmen wie er dort ist. \n o.Ä. funktioniert hier also nicht
        private String xmlPath = @".\EKGData\";

        // Einfache änderung der Root Bezeichnung möglich
        private String xmlRootName = "EKGDaten";

        private XDocument doc;

        private IViewECG viewECG;
        
        private IModelECGDevice modelECGDevice;

        private IControllerECG controllerECG;

        IViewECG IModelLocalData.ViewEKG { set => viewECG = value; }
        IModelECGDevice IModelLocalData.ModelECGDevice { set => modelECGDevice = value; }
        IControllerECG IModelLocalData.ControllerECG { set => controllerECG = value; }

        /// <summary>
        /// Der Pfad der XML Datei. Ohne Dateiname und .xml
        /// </summary>
        public String XMLPath { set => xmlPath = value; get => xmlPath; }

        public ModelXML()
        {
            doc = new XDocument(new XElement(xmlRootName));
            // Überprüfung ob der Ordner Path überhaupt existiert
            if (!Directory.Exists(xmlPath))
            {
                // Wenn der Ordner nicht Existiert, erstelle ihn
                Directory.CreateDirectory(xmlPath);
            }
        }

        /// <summary>
        /// Setzt einen neuen Datensatz in die XML Datei <br></br>
        /// Speichert nach jeder änderung die Datei erneut ab
        /// </summary>
        /// <param name="datensatz">Der Neue Datensatz</param>
        private void addEKGDatensatz(XElement datensatz)
        {

            doc.Element(xmlRootName).Add(datensatz);
            doc.Save(xmlPath + "EKGData.xml");

        }

        List<double> IModelLocalData.get24hData()
        {
            throw new NotImplementedException();
        }

        void IModelLocalData.saveLiveData(ECGValue aktuellerWert)
        {
            // Zeitstempel der aktuellen Messung zum Speichern hier abspeichern
            // erleichtert ggf. spätere Formatierung des Time Stamps
            String currentValue = aktuellerWert.TimeStamp.ToString();

            // Deklariere und Initalisiere das neue XElement mit den Datensatz
            XElement ekgDatensatz = new XElement("EKGDatensatz",
                                        new XElement("Zeitstempel", currentValue),
                                        new XElement("Wert", aktuellerWert.Value));

            // Füge den neuen Datensatz zu der XML Datei hinzu und speichere die Datei
            addEKGDatensatz(ekgDatensatz);

        }

        void IModelLocalData.set24hData(ECGValue aktuellerWert)
        {
            throw new NotImplementedException();
        }
    }
}
