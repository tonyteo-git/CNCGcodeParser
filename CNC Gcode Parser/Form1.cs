//Last run 15:04 03-Aug-20
//Last update: 8:41 04-Aug-20 (change value of A to final value (through by reducer))
//Update 05-Aug-20, change speed of A axis, change command speed in positioning in Gcode_Process()
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public struct PointData
{
    public short Iden; //Da.1 -> Da.4
    public short MCode; // Da.9
    public short Dwell; //Dwell time Da.8
    public short Dummy; // Dummy data, no meaning.
    public short Speed1; // Speed as data 32 bit. Da.7 
    public short Speed2; // x10^-2 mm/min
    public short Address1; // Line address as 32 bit
    public short Address2; // x10^-4 mm or x10^-5 degree
    public short Arc1; // arc address as 32 bit
    public short Arc2;
    //public short Address3; // Address Y
    //public short Address4;hehe
}

namespace CNC_Gcode_Parser
{
    public partial class Form1 : Form
    {
        //--Delaring Field Variables
        int ComID;
        bool ComConnected;
        List<string> GCodeLines = new List<string>();
        PointData[] DataX, DataY, DataZ, DataA;
        //int x = 0, y = 0, z = 0, a = 0, g = 0; //Check availability of X Y Z A
        static int Count;
        static double[,] Point_No = new double[Count, 5];
        List<short> ListPosData1 = new List<short>();
        List<short> ListPosData2 = new List<short>();
        List<short> ListPosData3 = new List<short>();
        List<short> ListPosData4 = new List<short>();
        public short[] PosData1;
        public short[] PosData2;
        public short[] PosData3;
        public short[] PosData4;
        //..

        public Form1()
        {
            InitializeComponent();
            nRunBtn.Enabled = false;
        }

        private void IntTo2Short(int Data32, out short Low16, out short High16)
        {
            Low16 = Convert.ToInt16((Data32 << 16) >> 16);
            High16 = Convert.ToInt16(Data32 >> 16);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string test = "";
            double num = 0;
            num = double.Parse(test);
            IntTo2Short((int)num, out short num1, out short num2);
            MessageBox.Show(num1.ToString() + " and " + num2.ToString());

        }

        private void Connect_button_Click(object sender, EventArgs e)
        {
            // Setting with real PLC
            //PLC.ActPortNumber = ComID;

            PLC.ActPortNumber = ComID;
            PLC.ActUnitType = 1;
            PLC.ActProtocolType = 4;
            PLC.ActNetworkNumber = 0;
            PLC.ActStationNumber = 255;
            PLC.ActUnitNumber = 0;
            PLC.ActConnectUnitNumber = 0;
            PLC.ActIONumber = 0;
            PLC.ActCpuType = 271;
            PLC.ActBaudRate = 19200;
            PLC.ActDataBits = 8;
            PLC.ActParity = 1;
            PLC.ActStopBits = 0;
            PLC.ActControl = 8;
            PLC.ActTimeOut = 10000;
            PLC.ActSumCheck = 0;
            PLC.ActSourceNetworkNumber = 0;
            PLC.ActSourceStationNumber = 0;
            PLC.ActDestinationPortNumber = 0;
            PLC.ActDestinationIONumber = 0;
            PLC.ActMultiDropChannelNumber = 0;
            PLC.ActThroughNetworkType = 0;
            PLC.ActIntelligentPreferenceBit = 0;
            PLC.ActDidPropertyBit = 0;
            PLC.ActDsidPropertyBit = 0;
            PLC.ActPacketType = 1;
            PLC.ActConnectWay = 0;
            PLC.ActLineType = 0;
            PLC.ActConnectionCDWaitTime = 0;
            PLC.ActConnectionModemReportWaitTime = 0;
            PLC.ActDisconnectionCDWaitTime = 0;
            PLC.ActDisconnectionDelayTime = 0;
            PLC.ActTransmissionDelayTime = 0;
            PLC.ActATCommandResponseWaitTime = 0;
            PLC.ActPasswordCancelResponseWaitTime = 0;
            PLC.ActATCommandPasswordCancelRetryTimes = 0;
            PLC.ActCallbackCancelWaitTime = 0;
            PLC.ActCallbackDelayTime = 0;
            PLC.ActCallbackReceptionWaitingTimeOut = 0;
            PLC.ActTargetSimulator = 0;

            int ErrorReturn = PLC.Open();
            if (ErrorReturn == 0)
            {
                Connect_button.Text = "Disconnect";
                LbStatus.Text = "Connected";
                ComConnected = true;
                ErrorViewMessage(ErrorReturn);
            }
            else
            {
                ErrorReturn = PLC.Close();
                Connect_button.Text = "Connect";
                LbStatus.Text = "Disconnected";
                ComConnected = false;
                ErrorViewMessage(ErrorReturn);
            }
        }

        private void ErrorViewMessage(int InCode)
        {
            int lRet;
            string sMessage;
            lRet = axActSupportMsg1.GetErrorMessage(InCode, out sMessage);
            MessageBox.Show(sMessage, "Error Infomation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 7; i++)
            {
                COM.Items.Add("COM " + i.ToString());
            }
        }

        private void COM_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComID = COM.SelectedIndex + 1;
        }

        private void Initialize_button_Click(object sender, EventArgs e)
        {
            //Set speed
            PLC.SetDevice("X40", 1);
            PLC.SetDevice("X40", 0);
            //Zero point return X48: Zero point return
            PLC.SetDevice("X48", 1);
            PLC.SetDevice("X48", 0);
        }


        private void Browse_button_Click(object sender, EventArgs e)    //Open File Dialog
        {
            OpenFileDialog file = new OpenFileDialog
            {
                FileName = "",
                Title = "Open",
                Filter = "All files|*.*|*.gc|*.gc|*.etf|*.etf|*.txt|*.txt|*.GC|*.GC|*.tap|*.tap|*.gcode|*.gcode|*.nc|*.nc"
            };
            DialogResult result = file.ShowDialog();
            comboBox1.Text = file.FileName;
            if (result == DialogResult.OK)
            {
                System.IO.StreamReader OpenFile = new System.IO.StreamReader(file.FileName);
                using (OpenFile)
                {
                    GCodeBox.Text = OpenFile.ReadToEnd();
                }
            }
        }

        private void Open_button_Click(object sender, EventArgs e)  //Open file
        {
            OpenFileDialog file = new OpenFileDialog();
            file.FileName = comboBox1.Text;
            if (file.FileName != "")
            {
                System.IO.StreamReader OpenFile = new System.IO.StreamReader(file.FileName);
                using (OpenFile)
                {
                    GCodeBox.Text = OpenFile.ReadToEnd();
                }
            }
        }

        private void Load_button_Click(object sender, EventArgs e)
        {
            LbStatus.Text = "Busy...";
            Gcode_Process();
            SendBlock600Data(ref PosData1, 1);
            SendBlock600Data(ref PosData2, 2);
            SendBlock600Data(ref PosData3, 3);
            SendBlock600Data(ref PosData4, 4);




            LbStatus.Text = "Done Uploading";
            for (int i = 0; i < GCodeLines.Count; i++)
            {
                string no = "[" + (i + 1).ToString() + "]";
                string pX = " X: " + addressX[i].ToString();
                string pY = " Y: " + addressY[i].ToString();
                string pZ = " Z: " + addressZ[i].ToString();
                //add A axis//
                //string pA = " A: " + addressA[i].ToString();
                string pA = " A: " + gcode[i, 4].ToString();
                string sXY = " speed XY: " + ((int)(speedXY[i] / 100)).ToString();
                string sZ = " speed Z: " + ((int)(speedZ[i] / 100)).ToString();
                //add A axis//
                string sA = " speed A: " + ((int)(speedA[i] / 1000)).ToString();
                string l = " Length: " + ((length[i])).ToString();
                textBox1.Text += no + pX + pY + pZ + pA + sXY + sZ + sA + l;
                //textBox1.Text = string.Join(Environment.NewLine, GCodeLines);
                //textBox1.Text += "[" + (i+1).ToString() + "] ";
                //textBox1.Text += "X:" + addressX[i].ToString() + " ";
                //textBox1.Text += "Y:" + addressY[i].ToString() + " ";
                //textBox1.Text += "Z:" + addressZ[i].ToString() + " ";
                //textBox1.Text += "speedXY:" + ((int)(speedXY[i]/100)).ToString() + " speedZ:" + ((int)(speedZ[i]/100)).ToString();
                //textBox1.Text += "dwellXY:" + (DataX[i].Dwell).ToString() + " dwellZ:" + (DataZ[i].Dwell).ToString();
                textBox1.Text += Environment.NewLine;
            }
        }


