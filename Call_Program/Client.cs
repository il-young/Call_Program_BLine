using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.ComponentModel;


// 원격 장치로부터 데이터를 수신하는 상태 객체.
public class StateObject
{
    // Client socket.
    public Socket workSocket = null;
    // Size of receive buffer.
    public const int BufferSize = 1024;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];
    // Received data string.
    public StringBuilder sb = new StringBuilder();
}

public class AsynchronousClient
{
    // 원격 장치의 포트 번호.
    private static int port = 11000;
    private static IPAddress Target_IP;

    // ManualResetEvent 인스턴스가 완료되었음을 알립니다.
    private static ManualResetEvent connectDone =
        new ManualResetEvent(false);
    private static ManualResetEvent sendDone =
        new ManualResetEvent(false);
    private static ManualResetEvent receiveDone =
        new ManualResetEvent(false);

    // 원격 장치에서 응답.
    private static String response = String.Empty;
    private BackgroundWorker bgw_Receive = new BackgroundWorker();
    private static Socket client;

    public AsynchronousClient()
    {
        bgw_Receive.DoWork += Bgw_Receive_DoWork; 
    }

    private void Bgw_Receive_DoWork(object sender, DoWorkEventArgs e)
    {
        while(true)
        {
            // 원격 장치로부터 응답을 수신합니다.

            if (client.Connected == true)
            {
                Receive(client);
                
            }

            System.Threading.Thread.Sleep(100);
        }
    }

    private static void StartClient()
    {
        // 원격 장치에 연결합니다.
        try
        {
            // 소켓의 원격 끝점을 설정합니다.
            // 원격 장치는 "host.contoso.com"입니다.
            //IPHostEntry ipHostInfo = Dns.Resolve("host.contoso.com");
            IPAddress ipAddress = Target_IP;
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP socket.
            client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect to the remote endpoint.
            client.BeginConnect(remoteEP,
                new AsyncCallback(ConnectCallback), client);
            

            // 원격 장치에 대한 테스트 데이터를 전송합니다.
            //Send(client, "This is a test<EOF>");
            //sendDone.WaitOne();

            

            // 콘솔에 대한 응답을 작성합니다.
            //Console.WriteLine("Response received : {0}", response);

            // Release the socket.
            //client.Shutdown(SocketShutdown.Both);
            //client.Close();

            

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            // 상태 개체에서 소켓을 검색합니다.
            Socket client = (Socket)ar.AsyncState;

            // 연결을 완료합니다.
            client.EndConnect(ar);

            Console.WriteLine("Socket connected to {0}",
                client.RemoteEndPoint.ToString());

            // 연결이 된 것을 알려줍니다.
            connectDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void Receive(Socket client)
    {
        try
        {
            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = client;

            // 원격 장치로부터 데이터를 수신하기 시작한다.
            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReceiveCallback), state);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            // 상태 개체와 클라이언트 소켓을 취득합니다
            // 비동기 상태 개체에서.
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;

            // 원격 장치에서 데이터를 읽습니다.
            int bytesRead = client.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There might be more data, so store the data received so far.
                // 더 많은 데이터가 될 수 있으므로, 지금까지 수신 된 데이터를 저장할 수도있다.
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                // 나머지 데이터를 가져옵니다.
                //client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                //    new AsyncCallback(ReceiveCallback), state);

                //Call_Program.Form1.SRV_Parse(state.sb.ToString());
            }
            else
            {
                
                if (state.sb.Length > 1)
                {
                    response = state.sb.ToString();
                }
                // 모든 바이트가 수신 된 신호.
                receiveDone.Set();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void Send(Socket client, String data)
    {
        // ASCII 인코딩을 사용하여 바이트의 데이터를 문자열 데이터를 변환합니다.
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // 원격 장치에 데이터를 보내기 시작.
        client.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), client);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // 상태 개체에서 소켓을 검색합니다.
            Socket client = (Socket)ar.AsyncState;

            // 원격 장치에 데이터를 송신 완료.
            int bytesSent = client.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to server.", bytesSent);

            // 모든 바이트가 전송 된 것을 신호.
            sendDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }



    /*********************************************************************************************/

    public void Set_Port(int nport)
    {
        port = nport;
    }

    public void Set_IP(string IP)
    {
        Target_IP = IPAddress.Parse(IP);
    }

    public void Start()
    {
        StartClient();

        if(bgw_Receive.IsBusy == false)
            bgw_Receive.RunWorkerAsync();
    }

    public void Send_Data(String data)
    {
        Send(client, data);
    }
    public bool Is_Connected()
    {
        return client.Connected;
    }

    public void Connect()
    {
        IPAddress ipAddress = Target_IP;
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

        // Create a TCP/IP socket.
        client = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        // Connect to the remote endpoint.
        client.BeginConnect(remoteEP,
            new AsyncCallback(ConnectCallback), client);
    }

    public void Close()
    {
        client.Close();
    }
}


    