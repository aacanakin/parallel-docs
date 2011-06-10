using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;

using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.ComponentModel;
using System.Drawing;

namespace ParallelDocs.Logic
{
    static class ConnectionManager
    {
        private const int chatPort = 15014;       
        private const int dataPort = 12000;
        private const int documentPort = 13000;
		private const int shareRequestPort = 15003;
		private const int requestResultPort = 15041;
		public const int fileDocumentPort = 10023;
		public const int documentMessagePort = 15123;
		public const int docFontPort = 10024;

		public static bool receiverStartedFlag = false;	
		public static bool requestResult = false;
		public static bool requestResultFirst = true;
		public static bool requestFirst = true;
		public static string ownerIp = "";
        
        public static string getLocalName()
        {
            string name = Dns.GetHostName();
            return name;
        }

        public static string getLocalIp()
        {
            string ip = null;

            string hostname = getLocalName();
            IPHostEntry iph = Dns.GetHostEntry(hostname);

            foreach (IPAddress ipAdd in iph.AddressList)
            {

                if (ipAdd.AddressFamily.ToString() == "InterNetwork")
                {
                    ip = ipAdd.ToString();                
                }
            
            }

            return ip;
        }

        public static bool startChatConnection(string ipAddress) 
        {
            //Socket s = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);
            
            try
            {
                
                IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                return true;                                           
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.ToString());
                return false;
            }

        }

        public static void startDataConnection()
        {
            // initiate the data connection
            // set the port and sockets
            // send the document by using the wireless network
        }

		public static void sendRequestResult(string result)
		{ 
			Socket requestResultSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			try
			{
				IPEndPoint requestResultGroupEp = new IPEndPoint(IPAddress.Broadcast, requestResultPort);
				
				requestResultSender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
				string request_result_message = result + ":" + ProfileManager.getCurrentProfile().getFullName(); // document name
				byte[] sendBytes = Encoding.UTF8.GetBytes(request_result_message);
				requestResultSender.SendTo(sendBytes, requestResultGroupEp);
			}
			catch (Exception e)
			{

				MessageBox.Show("Exception : " + e);
			}
				
		}

		public static void sendRequest() {

			Socket requestSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			try
			{
				IPEndPoint requestGroupEp = new IPEndPoint(IPAddress.Broadcast, shareRequestPort);				
				requestSender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
				string request_message = ProfileManager.getCurrentProfile().getFullName() + ":" + DocumentManager.getCurrentDocument().getDocName();// document name
				byte[] sendBytes = Encoding.UTF8.GetBytes(request_message);
				requestSender.SendTo(sendBytes, requestGroupEp);
			}
			catch (Exception e)
			{

				MessageBox.Show("Exception : " + e);
			}
		
		}
        
        public static void sendChatMsg( string message )
        {
            //after the connection init,
            //send a string message by using chat port
            //
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            
            try
            {
                IPEndPoint groupEp = new IPEndPoint(IPAddress.Broadcast, chatPort);
                               
                //IPEndPoint groupEp = new IPEndPoint(IPAddress.Parse("255.255.255.255"), groupPort);
                //IPEndPoint groupEp = new IPEndPoint(IPAddress.Broadcast, chatPort);
                
                sender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
				message = ProfileManager.getCurrentProfile().getFullName() + " : " + message;
                byte[] sendBytes = Encoding.UTF8.GetBytes(message);
                sender.SendTo(sendBytes, groupEp);
                            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex);
            }
            finally {

                //sender.Close();    
            }

        }

		public static void sendDocumentFile(Document d)
		{
			Socket documentFileSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);			

			try
			{
				
				IPEndPoint documentFileGroupEp = new IPEndPoint(IPAddress.Broadcast, fileDocumentPort);
				documentFileSender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
				BinaryFormatter bf = new BinaryFormatter(); 
				IFormatter formatter = new BinaryFormatter();
				MemoryStream stream = new MemoryStream();
				formatter.Serialize(stream, d);
				Byte[] buffer = stream.ToArray();
				stream.Close();
				documentFileSender.SendTo(buffer, documentFileGroupEp);
				MessageBox.Show("Buffer Length : " + buffer.Length);
				
			}
			catch (Exception e)
			{

				MessageBox.Show("Exception : " + e);
			}
		
		}
               
        public static void sendDocumentMessage(string doc_content) 
        {

			Socket documentMessageSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			try
			{
				IPEndPoint documentMessageGroupEp = new IPEndPoint(IPAddress.Broadcast, documentMessagePort);
				documentMessageSender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
				Byte[] buffer = Encoding.UTF8.GetBytes(doc_content);
				documentMessageSender.SendTo(buffer, documentMessageGroupEp);
				//MessageBox.Show("Buffer Length : " + buffer.Length);
				//IPEndPoint documentFileGroupEp = new IPEndPoint(IPAddress.Broadcast, documentMessagePort);
				//documentFileSender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
				//BinaryFormatter bf = new BinaryFormatter();
				//IFormatter formatter = new BinaryFormatter();
				//MemoryStream stream = new MemoryStream();
				//formatter.Serialize(stream, d);

				//Byte[] buffer = stream.ToArray();
				//stream.Close();
				//documentFileSender.SendTo(buffer, documentFileGroupEp);
				//MessageBox.Show("Buffer Length : " + buffer.Length);

			}
			catch (Exception e)
			{

				MessageBox.Show("Exception : " + e);
			}
			//ConnectionManager.sendDocumentFile(DocumentManager.getCurrentDocument());
			//Socket documentMessageSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			//try
			//{

			//    IPEndPoint documentMessageGroupEp = new IPEndPoint(IPAddress.Broadcast, documentMessagePort);
			//    documentMessageSender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

			//    Byte[] buffer = Encoding.UTF8.GetBytes(dm);				
			//    documentMessageSender.SendTo(buffer, documentMessageGroupEp);
			//    //MessageBox.Show("Buffer Length : " + buffer.Length);
			//}
			//catch (Exception e)
			//{

			//    MessageBox.Show("Exception : " + e);
			//}

            //whenever any key pressed send the document message to the buffer 
            //get the char start and end position
            //make a byte buffer and send it through the send port
                
        }

		public static void sendFontInfo()
		{
			Socket docFontSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			try
			{
				TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
				string fontString = tc.ConvertToString(DocumentManager.getCurrentDocument().getFont());

				IPEndPoint docFontGroupEp = new IPEndPoint(IPAddress.Broadcast, docFontPort);
				docFontSender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
				Byte[] buffer = Encoding.UTF8.GetBytes(fontString);
				docFontSender.SendTo(buffer, docFontGroupEp);

			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception : " + ex);
			}

		}


        public static IPAddress[] getAllUnicastAddresses() 
        {

            List<IPAddress> ips = new List<IPAddress>();
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in adapters)
            {
                    if (adapter.OperationalStatus == OperationalStatus.Up)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        
                        foreach (IPAddressInformation unicast in properties.UnicastAddresses)
                        {
                            if ((!IPAddress.IsLoopback(unicast.Address)) && (unicast.Address.AddressFamily != AddressFamily.InterNetworkV6))
                            {
                                ips.Add(unicast.Address);
                            }
                        }
                    }
            }
            
            return ips.ToArray();
                       
        }

        public static PhysicalAddress[] getAdaptersMacAddress() 
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            List<PhysicalAddress> macs = new List<PhysicalAddress>();

            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up)
                {
                                    
                    PhysicalAddress mac_address_physical = adapter.GetPhysicalAddress();

                    macs.Add(mac_address_physical);

                }
            }

            return macs.ToArray<PhysicalAddress>();
        }

    }
}