        private void SendPosData(ref short[] PosData, short AxisNo, short BlockNo, short NoOfData)
        {
            // if (ComConnected)
            // {
            if (AxisNo == 1)
            {
                PLC.SetDevice("M98", 1);
                if (BlockNo == 1)
                {
                    int ret = PLC.WriteBuffer(0, 1300, NoOfData * 10, ref PosData[0]);
                }
                if (BlockNo > 1)
                {
                    short TargetAxis = 1;
                    int temp = (BlockNo - 1) * 100 + 1;
                    short HeadDataNo = Convert.ToInt16(temp);
                    short WriteRequest = 2;
                    int ret = PLC.WriteBuffer(0, 5110, 1000, ref PosData[0]);
                    ret = PLC.WriteBuffer(0, 5100, 1, ref TargetAxis);
                    ret = PLC.WriteBuffer(0, 5101, 1, ref HeadDataNo);
                    ret = PLC.WriteBuffer(0, 5102, 1, ref NoOfData);
                    ret = PLC.WriteBuffer(0, 5103, 1, ref WriteRequest);
                    while (WriteRequest == 2)
                    {
                        LbStatus.Text = "IN WHILE 1";
                        ret = PLC.ReadBuffer(0, 5103, 1, out WriteRequest);
                    }
                }
                PLC.SetDevice("M98", 0);
            }
            if (AxisNo == 2)
            {
                PLC.SetDevice("M98", 1);
                if (BlockNo == 1)
                {
                    int ret = PLC.WriteBuffer(0, 2300, NoOfData * 10, ref PosData[0]);
                }
                if (BlockNo > 1)
                {
                    short TargetAxis = 2;
                    int temp = (BlockNo - 1) * 100 + 1;
                    short HeadDataNo = Convert.ToInt16(temp);
                    short WriteRequest = 2;
                    int ret = PLC.WriteBuffer(0, 5110, 1000, ref PosData[0]);
                    ret = PLC.WriteBuffer(0, 5100, 1, ref TargetAxis);
                    ret = PLC.WriteBuffer(0, 5101, 1, ref HeadDataNo);
                    ret = PLC.WriteBuffer(0, 5102, 1, ref NoOfData);
                    ret = PLC.WriteBuffer(0, 5103, 1, ref WriteRequest);
                    while (WriteRequest == 2)
                    {
                        LbStatus.Text = "IN WHILE 2";
                        ret = PLC.ReadBuffer(0, 5103, 1, out WriteRequest);
                    }
                }
                PLC.SetDevice("M98", 0);
            }
            if (AxisNo == 3)
            {
                PLC.SetDevice("M99", 1);
                if (BlockNo == 1)
                {
                    int ret = PLC.WriteBuffer(2, 1300, NoOfData * 10, ref PosData[0]);
                }
                if (BlockNo > 1)
                {
                    short TargetAxis = 1;
                    int temp = (BlockNo - 1) * 100 + 1;
                    short HeadDataNo = Convert.ToInt16(temp);
                    short WriteRequest = 2;
                    int ret = PLC.WriteBuffer(2, 5110, 1000, ref PosData[0]);
                    ret = PLC.WriteBuffer(2, 5100, 1, ref TargetAxis);
                    ret = PLC.WriteBuffer(2, 5101, 1, ref HeadDataNo);
                    ret = PLC.WriteBuffer(2, 5102, 1, ref NoOfData);
                    ret = PLC.WriteBuffer(2, 5103, 1, ref WriteRequest);
                    while (WriteRequest == 2)
                    {
                        LbStatus.Text = "IN WHILE 3";
                        ret = PLC.ReadBuffer(2, 5103, 1, out WriteRequest);
                    }
                }
                PLC.SetDevice("M99", 0);
            }
            if (AxisNo == 4)
            {
                PLC.SetDevice("M99", 1);
                if (BlockNo == 1)
                {
                    int ret = PLC.WriteBuffer(2, 2300, NoOfData * 10, ref PosData[0]);
                }
                if (BlockNo > 1)
                {
                    short TargetAxis = 2;
                    int temp = (BlockNo - 1) * 100 + 1;
                    short HeadDataNo = Convert.ToInt16(temp);
                    short WriteRequest = 2;
                    int ret = PLC.WriteBuffer(2, 5110, 1000, ref PosData[0]);
                    ret = PLC.WriteBuffer(2, 5100, 1, ref TargetAxis);
                    ret = PLC.WriteBuffer(2, 5101, 1, ref HeadDataNo);
                    ret = PLC.WriteBuffer(2, 5102, 1, ref NoOfData);
                    ret = PLC.WriteBuffer(2, 5103, 1, ref WriteRequest);
                    while (WriteRequest == 2)
                    {
                        LbStatus.Text = "IN WHILE 4";
                        ret = PLC.ReadBuffer(2, 5103, 1, out WriteRequest);
                    }
                }
                PLC.SetDevice("M99", 0);
            }
            LbStatus.Text = "Wrote to Axis " + AxisNo.ToString() + " Block " + BlockNo.ToString() + " " + NoOfData.ToString() + " Points";
            //}
            /*
            else
            {
                MessageBox.Show("Open Com Port", "Error Infomation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            */
        }

        private void SendStorePosData(ref int[] Posdata, short AxisNo, short StoreBlockNo)
        {
            //  if (ComConnected)
            //   {
            if (AxisNo == 1) // X axis
            {
                string szLabel = "D0";
                if (StoreBlockNo == 1)
                {
                    szLabel = "D0";
                }
                if (StoreBlockNo == 2)
                {
                    szLabel = "D1000";
                }
                if (StoreBlockNo == 3)
                {
                    szLabel = "R0";
                }
                if (StoreBlockNo == 4)
                {
                    szLabel = "R1000";
                }
                int Ret = PLC.WriteDeviceBlock(szLabel, 1000, ref Posdata[0]);
            }
            if (AxisNo == 2) // Y Axis
            {
                string szLabel = "D2000";
                if (StoreBlockNo == 1)
                {
                    szLabel = "D2000";
                }
                if (StoreBlockNo == 2)
                {
                    szLabel = "D3000";
                }
                if (StoreBlockNo == 3)
                {
                    szLabel = "R2000";
                }
                if (StoreBlockNo == 4)
                {
                    szLabel = "R3000";
                }
                int Ret = PLC.WriteDeviceBlock(szLabel, 1000, ref Posdata[0]); // write 
            }
            if (AxisNo == 3)
            {
                string szLabel = "D4000";
                if (StoreBlockNo == 1)
                {
                    szLabel = "D4000";
                }
                if (StoreBlockNo == 2)
                {
                    szLabel = "D5000";
                }
                if (StoreBlockNo == 3)
                {
                    szLabel = "R4000";
                }
                if (StoreBlockNo == 4)
                {
                    szLabel = "R5000";
                }
                int Ret = PLC.WriteDeviceBlock(szLabel, 1000, ref Posdata[0]);
            }
            if (AxisNo == 4)
            {
                string szLabel = "D6000";
                if (StoreBlockNo == 1)
                {
                    szLabel = "D6000";
                }
                if (StoreBlockNo == 2)
                {
                    szLabel = "D7000";
                }
                if (StoreBlockNo == 3)
                {
                    szLabel = "R6000";
                }
                if (StoreBlockNo == 4)
                {
                    szLabel = "R7000";
                }
                int Ret = PLC.WriteDeviceBlock(szLabel, 1000, ref Posdata[0]);
            }
            //  }
        }

        private void SendBlock600Data(ref short[] ArrayPosData, short AxisNo)
        {
            short[] DataSend1 = new short[1000];
            short[] DataSend2 = new short[1000];
            short[] DataSend3 = new short[1000];
            short[] DataSend4 = new short[1000];
            short[] DataSend5 = new short[1000];
            short[] DataSend6 = new short[1000];
            int[] DataSend7 = new int[1000];
            int[] DataSend8 = new int[1000];
            int[] DataSend9 = new int[1000];
            int[] DataSend10 = new int[1000];
            for (int i = 0; i < ArrayPosData.Length; i++)
            {
                if (i >= 0 && i < 1000)
                {
                    DataSend1[i] = ArrayPosData[i];
                }
                if (i >= 1000 && i < 2000)
                {
                    DataSend2[i - 1000] = ArrayPosData[i];
                }
                if (i >= 2000 && i < 3000)
                {
                    DataSend3[i - 2000] = ArrayPosData[i];
                }
                if (i >= 3000 && i < 4000)
                {
                    DataSend4[i - 3000] = ArrayPosData[i];
                }
                if (i >= 4000 && i < 5000)
                {
                    DataSend5[i - 4000] = ArrayPosData[i];
                }
                if (i >= 5000 && i < 6000)
                {
                    DataSend6[i - 5000] = ArrayPosData[i];
                }
                if (i >= 6000 && i < 7000)
                {
                    DataSend7[i - 6000] = ArrayPosData[i];
                }
                if (i >= 7000 && i < 8000)
                {
                    DataSend8[i - 7000] = ArrayPosData[i];
                }
                if (i >= 8000 && i < 9000)
                {
                    DataSend9[i - 8000] = ArrayPosData[i];
                }
                if (i >= 9000 && i < 10000)
                {
                    DataSend10[i - 9000] = ArrayPosData[i];
                }
            }
            if (ArrayPosData.Length >= 0) SendPosData(ref DataSend1, AxisNo, 1, 100);
            if (ArrayPosData.Length >= 1000) SendPosData(ref DataSend2, AxisNo, 2, 100);
            if (ArrayPosData.Length >= 2000) SendPosData(ref DataSend3, AxisNo, 3, 100);
            if (ArrayPosData.Length >= 3000) SendPosData(ref DataSend4, AxisNo, 4, 100);
            if (ArrayPosData.Length >= 4000) SendPosData(ref DataSend5, AxisNo, 5, 100);
            if (ArrayPosData.Length >= 5000) SendPosData(ref DataSend6, AxisNo, 6, 100);
            if (ArrayPosData.Length >= 6000) SendStorePosData(ref DataSend7, AxisNo, 1);
            if (ArrayPosData.Length >= 7000) SendStorePosData(ref DataSend8, AxisNo, 2);
            if (ArrayPosData.Length >= 8000) SendStorePosData(ref DataSend9, AxisNo, 3);
            if (ArrayPosData.Length >= 9000) SendStorePosData(ref DataSend10, AxisNo, 4);
        }


