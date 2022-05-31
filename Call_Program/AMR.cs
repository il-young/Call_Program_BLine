using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;


namespace Call_Program
{
    public class StateObject
    {
        // Client socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 256;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }

    public class AMR
    {
        public delegate void Location(string name, System.Drawing.Point p);
        public event Location location_event;

        public delegate void add_amr(PictureBox box);
        public event add_amr add_amr_event;


        private string AMR_NAME;
        private IPAddress AMR_IP;
        private int AMR_PORT;
        private string AMR_PW;
        private string AMR_TYPE;
        private bool b_amr_t = false;

        //private Socket AMR_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //private Socket cdsock;

        private Simply_TCP.Client AMR_Client;

        private double map_location_x;
        private double map_location_y;

        private double map_location_per_x;
        private double map_location_per_y;

        private Size back_img_size;
        private Size pic_size;

        private int AMR_X, AMR_Y;
        private int B_AMR_X = int.MaxValue;
        private int B_AMR_Y = int.MaxValue;
        private string AMR_AREA = "";
        private string TRAFFIC_AREA = "";
        private bool AMR_IS_CON;
        private string AMR_STATUS = "NONE";
        
        private BackgroundWorker bg_AMR = new BackgroundWorker();
        private string X_Move = "";
        private string Y_Move = "";

        private Rectangle map_size;
        private Rectangle org_map;

        private Point ORG_location;
        private Point map_offset;

        private byte[] recvbuffer = new byte[4096];

        private System.Windows.Forms.PictureBox pic_amr = new System.Windows.Forms.PictureBox();

        public AMR(string name, string ip, int port, string pw, string type, Point offset, PictureBox box)
        {
            AMR_NAME = name;
            AMR_IP = IPAddress.Parse(ip);
            AMR_PORT = port;
            AMR_PW = pw;
            AMR_TYPE = type;
            map_offset = offset;
            pic_amr.Name = name;


            //IPEndPoint remoteIP = new IPEndPoint(AMR_IP, AMR_PORT);
            //SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            //args.RemoteEndPoint = remoteIP;

            //StartClient();

            AMR_Client = new Simply_TCP.Client(ip, AMR_PORT);
            AMR_Client.set_start_str("");
            AMR_Client.set_end_str("\u000d\u000a");
            AMR_Client.set_send_delay(100);
            AMR_Client.receiv_event += AMR_Client_receiv_event;

            AMR_Client.Connect();

            bg_AMR.DoWork += Bg_AMR_DoWork;
        }

        public AMR(string name, string ip, int port, string pw, string type)
        {
            AMR_NAME = name;
            AMR_IP = IPAddress.Parse(ip);
            AMR_PORT = port;
            AMR_PW = pw;
            AMR_TYPE = type;

            //IPEndPoint remoteIP = new IPEndPoint(AMR_IP, AMR_PORT);
            //SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            //args.RemoteEndPoint = remoteIP;

            //StartClient();

            AMR_Client = new Simply_TCP.Client(ip, AMR_PORT);
            AMR_Client.set_start_str("");
            AMR_Client.set_end_str("\u000d\u000a");
            AMR_Client.set_send_delay(100);
            AMR_Client.receiv_event += AMR_Client_receiv_event;

            AMR_Client.Connect();

            bg_AMR.DoWork += Bg_AMR_DoWork;
        }

        private void AMR_Client_receiv_event(string data)
        {
            Receivedata(data);
        }

        private void Bg_AMR_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (b_amr_t == true)
                {
                    
                    ask_status();
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.Message);
            }            
        }

        DateTime recive_time = DateTime.Now;

        public void Send(String data)
        {
            try
            {                
                AMR_Client.Send_data(data); 
            }
             catch(SocketException e)
            {
                
            }
        }


        private void Receivedata(string str)
        {
            if (str != "")
            {
                str = str.Replace("\0", "").Trim();
                receive_data(str);                            
            }
        }

        public void set_map_size(System.Drawing.Rectangle rec)
        {
            map_size = rec;
        }

        public void set_pic_size(Size s)
        {
            pic_size = s;
        }


        private void Set_map_location()
        {
            // LD Location point -> Map Location percent
            map_location_x = Math.Abs((double)(map_size.X - AMR_X)) / (double)map_size.Width;
            map_location_y = Math.Abs((double)(map_size.Y - AMR_Y)) / (double)map_size.Height;

            // Map location percent -> LD Map location point
            ORG_location.X = org_map.X + (int)((double)org_map.Width * map_location_x);
            ORG_location.Y = org_map.Y + (int)((double)org_map.Height * map_location_y);

            // LD Map location point -> back image location percent
            map_location_per_x = (double)ORG_location.X / (double)back_img_size.Width;
            map_location_per_y = (double)ORG_location.Y / (double)back_img_size.Height;

            //location_event(AMR_NAME, ORG_location);
        }

        public void set_ORG_map_size(Rectangle p)
        {
            org_map = p;
        }

        public void set_org_back_img_size(Size p)
        {
            back_img_size = p;
        }        
        

