using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;


//using SocketGlobal;
//using SocketGlobal.SendData;

//using SuperSocket.ClientEngine;

//using FreeNet;


namespace Call_Program
{
    public partial class Form1 : Form
    {
        AMR aMR = null;
        
        public struct stSERVER
        {
            public string SRV_NAME;
            public string SRV_IP;
            public string SRV_PORT;
            public Socket SRV_Client;
        }

        public struct stSTB
        {
            public string NAME;
            public string GOAL_NAME;
        }

        public stSERVER AMC_SRV = new stSERVER();
        public static stSTB STB = new stSTB();
        string Vehicle_Name = "";
        string AMR_Status = "";
        bool isCall = false;

        
        //private AsyncTcpSession Client;

        public AsynchronousClient Call_Client = new AsynchronousClient();

        public Form1()
        {
            //Call_Client.Start();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            Read_Setting_Text();

            Call_Client.Set_IP(AMC_SRV.SRV_IP);
            Call_Client.Set_Port(int.Parse(AMC_SRV.SRV_PORT));
            btn_Call.Text = aMR.Get_AMR_NAME() + " 호출";
             
            //Call_Client.Start();

            //IPEndPoint ipServer = new IPEndPoint(IPAddress.Parse(AMC_SRV.SRV_IP)
            //                                   , Convert.ToInt32(AMC_SRV.SRV_PORT));
            //EndPoint epTemp = ipServer as EndPoint;

            //Client = new AsyncTcpSession(epTemp);

            //Client.Connected += Client_Connected;
            //Client.Closed += Client_Closed;
            //Client.DataReceived += Client_DataReceived;
            //Client.Error += Client_Error; ;
            //Client.SendingQueueSize = 0xffff;
            
            
            bgw_client.RunWorkerAsync();
        }