        //Read Full coordinates
        public static double[,] gcode;
        public static bool[] Arc_XY;
        public static double[] inc_a;
        public static double[] abs_a;
        //gcode[ , 0]: G value
        //gcode[ , 1]: X value
        //gcode[ , 2]: Y value
        //gcode[ , 3]: Z value
        //gcode[ , 4]: A value
        //gcode[ , 5]: I value
        //gcode[ , 6]: J value
        private void Gcode_Parse() // Read Gcode box
        {
            GCodeLines.Clear();
            string[] lines = GCodeBox.Lines;
            inc_a = new double[lines.Length + 1];
            abs_a = new double[lines.Length + 1];
            gcode = new double[lines.Length + 1, 7];
            Arc_XY = new bool[lines.Length + 1];
            int p = 0;
            gcode[0, 0] = 1;
            gcode[0, 1] = 0;
            gcode[0, 2] = 0;
            gcode[0, 3] = 0;
            gcode[0, 4] = 0; //a
            gcode[0, 5] = 0;
            gcode[0, 6] = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                int j = 0;
                int g = 0, x = 0, y = 0, z = 0, a = 0, r = 0; //check availability
                double gv = 1, xv = 0, yv = 0, zv = 0, av = 0, iv = 0, jv = 0; //values
                double av1 = 0;
                double av2 = 0;
                if (lines[i] == "") continue;
                if (lines[i][0] == ';') continue;
                GCodeLines.Add(lines[i]); // Add to list.
                //Read Gcode
                while (j < lines[i].Length)
                {
                    if (lines[i][j] == 'G')
                    {
                        g = 1;
                        string temp = "";
                        while (lines[i][j] != ' ' && j + 1 < lines[i].Length && lines[i][j + 1] != ';')
                        {
                            j++;
                            temp += lines[i][j];
                        }
                        gv = double.Parse(temp);
                    }
                    if (lines[i][j] == 'X')
                    {
                        x = 1;
                        string temp = "";
                        while (lines[i][j] != ' ' && j + 1 < lines[i].Length && lines[i][j + 1] != ';')
                        {
                            j++;
                            temp += lines[i][j];
                        }
                        xv = double.Parse(temp);
                    }
                    if (lines[i][j] == 'Y')
                    {
                        y = 1;
                        string temp = "";
                        while (lines[i][j] != ' ' && j + 1 < lines[i].Length && lines[i][j + 1] != ';')
                        {
                            j++;
                            temp += lines[i][j];
                        }
                        yv = double.Parse(temp);
                    }
                    if (lines[i][j] == 'Z')
                    {
                        z = 1;
                        string temp = "";
                        while (lines[i][j] != ' ' && j + 1 < lines[i].Length && lines[i][j + 1] != ';')
                        {
                            j++;
                            temp += lines[i][j];
                        }
                        zv = double.Parse(temp);
                    }
                    if (lines[i][j] == 'A')
                    {
                        a = 1;
                        string temp = "";
                        while (lines[i][j] != ' ' && j + 1 < lines[i].Length && lines[i][j + 1] != ';')
                        {
                            j++;
                            temp += lines[i][j];
                        }
                        //New edit   
                        av1 = double.Parse(temp);
                        av = av1 * 9;
                    }

                    if (lines[i][j] == 'I')
                    {
                        r = 1;
                        string temp = "";
                        while (lines[i][j] != ' ' && j + 1 < lines[i].Length && lines[i][j + 1] != ';')
                        {
                            j++;
                            temp += lines[i][j];
                        }
                        iv = double.Parse(temp);
                    }
                    if (lines[i][j] == 'J')
                    {
                        r = 1;
                        string temp = "";
                        while (lines[i][j] != ' ' && j + 1 < lines[i].Length && lines[i][j + 1] != ';')
                        {
                            j++;
                            temp += lines[i][j];
                        }
                        jv = double.Parse(temp);
                    }
                    j++;
                }

                {
                    if (g == 0)
                    {
                        if (p == 0) gv = gcode[p, 0];
                        else gv = gcode[p - 1, 0];
                    }
                    if (x == 0)
                    {
                        if (p == 0) xv = gcode[p, 1];
                        else xv = gcode[p - 1, 1];
                    }
                    if (y == 0)
                    {
                        if (p == 0) yv = gcode[p, 2];
                        else yv = gcode[p - 1, 2];
                    }
                    if (z == 0)
                    {
                        if (p == 0) zv = gcode[p, 3];
                        else zv = gcode[p - 1, 3];
                    }
                    if (a == 0)
                    {
                        if (p == 0) av = gcode[p, 4];
                        else av = gcode[p - 1, 4];
                    }
                    if (r == 0)
                    {
                        iv = 0;
                        jv = 0;
                    }
                    else Arc_XY[p] = true;
                }

                if (x == 1 || y == 1 || z == 1 || a == 1)
                {
                    gcode[p, 0] = gv;
                    gcode[p, 1] = xv;
                    gcode[p, 2] = yv;
                    gcode[p, 3] = zv;
                    gcode[p, 4] = av;
                    gcode[p, 5] = iv;
                    gcode[p, 6] = jv;
                }
                p++;
            }
            for (int i = 0; i < lines.Length; i++)
            {
                abs_a[i] = gcode[i, 4];
            }
            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 0) inc_a[i] = abs_a[i];
                else
                {
                    inc_a[i] = abs_a[i] - abs_a[i - 1];
                }
            }
        }
        //..

        //Calculations
        private double CurveLength2(double X1, double Y1, double X2, double Y2, double I, double J)
        {
            double a = ((X1 - I) * (X2 - I)) + ((Y1 - J) * (Y2 - J));
            double b = Math.Sqrt((X1 - I) * (X1 - I) + (Y1 - J) * (Y1 - J)) * Math.Sqrt((X2 - I) * (X2 - I) + (Y2 - J) * (Y2 - J));
            double angle = Math.Acos(a / b);
            double d = angle * Math.Sqrt(((X1 - I) * (X1 - I)) + ((Y1 - J) * (Y1 - J)));
            return d;
        }

        private double Distance(double A1, double A2, double B1, double B2)
        {
            double dA = Math.Abs(A2 - A1);
            double dB = Math.Abs(B2 - B1);
            return Math.Sqrt(dA * dA + dB * dB);
        }
        private void getVelXY(double velZ, double Z1, double Z2, double X1, double X2, double Y1, double Y2, out double velXY) //Velocity of XY based on Z vel
        {
            double dx = Math.Abs(X2 - X1);
            double dy = Math.Abs(Y2 - Y1);
            double dz = Math.Abs(Z2 - Z1);
            double dxy = Math.Sqrt(dx * dx + dy * dy);
            velXY = (dxy * velZ) / dz;
        }

        public bool is4thaxis = false;
        //..
        public double[] speedXY, speedZ, speedA;

        private void checkA_CheckedChanged(object sender, EventArgs e)
        {
            is4thaxis = true;
        }



        public double speed_sp = 0;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
            speedLB.Text = trackBar1.Value.ToString() + "  (RPM)";
            speed_sp = trackBar1.Value;
            nRunBtn.Enabled = true;
            if(speed_sp == 22000)
            {
                PLC.SetDevice("M100", 1);
              //  PLC.SetDevice("M100", 0);
                PLC.SetDevice("M101", 0);
                PLC.SetDevice("M102", 0);
                PLC.SetDevice("M103", 0);
                PLC.SetDevice("M104", 0);
                PLC.SetDevice("M105", 0);
                PLC.SetDevice("M106", 0);
                nRunBtn.Enabled = true;
            }
            else if(speed_sp == 20000)
            {
                PLC.SetDevice("M104", 1);
              //  PLC.SetDevice("M104", 0);
                PLC.SetDevice("M100", 0);
                PLC.SetDevice("M101", 0);
                PLC.SetDevice("M102", 0);
                PLC.SetDevice("M103", 0);
                PLC.SetDevice("M105", 0);
                PLC.SetDevice("M106", 0);
                nRunBtn.Enabled = true;
            }
            else if (speed_sp == 18000)
            {
                PLC.SetDevice("M105", 1);
            //    PLC.SetDevice("M105", 0);
                PLC.SetDevice("M100", 0);
                PLC.SetDevice("M101", 0);
                PLC.SetDevice("M102", 0);
                PLC.SetDevice("M103", 0);
                PLC.SetDevice("M104", 0);
                PLC.SetDevice("M106", 0);
                nRunBtn.Enabled = true;
            }
            else if (speed_sp == 16000)
            {
                PLC.SetDevice("M101", 1);
            //    PLC.SetDevice("M101", 0);
                PLC.SetDevice("M100", 0);
                PLC.SetDevice("M102", 0);
                PLC.SetDevice("M103", 0);
                PLC.SetDevice("M104", 0);
                PLC.SetDevice("M105", 0);
                PLC.SetDevice("M106", 0);
                nRunBtn.Enabled = true;
            }
            else if (speed_sp == 14000)
            {
                PLC.SetDevice("M103", 1);
            //    PLC.SetDevice("M103", 0);
                PLC.SetDevice("M100", 0);
                PLC.SetDevice("M101", 0);
                PLC.SetDevice("M102", 0);
                PLC.SetDevice("M104", 0);
                PLC.SetDevice("M105", 0);
                PLC.SetDevice("M106", 0);
                nRunBtn.Enabled = true;
            }
            else if (speed_sp == 12000)
            {
                PLC.SetDevice("M102", 1);
            //    PLC.SetDevice("M102", 0);
                PLC.SetDevice("M100", 0);
                PLC.SetDevice("M101", 0);
                PLC.SetDevice("M103", 0);
                PLC.SetDevice("M104", 0);
                PLC.SetDevice("M105", 0);
                PLC.SetDevice("M106", 0);
                nRunBtn.Enabled = true;
            }
            else if (speed_sp == 10000)
            {
                PLC.SetDevice("M106", 1);
             //   PLC.SetDevice("M106", 0);
                PLC.SetDevice("M100", 0);
                PLC.SetDevice("M101", 0);
                PLC.SetDevice("M102", 0);
                PLC.SetDevice("M103", 0);
                PLC.SetDevice("M104", 0);
                PLC.SetDevice("M105", 0);
                nRunBtn.Enabled = true;
            }
            else if (speed_sp == 8000)
            {
                speedLB.Text = " 0 (RPM)";
            }
        }

        private void nRunBtn_Click(object sender, EventArgs e)
        {
            if(speed_sp != 0)
            {
                PLC.SetDevice("M109", 1);
                PLC.SetDevice("M109", 0);
                SpindleStt.Text = "RUNNING";
                SpindleStt.ForeColor = System.Drawing.Color.Lime;
            }
            else
            {
                MessageBox.Show("Error: No Speed Mode Selection");
            }
        }

        private void nStopBtn_Click(object sender, EventArgs e)
        {
            PLC.SetDevice("M110", 1);
            PLC.SetDevice("M110", 0);
            nRunBtn.Enabled = true;
            SpindleStt.Text = "STOP";
            SpindleStt.ForeColor = System.Drawing.Color.Red;
        }

        public double[] addressX, addressY, addressZ, addressA, addressI, addressJ;
        public double[] length;
        public double[] test_dwellX;
        private void Gcode_Process()
        {
            Gcode_Parse();
            Count = GCodeLines.Count;
            DataX = new PointData[Count];
            DataY = new PointData[Count];
            DataZ = new PointData[Count];
            DataA = new PointData[Count];
            const double SpeedXY_const = 150000;    // x10^-2 (mm/min)
            const double SpeedZ_const = 15000;     // x10^-2 (mm/min)
            const double SpeedA_const = 540000;   // x10^-3 (degree/min)
            double SpeedXY = new double();
            double SpeedX = new double();
            double SpeedY = new double();
            double SpeedZ = new double();
            double SpeedA = new double();
            double dwellXY = new double();
            double dwellX = new double();
            double dwellY = new double();
            double dwellZ = new double();
            double dwellA = new double();
            short ident1_g01 = 0x101; //1 axis G01 gcode
            short ident2_g01 = 0x401; //2-axis G01 gcode
            short ident3_g01 = 0x201; //2-axis feed dimension
            short ident2_g02 = 0x901; //2-axis G02 gcode
            short ident2_g03 = 0xA01; //2-axis G03 gcode
            speedXY = new double[Count];
            speedZ = new double[Count];
            speedA = new double[Count];
            length = new double[Count];
            addressX = new double[Count];   // (mm)
            addressY = new double[Count];
            addressZ = new double[Count];   // (degree)
            addressA = new double[Count];
            addressI = new double[Count];
            addressJ = new double[Count];
            test_dwellX = new double[Count];
            //short dwell0 = 0;
            short dwell = 100;
            if (Count >= 1)
            {
                if (is4thaxis == false)
                {
                    //First point declaration
                    addressX[0] = gcode[0, 1]; //xv value
                    IntTo2Short((int)(addressX[0] * 10000), out short AddX1, out short AddX2);
                    addressY[0] = gcode[0, 2]; //yv value
                    IntTo2Short((int)(addressY[0] * 10000), out short AddY1, out short AddY2);
                    addressZ[0] = gcode[0, 3]; //zv value
                    IntTo2Short((int)(addressZ[0] * 10000), out short AddZ1, out short AddZ2);
                    //change to inc_a
                    //old
                    //addressA[0] = gcode[0, 4]; //av value
                    //new
                    addressA[0] = inc_a[0];
                    IntTo2Short((int)(addressA[0] * 100000), out short AddA1, out short AddA2);
                    addressI[0] = gcode[0, 5]; // iv value
                    IntTo2Short((int)(addressI[0] * 10000), out short AddI1, out short AddI2);
                    addressJ[0] = gcode[0, 6]; //jv value
                    IntTo2Short((int)(addressJ[0] * 10000), out short AddJ1, out short AddJ2);

                    DataX[0].Iden = ident2_g01;
                    DataA[0].Iden = ident3_g01;
                    DataZ[0].Iden = ident1_g01;
                    DataX[0].MCode = 0; DataY[0].MCode = 0; DataZ[0].MCode = 0; DataA[0].MCode = 0;

                    if (Arc_XY[0] == true && gcode[0, 0] == 2)
                    {
                        DataX[0].Iden = ident2_g02;// DataY[0].Iden = ident2_g02;
                    }
                    if (Arc_XY[0] == true && gcode[0, 0] == 3)
                    {
                        DataX[0].Iden = ident2_g03;// DataY[0].Iden = ident2_g03;
                    }
                    //Speed
                    // edited: add value speed A
                    if (gcode[0, 0] == 0)
                    {
                        SpeedXY = SpeedXY_const * 4;
                        SpeedZ = SpeedZ_const * 2;
                        SpeedA = SpeedA_const;
                    }
                    if (gcode[0, 0] == 1 || gcode[0, 0] == 2 || gcode[0, 0] == 3)
                    {
                        SpeedXY = SpeedXY_const;
                        SpeedZ = SpeedZ_const;
                        SpeedA = SpeedA_const;
                    }
                    if (addressZ[0] == 0) //Condition 1: Z doesn't change
                    {
                        speedZ[0] = 1;
                        DataZ[0].Speed1 = 1;
                        DataZ[0].Speed2 = 1;
                        speedXY[0] = SpeedXY;
                        IntTo2Short((int)speedXY[0], out short speedxy1, out short speedxy2);
                        DataX[0].Speed1 = speedxy1;
                        DataX[0].Speed2 = speedxy2;
                        DataY[0].Speed1 = speedxy1;
                        DataY[0].Speed2 = speedxy2;
                    }

                    else //Condition 2: Z changes
                    {
                        speedZ[0] = SpeedZ;
                        getVelXY(speedZ[0], 0, addressZ[0], 0, addressX[0], 0, addressY[0], out double vxy);
                        if (vxy == 0) vxy = 1;
                        speedXY[0] = vxy;
                        IntTo2Short((int)speedXY[0], out short speedxy1, out short speedxy2);
                        IntTo2Short((int)speedZ[0], out short speedz1, out short speedz2);
                        DataX[0].Speed1 = speedxy1;
                        DataX[0].Speed2 = speedxy2;
                        DataY[0].Speed1 = speedxy1;
                        DataY[0].Speed2 = speedxy2;
                        DataZ[0].Speed1 = speedz1;
                        DataZ[0].Speed2 = speedz2;
                    }

                    // add setting for A-axis
                    if (addressA[0] == 0) //Condition 1: A doesn't change
                    {
                        speedA[0] = 1;
                        DataA[0].Speed1 = 1;
                        DataA[0].Speed2 = 1;
                        speedXY[0] = SpeedXY;
                        IntTo2Short((int)speedXY[0], out short speedxy1, out short speedxy2);
                        DataX[0].Speed1 = speedxy1;
                        DataX[0].Speed2 = speedxy2;
                        DataY[0].Speed1 = speedxy1;
                        DataY[0].Speed2 = speedxy2;
                    }
                    else //Condition 2: A changes
                    {
                        speedA[0] = SpeedA;
                        getVelXY(speedA[0], 0, addressA[0], 0, addressX[0], 0, addressY[0], out double vxy);
                        if (vxy == 0) vxy = 1;
                        speedXY[0] = vxy;
                        IntTo2Short((int)speedXY[0], out short speedxy1, out short speedxy2);
                        IntTo2Short((int)speedA[0], out short speeda1, out short speeda2);
                        DataX[0].Speed1 = speedxy1;
                        DataX[0].Speed2 = speedxy2;
                        DataY[0].Speed1 = speedxy1;
                        DataY[0].Speed2 = speedxy2;
                        DataA[0].Speed1 = speeda1;
                        DataA[0].Speed2 = speeda2;
                    }

                    //Dwell
                    DataX[0].Dwell = dwell; DataY[0].Dwell = dwell; DataZ[0].Dwell = dwell; DataA[0].Dwell = dwell;
                    dwellZ = 200; dwellXY = 0; dwellA = 800;
                    if (addressZ[0] == 0) //Condition 1: Z doesn't change
                    {
                        double dxy = Distance(0, addressX[0], 0, addressY[0]);
                        dwellZ = 1000 * (dxy * 100) / (speedXY[0] / 60);
                    }
                    if (addressX[0] == 0 && addressY[0] == 0) //Condition 2: XY don't change
                    {
                        double dz = Math.Abs(0 - addressZ[0]);
                        dwellXY = 1000 * (dz * 100) / (speedZ[0] / 60);
                    }
                    //add Dwell A
                    if (addressA[0] == 0) //Condition 1: A doesn't change
                    {
                        double dxy = Distance(0, addressX[0], 0, addressY[0]);
                        dwellA = 4000 * (dxy * 100) / (speedXY[0] / 60);
                    }
                    if (addressX[0] == 0 && addressY[0] == 0) //Condition 2: XY don't change
                    {
                        double dA = Math.Abs(0 - addressA[0]);
                        dwellXY = 4000 * (dA * 100) / (speedA[0] / 60);
                    }
                    DataX[0].Dwell += (short)dwellXY;
                    DataY[0].Dwell += (short)dwellXY;
                    DataZ[0].Dwell += (short)dwellZ;
                    DataA[0].Dwell += (short)dwellA;

                    DataX[0].Address1 = AddX1; DataY[0].Address1 = AddY1; DataZ[0].Address1 = AddZ1; DataA[0].Address1 = AddA1;
                    DataX[0].Address2 = AddX2; DataY[0].Address2 = AddY2; DataZ[0].Address2 = AddZ2; DataA[0].Address2 = AddA2;
                    DataX[0].Arc1 = AddI1; DataY[0].Arc1 = AddJ1; DataZ[0].Arc1 = 0; DataA[0].Arc1 = 0;
                    DataX[0].Arc2 = AddI2; DataY[0].Arc2 = AddJ2; DataZ[0].Arc2 = 0; DataA[0].Arc2 = 0;

                    //next line

                    for (int i = 1; i < Count; ++i)
                    {
                        addressX[i] = gcode[i, 1];
                        addressY[i] = gcode[i, 2];
                        addressZ[i] = gcode[i, 3];
                        //change address A = to inc
                        //old
                        // addressA[i] = gcode[i, 4];
                        //new
                        addressA[i] = inc_a[i];
                        addressI[i] = gcode[i, 5];
                        addressJ[i] = gcode[i, 6];

                        //--Data 0: Positioning Identifier
                        //int IdenXY;
                        if (Arc_XY[i] == true && gcode[i, 0] == 2)  //G02
                        {
                            DataX[i].Iden = ident2_g02;
                            DataA[i].Iden = ident3_g01;
                            DataZ[i].Iden = ident1_g01;
                        }
                        else
                        if (Arc_XY[i] == true && gcode[i, 0] == 3)  //G03
                        {
                            DataX[i].Iden = ident2_g03;
                            DataA[i].Iden = ident3_g01;
                            DataZ[i].Iden = ident1_g01;

                        }
                        else
                        {
                            DataX[i].Iden = ident2_g01;
                            DataA[i].Iden = ident3_g01;
                            DataZ[i].Iden = ident1_g01;
                        }
                        //..
                        //--Data 9: M code
                        DataX[i].MCode = 0;
                        DataY[i].MCode = 0;
                        DataZ[i].MCode = 0;
                        DataA[i].MCode = 0;
                        //..
                        //--Data 7: Command speed
                        if (gcode[i, 0] == 0)
                        {
                            SpeedXY = SpeedXY_const * 4;
                            SpeedZ = SpeedZ_const * 2;
                            SpeedA = SpeedA_const;
                        }
                        if (gcode[i, 0] == 1 || gcode[i, 0] == 2 || gcode[i, 0] == 3)
                        {

                            SpeedXY = SpeedXY_const / 6;
                            SpeedZ = SpeedZ_const / 4;
                            SpeedA = SpeedA_const / 1;
                        }
                        if (addressZ[i] == addressZ[i - 1]) //Condition 1: Z doesn't change
                        {
                            speedZ[i] = 1;
                            DataZ[i].Speed1 = 1;
                            DataZ[i].Speed2 = 1;
                            speedXY[i] = SpeedXY;
                            IntTo2Short((int)speedXY[i], out short speedxy1, out short speedxy2);
                            DataX[i].Speed1 = speedxy1;
                            DataX[i].Speed2 = speedxy2;
                            DataY[i].Speed1 = speedxy1;
                            DataY[i].Speed2 = speedxy2;
                        }
                        else //Condition 2: Z changes
                        {
                            speedZ[i] = SpeedZ;
                            getVelXY(speedZ[i], addressZ[i - 1], addressZ[i], addressX[i - 1], addressX[i], addressY[i - 1], addressY[i], out double vxy);
                            if (vxy == 0) vxy = 1;
                            speedXY[i] = vxy;
                            IntTo2Short((int)speedXY[i], out short speedxy1, out short speedxy2);
                            IntTo2Short((int)speedZ[i], out short speedz1, out short speedz2);
                            DataX[i].Speed1 = speedxy1;
                            DataX[i].Speed2 = speedxy2;
                            DataY[i].Speed1 = speedxy1;
                            DataY[i].Speed2 = speedxy2;
                            DataZ[i].Speed1 = speedz1;
                            DataZ[i].Speed2 = speedz2;
                        }
                        //Add A-axis Condition 
                        if (addressA[i] == addressA[i - 1]) //Condition 1: A doesn't change
                        {
                            speedA[i] = 1;
                            DataA[i].Speed1 = 1;
                            DataA[i].Speed2 = 1;
                            //   speedXY[i] = SpeedXY;
                            //   IntTo2Short((int)speedXY[i], out short speedxy1, out short speedxy2);
                            //   DataX[i].Speed1 = speedxy1;
                            //   DataX[i].Speed2 = speedxy2;
                            //   DataY[i].Speed1 = speedxy1;
                            //   DataY[i].Speed2 = speedxy2;

                        }
                        else //Condition 2: A changes
                        {
                            speedA[i] = SpeedA;
                            getVelXY(speedA[i], addressA[i - 1], addressA[i], addressX[i - 1], addressX[i], addressY[i - 1], addressY[i], out double vxy);
                            if (vxy == 0) vxy = 1;
                            speedXY[i] = vxy;
                            IntTo2Short((int)speedXY[i], out short speedxy1, out short speedxy2);
                            IntTo2Short((int)speedA[i], out short speeda1, out short speeda2);
                            DataX[i].Speed1 = speedxy1;
                            DataX[i].Speed2 = speedxy2;
                            DataY[i].Speed1 = speedxy1;
                            DataY[i].Speed2 = speedxy2;
                            DataA[i].Speed1 = speeda1;
                            DataA[i].Speed2 = speeda2;

                        }


                        //..
                        //--Data 8: Dwell time
                        DataX[i].Dwell = dwell;
                        DataY[i].Dwell = dwell;
                        DataZ[i].Dwell = dwell;
                        DataA[i].Dwell = dwell;
                        dwellZ = 200; dwellXY = 0;
                        if (addressZ[i] == addressZ[i - 1]) //Codition 1: Z doesn't change
                        {
                            double dxy;
                            if (gcode[i, 0] == 2) dxy = CurveLength2(addressX[i - 1], addressY[i - 1], addressX[i], addressY[i], addressI[i], addressJ[i]);
                            else if (gcode[i, 0] == 3) dxy = CurveLength2(addressX[i - 1], addressY[i - 1], addressX[i], addressY[i], addressI[i], addressJ[i]);
                            else dxy = Distance(addressX[i - 1], addressX[i], addressY[i - 1], addressY[i]);
                            length[i] = dxy;
                            dwellZ = 1000 * (dxy * 100) / (speedXY[i] / 60);
                        }
                        if (addressX[i] == addressX[i - 1] && addressY[i] == addressY[i - 1]) //Codition 2: XY don't change
                        {
                            double dz = Math.Abs(addressZ[i - 1] - addressZ[i]);
                            length[i] = dz;
                            dwellXY = 1000 * (dz * 100) / (speedZ[i] / 60);
                        }

                        if (addressA[i] == addressA[i - 1]) //Codition 1: A doesn't change
                        {
                            double dxy;
                            if (gcode[i, 0] == 2) dxy = CurveLength2(addressX[i - 1], addressY[i - 1], addressX[i], addressY[i], addressI[i], addressJ[i]);
                            else if (gcode[i, 0] == 3) dxy = CurveLength2(addressX[i - 1], addressY[i - 1], addressX[i], addressY[i], addressI[i], addressJ[i]);
                            else dxy = Distance(addressX[i - 1], addressX[i], addressY[i - 1], addressY[i]);
                            length[i] = dxy;
                            dwellA = 4000 * (dxy * 100) / (speedXY[i] / 60);
                        }
                        /*
                        if (addressX[i] == addressX[i - 1] && addressY[i] == addressY[i - 1]) //Codition 2: XY don't change
                        {
                            double da = Math.Abs(addressA[i - 1] - addressA[i]);
                            length[i] = da;
                            dwellXY = 1000 * (da * 100) / (speedA[i] / 60);
                        }
                        */

                        DataX[i].Dwell += (short)dwellXY;
                        DataY[i].Dwell += (short)dwellXY;
                        DataZ[i].Dwell += (short)dwellZ;
                        DataA[i].Dwell += (short)dwellA;
                        //..
                        //--Data 5: Positioning Address/movement amount
                        IntTo2Short((int)(addressX[i] * 10000), out short AddressX1, out short AddressX2);
                        DataX[i].Address1 = AddressX1;
                        DataX[i].Address2 = AddressX2;
                        IntTo2Short((int)(addressY[i] * 10000), out short AddressY1, out short AddressY2);
                        DataY[i].Address1 = AddressY1;
                        DataY[i].Address2 = AddressY2;
                        IntTo2Short((int)(addressZ[i] * 10000), out short AddressZ1, out short AddressZ2);
                        DataZ[i].Address1 = AddressZ1;
                        DataZ[i].Address2 = AddressZ2;
                        //edit 10000 -> 100000
                        IntTo2Short((int)(addressA[i] * 100000), out short AddressA1, out short AddressA2);
                        DataA[i].Address1 = AddressA1;
                        DataA[i].Address2 = AddressA2;
                        //..
                        //--Data 6 Arc Address
                        IntTo2Short((int)(addressI[i] * 10000), out short AddressI1, out short AddressI2);
                        IntTo2Short((int)(addressJ[i] * 10000), out short AddressJ1, out short AddressJ2);
                        DataX[i].Arc1 = AddressI1;
                        DataX[i].Arc2 = AddressI2;
                        DataY[i].Arc1 = AddressJ1;
                        DataY[i].Arc2 = AddressJ2;
                        DataZ[i].Arc1 = 0;
                        DataZ[i].Arc2 = 0;
                        DataA[i].Arc1 = 0;
                        DataA[i].Arc2 = 0;
                        //..


                        //..
                    }

                    DataX[Count - 1].Iden = 0x400; // = 0100 0000 0000
                    DataY[Count - 1].Iden = 0x400;
                    DataA[Count - 1].Iden = 0x300;
                    DataZ[Count - 1].Iden = 0x100; // = 0001 0000 0000


                    ListPosData1.Clear();
                    ListPosData2.Clear();
                    ListPosData3.Clear();
                    ListPosData4.Clear();
                    for (int i = 0; i < Count; i++)
                    {
                        ListPosData1.Add(DataX[i].Iden);
                        ListPosData1.Add(DataX[i].MCode);
                        ListPosData1.Add(DataX[i].Dwell);
                        ListPosData1.Add(DataX[i].Dummy);
                        ListPosData1.Add(DataX[i].Speed1);
                        ListPosData1.Add(DataX[i].Speed2);
                        ListPosData1.Add(DataX[i].Address1);
                        ListPosData1.Add(DataX[i].Address2);
                        ListPosData1.Add(DataX[i].Arc1);
                        ListPosData1.Add(DataX[i].Arc2);

                        ListPosData2.Add(DataY[i].Iden);
                        ListPosData2.Add(DataY[i].MCode);
                        ListPosData2.Add(DataY[i].Dwell);
                        ListPosData2.Add(DataY[i].Dummy);
                        ListPosData2.Add(DataY[i].Speed1);
                        ListPosData2.Add(DataY[i].Speed2);
                        ListPosData2.Add(DataY[i].Address1);
                        ListPosData2.Add(DataY[i].Address2);
                        ListPosData2.Add(DataY[i].Arc1);
                        ListPosData2.Add(DataY[i].Arc2);

                        ListPosData3.Add(DataZ[i].Iden);
                        ListPosData3.Add(DataZ[i].MCode);
                        ListPosData3.Add(DataZ[i].Dwell);
                        ListPosData3.Add(DataZ[i].Dummy);
                        ListPosData3.Add(DataZ[i].Speed1);
                        ListPosData3.Add(DataZ[i].Speed2);
                        ListPosData3.Add(DataZ[i].Address1);
                        ListPosData3.Add(DataZ[i].Address2);
                        ListPosData3.Add(DataZ[i].Arc1);
                        ListPosData3.Add(DataZ[i].Arc2);

                        ListPosData4.Add(DataA[i].Iden);
                        ListPosData4.Add(DataA[i].MCode);
                        ListPosData4.Add(DataA[i].Dwell);
                        ListPosData4.Add(DataA[i].Dummy);
                        ListPosData4.Add(DataA[i].Speed1);
                        ListPosData4.Add(DataA[i].Speed2);
                        ListPosData4.Add(DataA[i].Address1);
                        ListPosData4.Add(DataA[i].Address2);
                        ListPosData4.Add(DataA[i].Arc1);
                        ListPosData4.Add(DataA[i].Arc2);
                    }
                    PosData1 = null;
                    PosData2 = null;
                    PosData3 = null;
                    PosData4 = null;
                    PosData1 = ListPosData1.ToArray();
                    PosData2 = ListPosData2.ToArray();
                    PosData3 = ListPosData3.ToArray();
                    PosData4 = ListPosData4.ToArray();
                }

                else
                {
                    //First point declaration
                    addressX[0] = gcode[0, 1]; //xv value
                    IntTo2Short((int)(addressX[0] * 10000), out short AddX1, out short AddX2);
                    addressY[0] = gcode[0, 2]; //yv value
                    IntTo2Short((int)(addressY[0] * 10000), out short AddY1, out short AddY2);
                    addressZ[0] = gcode[0, 3]; //zv value
                    IntTo2Short((int)(addressZ[0] * 10000), out short AddZ1, out short AddZ2);
                    //change A
                    addressA[0] = inc_a[0]; //av value
                    IntTo2Short((int)(addressA[0] * 100000), out short AddA1, out short AddA2);
                    addressI[0] = gcode[0, 5]; // iv value
                    IntTo2Short((int)(addressI[0] * 10000), out short AddI1, out short AddI2);
                    addressJ[0] = gcode[0, 6]; //jv value
                    IntTo2Short((int)(addressJ[0] * 10000), out short AddJ1, out short AddJ2);

                    DataX[0].Iden = ident2_g01;
                    DataA[0].Iden = ident3_g01;
                    DataZ[0].Iden = ident1_g01;
                    DataX[0].MCode = 0; DataY[0].MCode = 0; DataZ[0].MCode = 0; DataA[0].MCode = 0;

                    if (Arc_XY[0] == true && gcode[0, 0] == 2)
                    {
                        DataX[0].Iden = ident2_g02;// DataY[0].Iden = ident2_g02;
                    }
                    if (Arc_XY[0] == true && gcode[0, 0] == 3)
                    {
                        DataX[0].Iden = ident2_g03;// DataY[0].Iden = ident2_g03;
                    }
                    //Speed
                    // edited: add value speed A
                    if (gcode[0, 0] == 0)
                    {
                        SpeedXY = SpeedXY_const * 4;
                        SpeedZ = SpeedZ_const * 2;
                        SpeedA = SpeedA_const;
                    }
                    if (gcode[0, 0] == 1 || gcode[0, 0] == 2 || gcode[0, 0] == 3)
                    {
                        SpeedXY = SpeedXY_const * 2;
                        SpeedZ = SpeedZ_const;
                        SpeedA = SpeedA_const;
                    }
                    if (addressZ[0] == 0 && addressA[0] == 0) // only move XY
                    {
                        speedZ[0] = 1;
                        DataZ[0].Speed1 = 1;
                        DataZ[0].Speed2 = 1;
                        speedA[0] = 1;
                        DataA[0].Speed1 = 1;
                        DataA[0].Speed2 = 1;
                        speedXY[0] = SpeedXY;
                        IntTo2Short((int)speedXY[0], out short speedxy1, out short speedxy2);
                        DataX[0].Speed1 = speedxy1;
                        DataX[0].Speed2 = speedxy2;
                        DataY[0].Speed1 = speedxy1;
                        DataY[0].Speed2 = speedxy2;
                    }
                    else //Condition 2: Z changes
                    {
                        speedZ[0] = SpeedZ;
                        getVelXY(speedZ[0], 0, addressZ[0], 0, addressX[0], 0, addressY[0], out double vxy);
                        if (vxy == 0) vxy = 1;
                        speedXY[0] = vxy;
                        IntTo2Short((int)speedXY[0], out short speedxy1, out short speedxy2);
                        IntTo2Short((int)speedZ[0], out short speedz1, out short speedz2);
                        DataX[0].Speed1 = speedxy1;
                        DataX[0].Speed2 = speedxy2;
                        DataY[0].Speed1 = speedxy1;
                        DataY[0].Speed2 = speedxy2;
                        DataZ[0].Speed1 = speedz1;
                        DataZ[0].Speed2 = speedz2;
                    }

                    // add setting for A-axis
                    if (addressA[0] == 0) //Condition 1: A doesn't change
                    {
                        speedA[0] = 1;
                        DataA[0].Speed1 = 1;
                        DataA[0].Speed2 = 1;
                        speedXY[0] = SpeedXY;
                        IntTo2Short((int)speedXY[0], out short speedxy1, out short speedxy2);
                        DataX[0].Speed1 = speedxy1;
                        DataX[0].Speed2 = speedxy2;
                        DataY[0].Speed1 = speedxy1;
                        DataY[0].Speed2 = speedxy2;
                    }
                    else //Condition 2: A changes
                    {
                        speedA[0] = SpeedA;
                        getVelXY(speedA[0], 0, addressA[0], 0, addressX[0], 0, addressY[0], out double vxy);
                        if (vxy == 0) vxy = 1;
                        speedXY[0] = vxy;
                        IntTo2Short((int)speedXY[0], out short speedxy1, out short speedxy2);
                        IntTo2Short((int)speedA[0], out short speeda1, out short speeda2);
                        DataX[0].Speed1 = speedxy1;
                        DataX[0].Speed2 = speedxy2;
                        DataY[0].Speed1 = speedxy1;
                        DataY[0].Speed2 = speedxy2;
                        DataA[0].Speed1 = speeda1;
                        DataA[0].Speed2 = speeda2;
                    }

                    //Dwell
                    DataX[0].Dwell = dwell; DataY[0].Dwell = dwell; DataZ[0].Dwell = dwell; DataA[0].Dwell = dwell;
                    dwellZ = 200; dwellXY = 0; dwellA = 200;
                    if (addressZ[0] == 0 & addressA[0] == 0) //Condition 1: Z doesn't change
                    {
                        double dxy = Distance(0, addressX[0], 0, addressY[0]);
                        dwellZ = 1000 * (dxy * 100) / (speedXY[0] / 60);
                        dwellA = 1000 * (dxy * 100) / (speedXY[0] / 60);
                    }
                    /*
                    if (addressX[0] == 0 && addressY[0] == 0) //Condition 2: XY don't change
                    {
                        double dz = Math.Abs(0 - addressZ[0]);
                        dwellXY = 1000 * (dz * 100) / (speedZ[0] / 60);
                    }
                    //add Dwell A
                    if (addressA[0] == 0) //Condition 1: A doesn't change
                    {
                        double dxy = Distance(0, addressX[0], 0, addressY[0]);
                        dwellA = 4000 * (dxy * 100) / (speedXY[0] / 60);
                    }
                    if (addressX[0] == 0 && addressY[0] == 0) //Condition 2: XY don't change
                    {
                        double dA = Math.Abs(0 - addressA[0]);
                        dwellXY = 4000 * (dA * 100) / (speedA[0] / 60);
                    }
                    */

                    DataX[0].Dwell += (short)dwellXY;
                    DataY[0].Dwell += (short)dwellXY;
                    DataZ[0].Dwell += (short)dwellZ;
                    DataA[0].Dwell += (short)dwellA;

                    DataX[0].Address1 = AddX1; DataY[0].Address1 = AddY1; DataZ[0].Address1 = AddZ1; DataA[0].Address1 = AddA1;
                    DataX[0].Address2 = AddX2; DataY[0].Address2 = AddY2; DataZ[0].Address2 = AddZ2; DataA[0].Address2 = AddA2;
                    DataX[0].Arc1 = AddI1; DataY[0].Arc1 = AddJ1; DataZ[0].Arc1 = 0; DataA[0].Arc1 = 0;
                    DataX[0].Arc2 = AddI2; DataY[0].Arc2 = AddJ2; DataZ[0].Arc2 = 0; DataA[0].Arc2 = 0;
                    test_dwellX[0] = dwellXY;
                    //next line

                    for (int i = 1; i < Count; ++i)
                    {
                        addressX[i] = gcode[i, 1];
                        addressY[i] = gcode[i, 2];
                        addressZ[i] = gcode[i, 3];
                        addressA[i] = inc_a[i];
                        addressI[i] = gcode[i, 5];
                        addressJ[i] = gcode[i, 6];

                        //--Data 0: Positioning Identifier

                            DataX[i].Iden = ident2_g01;
                            DataY[i].Iden = 0;
                            DataA[i].Iden = ident3_g01;
                            DataZ[i].Iden = ident1_g01;

                        //..
                        //--Data 9: M code
                        DataX[i].MCode = 0;
                        DataY[i].MCode = 0;
                        DataZ[i].MCode = 0;
                        DataA[i].MCode = 0;
                        //..

                        //--Data 7: Command speed
                        if (gcode[i, 0] == 0)
                        {
                            SpeedXY = SpeedXY_const * 4;
                            SpeedZ = SpeedZ_const * 2;
                            SpeedA = SpeedA_const * 3 ;
                        }
                        if (gcode[i, 0] == 1 || gcode[i, 0] == 2 || gcode[i, 0] == 3)
                        {
                            SpeedXY = SpeedXY_const / 6; // = 1500/6 = 250 (mm)
                            SpeedZ = SpeedZ_const / 4;
                            SpeedA = SpeedA_const / 1; // = 78.53 (mm)
                        }
                        if (addressX[i] - addressX[i - 1] != 0 && addressZ[i] - addressZ[i - 1] == 0 && addressA[i] == 0) //Condition 1: X changes
                        {
                            speedZ[i] = 1;
                            DataZ[i].Speed1 = 1;
                            DataZ[i].Speed2 = 1;
                            speedA[i] = 1;
                            DataA[i].Speed1 = 1;
                            DataA[i].Speed2 = 1;
                            speedXY[i] = SpeedXY/4; // 250 / 4
                            IntTo2Short((int)speedXY[i], out short speedxy3, out short speedxy4);
                            DataX[i].Speed1 = speedxy3;
                            DataX[i].Speed2 = speedxy4;
                            DataY[i].Speed1 = speedxy3;
                            DataY[i].Speed2 = speedxy4;
                        }
                        else if (addressX[i] == addressX[i - 1] && addressA[i] == 0) //Condition 2: Z changes
                        {
                            speedZ[i] = SpeedZ;
                            getVelXY(speedZ[i], addressZ[i - 1], addressZ[i], addressX[i - 1], addressX[i], addressY[i - 1], addressY[i], out double vxy);
                            if (vxy == 0) vxy = 1;
                            speedXY[i] = vxy;
                            speedA[i] = 1;
                            IntTo2Short((int)speedXY[i], out short speedxy3, out short speedxy4);
                            IntTo2Short((int)speedZ[i], out short speedz3, out short speedz4);
                            IntTo2Short((int)speedA[i], out short speeda3, out short speeda4);
                            DataX[i].Speed1 = speedxy3;
                            DataX[i].Speed2 = speedxy4;
                            DataY[i].Speed1 = speedxy3;
                            DataY[i].Speed2 = speedxy4;
                            DataZ[i].Speed1 = speedz3;
                            DataZ[i].Speed2 = speedz4;
                            DataA[i].Speed1 = speeda3;
                            DataA[i].Speed2 = speeda4;
                        }
                        else if (addressX[i] == addressX[i - 1] && addressZ[i] == addressZ[i - 1] && addressA[i] != 0)// Condition 3: Only A changes
                        {
                            speedA[i] = SpeedA_const;
                            speedXY[i] = 1;
                            speedZ[i] = 1;
                            IntTo2Short((int)speedXY[i], out short speedxy3, out short speedxy4);
                            IntTo2Short((int)speedZ[i], out short speedz3, out short speedz4);
                            IntTo2Short((int)speedA[i], out short speeda3, out short speeda4);
                            DataX[i].Speed1 = speedxy3;
                            DataX[i].Speed2 = speedxy4;
                            DataY[i].Speed1 = speedxy3;
                            DataY[i].Speed2 = speedxy4;
                            DataZ[i].Speed1 = speedz3;
                            DataZ[i].Speed2 = speedz4;
                            DataA[i].Speed1 = speeda3;
                            DataA[i].Speed2 = speeda4;
                        }
                        //add code control condination of 2 axes
                        else if (addressX[i] - addressX[i - 1] != 0 && addressA[i] != 0) // Condition 4: A and X changes
                        {
                            double dxa;
                            double dax;
                            dxa = Distance(addressX[i - 1], addressX[i], addressY[i - 1], addressY[i]);
                            dax = addressA[i];
                           // double time_ax = dax / (SpeedA_const / (1000 * 60));
                           //  double velX = Math.Abs(dxa / time_ax * 100 * 60);
                            double time_ax = dax / (SpeedA_const / (1000 * 60));
                            double velX = Math.Abs(dxa / dax * SpeedA_const / 10);
                            speedA[i] = SpeedA_const;
                            speedXY[i] = velX;
                            speedZ[i] = 1;
                            IntTo2Short((int)speedXY[i], out short speedxy3, out short speedxy4);
                            IntTo2Short((int)speedZ[i], out short speedz3, out short speedz4);
                            IntTo2Short((int)speedA[i], out short speeda3, out short speeda4);
                            DataX[i].Speed1 = speedxy3;
                            DataX[i].Speed2 = speedxy4;
                            DataY[i].Speed1 = speedxy3;
                            DataY[i].Speed2 = speedxy4;
                            DataZ[i].Speed1 = speedz3;
                            DataZ[i].Speed2 = speedz4;
                            DataA[i].Speed1 = speeda3;
                            DataA[i].Speed2 = speeda4;
                            /*
                              double dxa;
                                double dax;
                                dxa = Distance(addressX[i - 1], addressX[i], addressY[i - 1], addressY[i]);
                                dax = addressA[i] ;
                                double time_ax = dax / (SpeedA_const / (1000 * 60 ));
                                double velX = Math.Abs(dxa / time_ax * 100 * 60);
                                speedA[i] = SpeedA_const;
                                speedXY[i] = velX;
                                speedZ[i] = 1;
                                IntTo2Short((int)speedXY[i], out short speedxy3, out short speedxy4);
                                IntTo2Short((int)speedZ[i], out short speedz3, out short speedz4);
                                IntTo2Short((int)speedA[i], out short speeda3, out short speeda4);
                                DataX[i].Speed1 = speedxy3;
                                DataX[i].Speed2 = speedxy4;
                                DataY[i].Speed1 = 1;
                                DataY[i].Speed2 = 1;
                                DataZ[i].Speed1 = speedz3;
                                DataZ[i].Speed2 = speedz4;
                                DataA[i].Speed1 = speeda3;
                                DataA[i].Speed2 = speeda4;
                             */
                        }
                        else if (addressX[i] == 0 && addressY[i] == 0 && addressZ[i] == 0) // Condition 4: Back Home
                        {
                            speedXY[i] = SpeedXY_const;
                            speedZ[i] = SpeedZ_const;
                            speedA[i] = 1;
                            IntTo2Short((int)speedXY[i], out short speedxy3, out short speedxy4);
                            IntTo2Short((int)speedZ[i], out short speedz3, out short speedz4);
                            IntTo2Short((int)speedA[i], out short speeda3, out short speeda4);
                            DataX[i].Speed1 = speedxy3;
                            DataX[i].Speed2 = speedxy4;
                            DataY[i].Speed1 = speedxy3;
                            DataY[i].Speed2 = speedxy4;
                            DataZ[i].Speed1 = speedz3;
                            DataZ[i].Speed2 = speedz4;
                            DataA[i].Speed1 = speeda3;
                            DataA[i].Speed2 = speeda4;
                        }



                        //..


                        //--Data 8: Dwell time
                        DataX[i].Dwell = dwell;
                        DataY[i].Dwell = dwell;
                        DataZ[i].Dwell = dwell;
                        DataA[i].Dwell = dwell;
                        dwellZ = 200; dwellXY = 0; dwellA = 100; dwellX = 100; dwellY = 100;

                        if (addressZ[i] == addressZ[i - 1] && addressA[i] == 0 && (addressX[i] != addressX[i - 1] || addressY[i] != addressY[i - 1])) //Condition 1: X changes
                        {
                            double dxy;
                            if (gcode[i, 0] == 2) dxy = CurveLength2(addressX[i - 1], addressY[i - 1], addressX[i], addressY[i], addressI[i], addressJ[i]);
                            else if (gcode[i, 0] == 3) dxy = CurveLength2(addressX[i - 1], addressY[i - 1], addressX[i], addressY[i], addressI[i], addressJ[i]);
                            else dxy = Distance(addressX[i - 1], addressX[i], addressY[i - 1], addressY[i]);
                            length[i] = dxy;
                            dwellZ = 1000 * (dxy * 100) / (speedXY[i] / 60);
                            dwellA = 1000 * (dxy * 100) / (speedXY[i] / 60);
                        }
                        else if (addressX[i] == addressX[i - 1] && addressY[i] == addressY[i - 1] && addressA[i] == 0 && addressZ[i] != addressZ[i - 1]) //Condition 2: Z changes
                        {
                            double dz = Math.Abs(addressZ[i - 1] - addressZ[i]);
                            length[i] = dz;
                            dwellX = (1000 * (dz * 100) / (speedZ[i] / 60));
                            dwellY = (1000 * (dz * 100) / (speedZ[i] / 60));
                            dwellA = (1000 * (dz * 100) / (speedZ[i] / 60));
                            test_dwellX[i] = dwellXY;
                        }

                        else if (addressA[i] != 0 && addressX[i] - addressX[i - 1] == 0)// Condition 3: A changes
                        {
                            double da;
                            da = Math.Abs(addressA[i]);
                            length[i] = Math.Abs(da);
                            dwellX = 10000 * (da * 100) / (speedA[i] / 60);
                            dwellY = 10000 * (da * 100) / (speedA[i] / 60);
                            dwellZ = 10000 * (da * 100) / (speedA[i] / 60);
                            test_dwellX[i] = dwellXY;
                        }
                        else if (addressX[i] - addressX[i - 1] != 0 && addressA[i] != 0) // Condition 4: A and X change
                        {
                            double da;
                            da = Math.Abs(addressA[i]);
                            length[i] = Math.Abs(da);
                            dwellY = 10000 * (da * 100) / (speedA[i] / 60);
                            dwellZ = 10000 * (da * 100) / (speedA[i] / 60);
                            test_dwellX[i] = dwellXY;
                        }

                        else if (addressX[i] == 0 && addressY[i] == 00 && addressZ[i] == 0) // Condition 5: Back home
                        {
                            double dxy;
                            dxy = Distance(addressX[i - 1], addressX[i], addressY[i - 1], addressY[i]);
                            length[i] = dxy;
                            dwellA = 1000 * (dxy * 100) / (speedXY[i] / 60);
                        }

                        DataX[i].Dwell += (short)dwellX;
                        DataY[i].Dwell += (short)dwellY;
                        DataZ[i].Dwell += (short)dwellZ;
                        DataA[i].Dwell += (short)dwellA;
                        //..
                        //--Data 5: Positioning Address/movement amount
                        IntTo2Short((int)(addressX[i] * 10000), out short AddressX1, out short AddressX2);
                        DataX[i].Address1 = AddressX1;
                        DataX[i].Address2 = AddressX2;
                        IntTo2Short((int)(addressY[i] * 10000), out short AddressY1, out short AddressY2);
                        DataY[i].Address1 = AddressY1;
                        DataY[i].Address2 = AddressY2;
                        IntTo2Short((int)(addressZ[i] * 10000), out short AddressZ1, out short AddressZ2);
                        DataZ[i].Address1 = AddressZ1;
                        DataZ[i].Address2 = AddressZ2;
                        //edit 10000 -> 100000
                        IntTo2Short((int)(addressA[i] * 100000), out short AddressA1, out short AddressA2);
                        DataA[i].Address1 = AddressA1;
                        DataA[i].Address2 = AddressA2;
                        //..
                        //--Data 6 Arc Address
                        IntTo2Short((int)(addressI[i] * 10000), out short AddressI1, out short AddressI2);
                        IntTo2Short((int)(addressJ[i] * 10000), out short AddressJ1, out short AddressJ2);
                        DataX[i].Arc1 = AddressI1;
                        DataX[i].Arc2 = AddressI2;
                        DataY[i].Arc1 = AddressJ1;
                        DataY[i].Arc2 = AddressJ2;
                        DataZ[i].Arc1 = 0;
                        DataZ[i].Arc2 = 0;
                        DataA[i].Arc1 = 0;
                        DataA[i].Arc2 = 0;
                        //..


                        //..
                    }


                    DataX[Count - 1].Iden = ident2_g01;
                    DataY[Count - 1].Iden = 0;
                    DataA[Count - 1].Iden = ident3_g01;
                    DataZ[Count - 1].Iden = ident1_g01;


                    ListPosData1.Clear();
                    ListPosData2.Clear();
                    ListPosData3.Clear();
                    ListPosData4.Clear();
                    for (int i = 0; i < Count; i++)
                    {
                        ListPosData1.Add(DataX[i].Iden);
                        ListPosData1.Add(DataX[i].MCode);
                        ListPosData1.Add(DataX[i].Dwell);
                        ListPosData1.Add(DataX[i].Dummy);
                        ListPosData1.Add(DataX[i].Speed1);
                        ListPosData1.Add(DataX[i].Speed2);
                        ListPosData1.Add(DataX[i].Address1);
                        ListPosData1.Add(DataX[i].Address2);
                        ListPosData1.Add(DataX[i].Arc1);
                        ListPosData1.Add(DataX[i].Arc2);

                        ListPosData2.Add(DataY[i].Iden);
                        ListPosData2.Add(DataY[i].MCode);
                        ListPosData2.Add(DataY[i].Dwell);
                        ListPosData2.Add(DataY[i].Dummy);
                        ListPosData2.Add(DataY[i].Speed1);
                        ListPosData2.Add(DataY[i].Speed2);
                        ListPosData2.Add(DataY[i].Address1);
                        ListPosData2.Add(DataY[i].Address2);
                        ListPosData2.Add(DataY[i].Arc1);
                        ListPosData2.Add(DataY[i].Arc2);

                        ListPosData3.Add(DataZ[i].Iden);
                        ListPosData3.Add(DataZ[i].MCode);
                        ListPosData3.Add(DataZ[i].Dwell);
                        ListPosData3.Add(DataZ[i].Dummy);
                        ListPosData3.Add(DataZ[i].Speed1);
                        ListPosData3.Add(DataZ[i].Speed2);
                        ListPosData3.Add(DataZ[i].Address1);
                        ListPosData3.Add(DataZ[i].Address2);
                        ListPosData3.Add(DataZ[i].Arc1);
                        ListPosData3.Add(DataZ[i].Arc2);

                        ListPosData4.Add(DataA[i].Iden);
                        ListPosData4.Add(DataA[i].MCode);
                        ListPosData4.Add(DataA[i].Dwell);
                        ListPosData4.Add(DataA[i].Dummy);
                        ListPosData4.Add(DataA[i].Speed1);
                        ListPosData4.Add(DataA[i].Speed2);
                        ListPosData4.Add(DataA[i].Address1);
                        ListPosData4.Add(DataA[i].Address2);
                        ListPosData4.Add(DataA[i].Arc1);
                        ListPosData4.Add(DataA[i].Arc2);
                    }
                    PosData1 = null;
                    PosData2 = null;
                    PosData3 = null;
                    PosData4 = null;
                    PosData1 = ListPosData1.ToArray();
                    PosData2 = ListPosData2.ToArray();
                    PosData3 = ListPosData3.ToArray();
                    PosData4 = ListPosData4.ToArray();
                }

            }
        }
    


        private void Execute_button_Click(object sender, EventArgs e)
        {
            PLC.SetDevice("M41", 1);//Send positioning 1 point
            PLC.SetDevice("M41", 0);
            PLC.SetDevice("M999", 1);//Start positioning 1 point
            PLC.SetDevice("M999", 0);
        }

    }
}