        private void ask_status()
        {
            try
            {   
                Send("status");                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }

       
        private void receive_data(string data)
        {
            string[] str_buf = new string[100];
            str_buf = data.Split('\r');

            try
            {
                recive_time = DateTime.Now;

                //Console.WriteLine(AMR_NAME + "] ");

                for (int j = 0; j < (str_buf.Length == 1 ? str_buf.Length : str_buf.Length - 1); j++)
                {
                    if (str_buf[j].Contains("Location:"))
                    {
                        
                        string[] str_temp = str_buf[j].Split(' ');

                        AMR_X = int.Parse(str_temp[1]);
                        AMR_Y = int.Parse(str_temp[2]);

                        Set_map_location();
                        //Set_AREA();
                        //Set_Traffic_AREA();
                        //What_Vator();
                    }
                    else if (str_buf[j].Contains("password"))
                    {                        
                        Send(AMR_PW);

                        AMR_IS_CON = true;

                        //b_amr_t = true;
                        //bg_AMR.RunWorkerAsync();
                    }
                    else if (str_buf[j].Contains("Status:"))
                    {
                        string[] str_temp = str_buf[j].Split(' ');
                        AMR_STATUS = str_temp[1];
                    }
                    else if (str_buf[j].Contains("Welcom") == true)
                    {
                        b_amr_t = true;

                        if(bg_AMR.IsBusy == false)
                            bg_AMR.RunWorkerAsync();
                    }
                }
            }
            catch (SocketException e)
            {
                if(e.SocketErrorCode == SocketError.ConnectionReset)
                {
                   
                }
            }
        }

        private DateTime Vator_cnt_timer = DateTime.Now;

        public void Set_AMR_NAME(string NAME)
        {
            AMR_NAME = NAME;
        }

        public string Get_AMR_NAME()
        {
            return AMR_NAME;
        }

        public void Set_AMR_IP(IPAddress IP)
        {
            AMR_IP = IP;
        }
                
        public IPAddress Get_AMR_IP()
        {
            return AMR_IP;
        }

        public void Set_AMR_PORT(int port)
        {
            AMR_PORT = port;
        }
        
        public void Set_AMR_PW(string PW)
        {
            AMR_PW = PW;
        }
        

        public string Get_AMR_AREA()
        {
            string temp = string.Format("{0},{1},{2}", AMR_AREA, AMR_X, AMR_Y);
            return temp;
        }
        
        public string Get_AMR_Status()
        {
            return AMR_STATUS;
        }


        public string Get_Traffic_AREA()
        {
            return TRAFFIC_AREA;
        }

        public string Get_MOVE()
        {
            return X_Move + "," + Y_Move;
        }

        public void set_offset(Point p)
        {
            map_offset = p;
        }

        public void set_pic(PictureBox box)
        {
            pic_amr.Size = new Size(30,30);
            pic_amr.BackColor = Color.Transparent;
            pic_amr.Parent = box;
            pic_amr.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_amr.Image = Image.FromFile(Application.StartupPath + "\\IMAGE\\" + AMR_NAME + ".png");
            //add_amr_event(pic_amr);
            
        }

        public string get_map_loc()
        {
            Point map_location = new Point((int)(back_img_size.Width * map_location_per_x), (int)(back_img_size.Height * map_location_per_y));
            string loc = string.Format("{0},{1}", map_location.X, map_location.Y);

            return loc;
        }

        public Point get_offset()
        {
            return map_offset;
        }

        public void move(Rectangle rect)
        {
            try
            {
                Point map_location = new Point((int)(back_img_size.Width * map_location_per_x), (int)(back_img_size.Height * map_location_per_y));

                //if (AMR_NAME == "AMT2")
                //{
                //    Console.WriteLine(string.Format("Orignal : {0},{1}", map_location.X, map_location.Y));
                //}

                if(map_location.X > rect.X && map_location.X < (rect.X + rect.Width))
                {
                    if (map_location.Y > rect.Y && map_location.Y < (rect.Y + rect.Height))
                    {
                        map_location.X += map_offset.X;
                        map_location.Y += map_offset.Y;

                        pic_amr.Location = new System.Drawing.Point(((int)(((float)(map_location.X - rect.X) / (float)rect.Width) * pic_size.Width) - (pic_amr.Size.Width / 2)), ((int)(((float)(map_location.Y - rect.Y) / (float)rect.Height) * pic_size.Height) - (pic_amr.Size.Height / 2)));

                        //if (AMR_NAME == "AMT2")
                        //{
                        //    Console.WriteLine(string.Format("OFFSET : {0},{1}", map_location.X + map_offset.X, map_location.Y + map_offset.Y));
                        //}


                        visible(true);
                    }
                    else
                    {
                        visible(false);
                    }
                }
                else
                {
                    visible(false);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void visible(bool b)
        {
            pic_amr.Visible = b;
        }

        bool is_agv_img_added = false;

        public void add_control()
        {
            add_amr_event(pic_amr);
            pic_amr.BringToFront();
            is_agv_img_added = true;
        }

        public void set_amr_img()
        {
            pic_amr.Image = Image.FromFile(Application.StartupPath + "\\IMAGE\\" + AMR_NAME + ".png");
        }

        public string GetAMRName()
        {
            return AMR_NAME;
        }
    }
}