        private void Client_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex.Message);
            }

        }

        //private void Client_DataReceived(object sender, DataEventArgs e)
        //{
        //    try
        //    {
        //        dataOriginal insData = new dataOriginal();
        //        insData.Length = e.Length;
        //        //데이터를 오프셋을 기준으로 자른다.
        //        Buffer.BlockCopy(e.Data, e.Offset, insData.Data, 0, insData.Length);
        //        Insert_System_Log(Encoding.UTF8.GetString(insData.Data));
        //        SRV_Parse(Encoding.UTF8.GetString(insData.Data));
        //    }
        //    catch (Exception ex)
        //    {
        //        Insert_ERR_Log(ex.Message);
        //    }
        //}

        private void Client_Closed(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex.Message);
            }
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            //SendMsg(claCommand.Command.ID_Check, STB.NAME);
        }

        static public string Vehicle_Status;
        static public string Vehicle_Area;

        private void Set_ST()
        {
            l_ST.Text = Vehicle_Status;
        }

        private void Set_AREA()
        {
            lb_AREA.Text = Vehicle_Area;
        }

        static DateTime Link_T = DateTime.Now;

        public static void SRV_Parse(string msg)
        {
            string[] cmd_msg = msg.Split(';');

            try
            {
                for (int i = 0; i < cmd_msg.Length; i++)
                {
                    string AMC_NAME = Find_AMC(cmd_msg[i]);
                    string STB_NAME = Find_STB(cmd_msg[i]);
                    string CMD_NAME = Find_CMD(cmd_msg[i]);
                    string GOAL_NAME = Find_GOAL(cmd_msg[i]);
                    string ST_NAME = Find_STATUS(cmd_msg[i]);

                    if (STB_NAME == STB.NAME)
                    {
                        if (AMC_NAME == "NOT_ASSIGN" && CMD_NAME == "QUEUED")
                        {
                            Message_Text = "다른 작업 중 입니다.";
                        }

                        if(CMD_NAME == "STATUS")
                        {
                            Vehicle_Status = ST_NAME;

                            if(ST_NAME.Contains("GO") == true)
                            {
                                if(ST_NAME.Contains(STB.GOAL_NAME) == true)
                                {
                                    Message_Text = STB.GOAL_NAME + "으로 이동 중입니다.";
                                }
                            }
                        }
                        else if (CMD_NAME == "AREA")
                            Vehicle_Area = ST_NAME;


                        if (CMD_NAME == "CALL" && ST_NAME == "ASSIGN")
                        {
                            Message_Text = "AMB가 할당 되었습니다.";
                        }

                        if (ST_NAME == "RESET_ACK")
                        {
                            Message_Text = "서버 재시작 중입니다.";
                        }                        

                        if(ST_NAME.Contains("REJECT") == true)
                        {
                            Message_Text = "중복된 명령어 입니다.";
                        }

                        if (CMD_NAME == "LINK_TEST" && ST_NAME == "LINK_ACK")
                        {
                            Link_T = DateTime.Now;
                                                        
                            if(Message_Text.Contains("서버 정상") == true)
                            {
                                if(Message_Text.Contains("....") == true)
                                    Message_Text = "서버 정상 동작 중.";
                                else
                                    Message_Text += ".";
                            }
                            else
                            {
                                Message_Text = "서버 정상 동작 중.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex.Message);
            }
        }

        
        public void Send_Call()
        {
            try
            {
                string _buf = string.Format("SEND,STB={0},AMC=NONE,CMD=CALL,GOAL={1},STATUS=CALL;", STB.NAME, STB.GOAL_NAME);

                Send_string(_buf);
                Insert_System_Log(_buf);
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex.Message);
            }
            
        }


        public void Send_Link_Test()
        {
            try
            {
                string _buf = string.Format("SEND,STB={0},AMC=NONE,CMD=LINK_TEST,GOAL={1},STATUS=LINK_TEST;", STB.NAME, STB.GOAL_NAME);

                
                Send_string(_buf);
                Insert_System_Log(_buf);
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex.Message);
            }
            
        }
        
        public void Send_Get_ST()
        {
            try
            {
                string _buf = string.Format("SEND,STB={0},AMC={1},CMD=STATUS,GOAL={2},STATUS=STATUS;", STB.NAME, Vehicle_Name ,STB.GOAL_NAME);


                Send_string(_buf);
                Insert_System_Log(_buf);
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex.Message);
            }

        }

        public void Send_Get_AREA()
        {
            try
            {
                string _buf = string.Format("SEND,STB={0},AMC={1},CMD=STATUS,GOAL={2},STATUS=AREA;", STB.NAME, Vehicle_Name,STB.GOAL_NAME);


                Send_string(_buf);
                Insert_System_Log(_buf);
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex.Message);
            }

        }

        public void Send_Server_Reset()
        {
            string _buf = string.Format("SEND,STB={0},AMC=NONE,CMD=LINK_TEST,GOAL={1},STATUS=RESET;", STB.NAME, STB.GOAL_NAME);

            Call_Client.Send_Data(_buf);
        }

        private void Send_string(string msg)
        {
            //byte[] byteSend = Encoding.UTF8.GetBytes(msg);

            try
            {
                if (Call_Client.Is_Connected() == true)
                {
                    Call_Client.Send_Data(msg);
                }
                else
                {
                    Call_Client.Connect();
                    Call_Client.Send_Data(msg);
                }
            }
            catch (Exception ex)
            {
                Insert_ERR_Log("[Send_String]" + ex.Message);
                throw;
            }
        }

        public static void Insert_System_Log(string msg)
        {
            if (Last_MSG == msg)
            {
                return;
            }

            string date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:FFF");
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\System\\" + System.DateTime.Now.ToString("yyyy/MM/dd") + "_Log.txt";
            DirectoryInfo dir = new DirectoryInfo(System.Environment.CurrentDirectory + "\\Log\\System\\");
                                   
            try
            {
                if (dir.Exists == false)
                {
                    dir.Create();

                    string temp;

                    temp = "========================================================" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "=                                   AMC SYSTEM Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                 =" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "========================================================" + Environment.NewLine;

                    System.IO.File.WriteAllText(log_dir, temp);

                    string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i];
                            st.WriteLine(str_temp.ToCharArray());
                        }
                    }

                    Check_Log_date(System.Environment.CurrentDirectory + "\\Log\\System\\", 30);
                }
                else
                {
                    string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i];
                            st.WriteLine(str_temp.ToCharArray());
                        }
                    }
                    st.Close();
                    st.Dispose();
                }

                Last_MSG = msg;


            }
            catch (Exception)
            {
                System.Threading.Thread.Sleep(10);
            }
        }

        public static void Insert_ERR_Log(string msg)
        {
            if (Last_MSG == msg)
            {
                return;
            }

            string date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:FFF"); ;
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\Error\\Err" + System.DateTime.Now.ToString("yyyy/MM/dd") + "_Log.txt";
            DirectoryInfo dir = new DirectoryInfo(System.Environment.CurrentDirectory + "\\Log\\Error\\Err");
                       
            try
            {
                if (dir.Exists == false)
                {
                    string temp;
                    dir.Create();

                    temp = "========================================================" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "=                                   AMC SYSTEM Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                 =" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "========================================================" + Environment.NewLine;

                    System.IO.File.WriteAllText(log_dir, temp);

                    string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i];
                            st.WriteLine(str_temp.ToCharArray());
                        }
                    }

                    Check_Log_date(System.Environment.CurrentDirectory + "\\Log\\System\\Error\\", 30);
                }
                else
                {
                    string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i];
                            st.WriteLine(str_temp.ToCharArray());
                        }
                    }
                    st.Close();
                    st.Dispose();
                }

                Last_MSG = msg;
            }
            catch (Exception)
            {
                System.Threading.Thread.Sleep(10);
            }
        }

        static private void Check_Log_date(string log_dir, int keep_day)
        {
            try
            {
                DirectoryInfo logdir = new DirectoryInfo(log_dir);

                foreach (FileInfo file in logdir.GetFiles())
                {
                    if (file.CreationTime < DateTime.Now.AddDays(-keep_day))
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex.Message);
            }
        }


        public static string Last_MSG;

        public void Read_Setting_Text()
        {
            try
            {
                string Setting_data = System.IO.File.ReadAllText(System.Environment.CurrentDirectory + "\\Setting\\Setting.txt");

                string[] str_data = Setting_data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                string[] LD_ip = new string[10];
                int[] LD_port = new int[10];
                string[] LD_name = new string[10];

                for (int i = 0; i < str_data.Length - 1; i++)
                {
                    if (str_data[i] == "[SERVER]")
                    {
                        string[] str_temp;
                        for (int j = 0; j < 4; j++)
                        {
                            str_temp = str_data[i + j + 1].Split('=');

                            if (str_temp[0] == "NAME")
                            {
                                AMC_SRV.SRV_NAME = str_temp[1];
                            }
                            else if (str_temp[0] == "IP")
                            {
                                AMC_SRV.SRV_IP = str_temp[1];
                            }
                            else if (str_temp[0] == "PORT")
                            {
                                AMC_SRV.SRV_PORT = str_temp[1];
                            }
                        }
                        AMC_SRV.SRV_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }
                    else if (str_data[i] == "[STB]")
                    {
                        string[] str_temp;

                        for (int j = 0; j < 2; j++)
                        {
                            str_temp = str_data[j + i + 1].Split('=');

                            if (str_temp[0] == "NAME")
                            {
                                STB.NAME = str_temp[1];
                            }
                            else if (str_temp[0] == "GOAL_NAME")
                            {
                                STB.GOAL_NAME = str_temp[1];
                            }
                        }
                    }
                    else if(str_data[i] =="[VEHICLE]")
                    {
                        string[] str_temp;

                        str_temp = str_data[i + 1].Split('=');
                        string name = "", ip = "", pw = "";
                        int port = -1;

                        for (int j = 0; j < 5; j++)
                        {
                            str_temp = str_data[j + i + 1].Split('=');

                            

                            if (str_temp[0] == "NAME")
                                name = str_temp[1];
                            else if (str_temp[0] == "IP")
                                ip = str_temp[1];
                            else if (str_temp[0] == "PORT")
                                port  = int.Parse(str_temp[1]);
                            else if (str_temp[0] == "PW")
                                pw = str_temp[1];                           

                        }
                        AMR amr_temp = new AMR(name, ip, port, pw, "AMT");

                        aMR = amr_temp;
                    }
                }
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex.Message);
            }
        }

        DateTime dstate_time = DateTime.Now;
        static string Message_Text = "";
        private void bgw_client_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            Call_Client.Start();
            double Sec = 0;

            while (true)
            {
                try
                {
                    Sec = (DateTime.Now - dstate_time).TotalSeconds;

                    AMR_Status = aMR.Get_AMR_Status();
                    Vehicle_Status = AMR_Status;

                    if (AMR_Status == "NONE")
                    {
                        Message_Text = "접속 되지 않았습니다.";
                    }
                    else if (AMR_Status.Contains(STB.GOAL_NAME) == true)
                    {
                        Message_Text = string.Format("Vehicle이 {0}로 이동 중 입니다.", STB.NAME);
                    }
                    else if(AMR_Status.Contains("Executing") == true)
                    {
                        Message_Text = string.Format("Vehicle이 {0}로 이동 중 입니다.", STB.NAME);
                    }
                    else if(AMR_Status.Contains("Completed") == true)
                    {
                        Message_Text = string.Format("{0}에 도착 했습니다.", STB.NAME);
                        Text = "도착";
                    }
                    else if(AMR_Status.Contains("Arrived") == true)
                    {
                        Message_Text = string.Format("{0}에 도착 했습니다.", STB.NAME);
                    }
                    else
                        Message_Text = AMR_Status;
                                       
                    
                    if (isCall == true)
                    {
                        if(AMR_Status.ToUpper().Contains("DOCKED") == true)
                        {
                            //aMR.Send();
                            aMR.Send(string.Format("executeMacro MOVE_{0}", STB.GOAL_NAME));
                            isCall = false;
                            System.Threading.Thread.Sleep(1000);
                        }
                        else if (AMR_Status.ToUpper().Contains(STB.GOAL_NAME.ToUpper()) == true)
                        {
                            isCall = false;
                        }                        
                    }

                    // 20220530 Direct
                    //if (Call_Client.Is_Connected() == true)
                    //{
                    //    if (Sec >= 10)
                    //    {
                    //        dstate_time = DateTime.Now;

                    // Direct 방식에서는 불필요
                    //        //Send_Link_Test();
                    //        //Send_Get_AREA();
                    //        //Send_Get_ST();
                    //    }



                    //    if ((DateTime.Now - Link_T).TotalMinutes >= 1)
                    //    {
                    //        dstate_time = DateTime.Now;
                    //        Message_Text = "Link Test 실패";
                    //        Call_Client.Close();
                    //    }
                    //}
                    //else
                    //{
                    //    Call_Client.Connect();

                    //    if ((DateTime.Now - Link_T).TotalSeconds >= 30)
                    //    {
                    //        Message_Text = "Link Test 실패";
                    //    }

                    //    System.Threading.Thread.Sleep(10000);
                    //}


                    if (bgw_Display.IsBusy == false)
                    {
                        bgw_Display.RunWorkerAsync();
                    }

                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Insert_ERR_Log("bgw_client_DoWork]" + ex.Message);
                }
            }
        }


        private static string Find_AMC(string msg)
        {
            try
            {
                string[] str_buf = msg.Split(',');

                for (int i = 0; i < str_buf.Length - 1; i++)
                {
                    string[] str_temp = str_buf[i].Split('=');

                    if (str_temp[0] == "AMC")
                    {
                        return str_temp[1];
                    }
                }                
            }
            catch (Exception ex)
            {
                Insert_ERR_Log("Find_AMC" + ex.Message);
            }
            return "EMPTY";
        }

        private static string Find_STB(string msg)
        {
            string val = "EMPTY";

            try
            {                
                string[] _msg = msg.Split(',');

                for (int i = 0; i < _msg.Length; i++)
                {
                    string[] _buf = _msg[i].Split('=');
                    if (_buf[0] != "")
                    {
                        if (_buf[0] == "STB")
                        {
                            val = _buf[1];
                        }
                    }
                }

                return val;
            }
            catch (Exception ex)
            {
                Insert_ERR_Log("Find_STB]" + ex.Message);
            }

            return val;
        }

        private static string Find_STATUS(string msg)
        {
            string val = "EMPTY";

            try
            {
                string[] _msg = msg.Split(',');

                for (int i = 0; i < _msg.Length; i++)
                {
                    string[] _buf = _msg[i].Split('=');

                    if (_buf[0] != "")
                    {
                        if (_buf[0] == "STATUS")
                        {
                            val = _buf[1];
                        }
                    }
                }

                return val;
            }
            catch (Exception ex)
            {
                Insert_ERR_Log("Find_STATUS]" + ex.Message);
            }

            return val;
        }


        private static string Find_CMD(string msg)
        {
            string[] str_buf = msg.Split(',');

            try
            {
                for (int i = 0; i < str_buf.Length; i++)
                {
                    string[] str_temp = str_buf[i].Split('=');

                    if (str_temp[0] == "CMD")
                    {
                        return str_temp.Length > 1 ? str_temp[1] : "EMPTY";
                    }
                }
                return "EMPTY";
            }
            catch (Exception ex)
            {
                Insert_ERR_Log("Find_CMD] " + ex.Message);
            }
            return "EMPTY";
        }

        private static string Find_GOAL(string msg)
        {
            string[] str_buf = msg.Split(',');

            for (int i = 0; i < str_buf.Length; i++)
            {
                string[] str_temp = str_buf[i].Split('=');

                if (str_temp[0] == "GOAL")
                {
                    return str_temp[1];
                }
            }
            return "EMPTY";
        }

        private void bgw_Display_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            
            while(true)
            {
                try
                {
                    lMessage.Text = Message_Text;

                    if(Message_Text.Contains("NONE") == false)
                    {
                        pb_green.Visible = true;
                        pb_red.Visible = false;
                    }
                    else
                    {
                        pb_green.Visible = false;
                        pb_red.Visible = true;
                    }

                    Set_ST();
                    Set_AREA();

                    if (bgw_client.IsBusy == false)
                    {
                        bgw_client.RunWorkerAsync();
                    }

                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Insert_ERR_Log(ex.Message);
                }                
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bgw_client.IsBusy == false)
            {
                bgw_client.CancelAsync();
            }

            if (bgw_Display.IsBusy == false)
            {
                bgw_Display.CancelAsync();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Send_Server_Reset();
        }

        private void btn_Call_Click(object sender, EventArgs e)
        {
            if (isCall == true)
            {
                Message_Text = "이미 호출이 예약 되어 있습니다..";
                Text = "이미 호출이 예약 되어 있습니다..";
            }
            else
            {
                Message_Text = "호출이 예약 되어 있습니다..";
                Text = "호출이 예약 되어 있습니다..";
                isCall = true;
            }
            // 20220530 Direct
            //Send_Call();
        }

        private void Set_Back_Color()
        {
            btn_Call.BackColor = Color.Lime;
        }
    }
}
