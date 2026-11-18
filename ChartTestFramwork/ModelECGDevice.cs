using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartTestFramwork
{
    internal class ModelECGDevice : IModelECGDevice
    {
        private IModelLocalData modelLocalData;
        private IViewECG viewEKG;
        private IControllerECG controllerEKG;
        private ECGDataRecievedEventArgs data = new ECGDataRecievedEventArgs();


        IViewECG IModelECGDevice.ViewECG { set => viewEKG=value; }
        IModelLocalData IModelECGDevice.ModelLocaldata { set => modelLocalData=value; }
        IControllerECG IModelECGDevice.ControllerECG { set => controllerEKG=value; }

        private SerialPort serialPort1=new SerialPort();
        private int recievedInt=0;
        private bool live = false;
        private string message;

        public delegate void ECGDataRecievedEventHandler(object sender, ECGDataRecievedEventArgs e);

        public event ECGDataRecievedEventHandler ECGDataRecieved;

        protected virtual void OnECGDataRecieved(ECGDataRecievedEventArgs e)
        {
            ECGDataRecievedEventHandler handler = ECGDataRecieved;
            handler?.Invoke(this, e);
        }


        public ModelECGDevice()
        {
            serialPort1.BaudRate = 9600;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Handshake = Handshake.None;
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (live == true)
            {


                try
                {
                    string message = serialPort1.ReadLine();
                    //MessageBox.Show(message);
                    string[] messageparts = message.Split(';');
                    int timestamp = Int32.Parse(messageparts[0]);
                    int wert =Int32.Parse(messageparts[1]);

                    //MessageBox.Show("TimeStamp:" + timestamp.ToString()
                    //    + " Value:" + wert.ToString());

                    recievedInt = wert;
                    viewEKG.NewValue = recievedInt;

                    data.ECGValueRecieved = new ECGValue();

                    data.ECGValueRecieved.Value = wert;
                    data.ECGValueRecieved.TimeStamp= timestamp;
                    OnECGDataRecieved(data);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            return;
        }

        void IModelECGDevice.setPort(string portName)
        {
            serialPort1.PortName = portName;
            
        }

        List<ECGValue> IModelECGDevice.getData24h()
        {
            throw new NotImplementedException();
        }

        


        void IModelECGDevice.deleteData24h()
        {
            throw new NotImplementedException();
        }

        void IModelECGDevice.startLiveData()
        {
            live = true;
            
            try
            {
                if(!serialPort1.IsOpen){
                    serialPort1.Open();
                }
            }
            
            catch (Exception ex)
            { 
                    MessageBox.Show(ex.Message);
            }
        }

        void IModelECGDevice.stopLiveData()
        {
            live=false;
        }

        
    }
}
