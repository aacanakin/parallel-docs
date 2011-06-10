using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ParallelDocs.Logic;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace ParallelDocs.GUI
{
	public partial class mainScreen : Form
	{
		delegate void UpdateChatBoxCallBack(string message);
		UpdateChatBoxCallBack updateChatBoxCallBack;
		delegate void RequestShowCallBack(string ip, string port, string msg, string owner, string doc_name);
		RequestShowCallBack requestShowCallBack;
		delegate void RequestResultShowCallBack(string editorName, string ip, string message);
		RequestResultShowCallBack requestResultShowCallBack;
		delegate void UpdateDocumentBoxCallBack();
		UpdateDocumentBoxCallBack updateDocumentBoxCallBack;
		delegate void UpdateDocumentTextCallBack();
		UpdateDocumentTextCallBack updateDocumentTextCallBack;
		delegate void UpdateFontCallBack(string font);
		UpdateFontCallBack updateFontCallBack;


		//private string st1, st2;

		public mainScreen()
		{
			InitializeComponent();
			updateChatBoxCallBack = new UpdateChatBoxCallBack(updateChatBox);
			requestShowCallBack = new RequestShowCallBack(requestShow);
			requestResultShowCallBack = new RequestResultShowCallBack(requestResultShow);
			updateDocumentBoxCallBack = new UpdateDocumentBoxCallBack(updateDocumentBox);
			updateDocumentTextCallBack = new UpdateDocumentTextCallBack(updateDocumentText);
			updateFontCallBack = new UpdateFontCallBack(updateFont);
		}
		private void updateFont(string font)
		{
			TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
			Font newFont = (Font)tc.ConvertFromString(font);
			DocumentManager.getCurrentDocument().setFont(newFont);
			documentRichTextBox.Font = newFont;
			//Font fontObj = (Font)font;
			//logRichTextBox.Text = "Start Font message\n";
			logRichTextBox.Text = "Font.ToString() : " + newFont.ToString();
			//logRichTextBox.Text = "End Font message\n";
		
		}

		private void updateDocumentBox()
		{
			if (logRichTextBox.InvokeRequired == true)
			{
				this.Invoke(updateDocumentBoxCallBack);
			}
			else
			{					
					
					//update document box
					
					DocumentManager.getCurrentDocument().setIsSaved(false);
					string content = DocumentManager.getCurrentDocument().getDocContent();
					string name = DocumentManager.getCurrentDocument().getDocName();
					string owner = DocumentManager.getCurrentDocument().getOwner();
					string path_name = DocumentManager.getCurrentDocument().getPathName();
					//string content = DocumentManager.getCurrentDocument().getDocContent();
					List<Profile> editors = new List<Profile>();
					editors = DocumentManager.getCurrentDocument().getEditors();
					//update log box 
					logRichTextBox.Text += "Start file document message\n";
					logRichTextBox.Text += "Doc name :" + name + "\n";
					logRichTextBox.Text += "Doc owner :" + owner + "\n";
					logRichTextBox.Text += "Doc path :" + path_name + "\n";
					logRichTextBox.Text += "Doc content :" + content + "\n";
					logRichTextBox.Text += "Editors : ";

					foreach (Profile editor in editors)
					{
						logRichTextBox.Text += " " + editor.getFullName();
					}
					logRichTextBox.Text += "\n";
					logRichTextBox.Text += "End file document message\n";
					logRichTextBox.Text += "Getting the document file\n";
						
					// update the document rich box.
					documentRichTextBox.Text = content;	
				
					// set the labels accordingly
					infoEditorsLabel.Text = "Editors :";

					foreach (Profile editor in editors)
					{
						infoEditorsLabel.Text += " " + editor.getFullName();
					}

					infoOwnerLabel.Text = "Owner : " +  owner;
					//controlEditCheckBox.Checked = false;

					// save the document locally

			}
					
		}

		private void updateDocumentText()
		{
			if (documentRichTextBox.InvokeRequired == true)
			{
				this.Invoke(updateDocumentTextCallBack);
			}
			else
			{

				logRichTextBox.Text +="Start Document Message\n";
				logRichTextBox.Text += "Editors : " + DocumentManager.getCurrentDocument().getEditors().ToString() + "\n"; 
				logRichTextBox.Text += "End Document Message\n";
				
				documentRichTextBox.Text = DocumentManager.getCurrentDocument().getDocContent();
				controlEditCheckBox.Checked = false;

			}
		}

		private void requestResultShow(string editorName, string ip, string message) // owner / server side
		{
			if (logRichTextBox.InvokeRequired == true)
			{
				this.Invoke(requestResultShowCallBack, editorName, ip, message);
			}
			else
			{
				logRichTextBox.Text += "Start Request Result Message\n";
				logRichTextBox.Text += "Message : " + message + "\n";
				logRichTextBox.Text += "Editor Name : " + editorName + "\n";
				logRichTextBox.Text += "Ip : " + ip + "\n";
				logRichTextBox.Text += "End Request Result Message\n";
				string[] messageArr = message.Split(':');
				string result = messageArr[0];

				if (result == "yes")
				{
					//receiveDocFont();
					receiveDocumentMessage();
					DocumentManager.getCurrentDocument().setShared(true);
					ConnectionManager.sendDocumentFile(DocumentManager.getCurrentDocument());
				}

				else
				{ 
					// dont receive
				}

			}
		
		}

		private void requestShow(string receivedIp, string receivedPort, string msg, string owner, string doc_name)
		{
			if (logRichTextBox.InvokeRequired == true)
			{
				this.Invoke(requestShowCallBack, receivedIp, receivedPort, msg, owner, doc_name);	
			}
			else
			{
				//receiveFileDocument();
				logRichTextBox.Text += "Start Request Message\n";
				logRichTextBox.Text += "Message : " + msg + "\n";
				logRichTextBox.Text += "From ip : " + receivedIp + "\n";
				logRichTextBox.Text += "From port : " + receivedPort + "\n";
				logRichTextBox.Text += "Owner : " + owner + "\n";
				logRichTextBox.Text += "Doc Name : " + doc_name + "\n";
				logRichTextBox.Text += "Request message : " + msg + "\n";
				logRichTextBox.Text += "End Request Message\n";
				
				if (receivedIp == ConnectionManager.getLocalIp())
				{
					// Do not send request in dialog
				}
				else
				{
					if (ConnectionManager.requestFirst)
					{
						DialogResult requestResult = MessageBox.Show(" " + owner + " wants to share document with you. Do you want to accept document share request ?", "Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

						if (requestResult == DialogResult.Yes)
						{
							ConnectionManager.requestResult = true;
							receiveFileDocument();
							//receiveDocFont();
							receiveDocumentMessage();
							ConnectionManager.sendRequestResult("yes");
							// infoOwnerLabel.Text = "Owner : " + owner;
							// send the request result to the send
							// receive the file in port
														
							ConnectionManager.requestFirst = false;
						}
						else
						{
							ConnectionManager.sendRequestResult("no");
							ConnectionManager.requestFirst = true; // after result is no, people can send request again
							// do nothing
						}
					}
					else
					{ 
						// Do not show dialog
					}
				}
			}				
		}

		private void updateChatBox(string message)
		{
			if (chatTextBox.InvokeRequired == true)
			{
				this.Invoke(updateChatBoxCallBack, message);
			}
			else
			{
				message = message.Replace("\0", "");
				chatTextBox.AppendText(message + "\r\n");
			}
		}

		private void editCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (controlEditCheckBox.Checked == true)
			{
				documentRichTextBox.ReadOnly = false;
			}
			else
			{
				documentRichTextBox.ReadOnly = true;
			}
		}
		
		private void mainScreen_Load(object sender, EventArgs e)
		{
			//st1 = "";

			try
			{				
				if (Program.first_run()) // show first run screen
				{
					ProfileManager.init();
					FirstRunScreen firstRunScreen = new FirstRunScreen();
					firstRunScreen.ShowDialog();
					infoProfileLabel.Text = "Profile : " + ProfileManager.getCurrentProfile().getFullName();
					infoOwnerLabel.Text = "Owner : " + ProfileManager.getCurrentProfile().getFullName();

					this.Text = "untitled.txt - Parallel Docs";
					infoHostNameLabel.Text = infoHostNameLabel.Text + " " + ConnectionManager.getLocalName();
					infoIpAddressLabel.Text = infoIpAddressLabel.Text + " " + ConnectionManager.getLocalIp();
					receiveChatMsg();
					receiveRequest();					
					DocumentManager.init();
					//receiveFileDocument();
					//receiveDocumentMessage();
				}
				else
				{
					ProfileManager.loadProfile();
					infoProfileLabel.Text = "Profile : " + ProfileManager.getCurrentProfile().getFullName();
					infoOwnerLabel.Text = "Owner : " + ProfileManager.getCurrentProfile().getFullName();

					this.Text = "untitled.txt - Parallel Docs";
					infoHostNameLabel.Text = infoHostNameLabel.Text + " " + ConnectionManager.getLocalName();
					infoIpAddressLabel.Text = infoIpAddressLabel.Text + " " + ConnectionManager.getLocalIp();
					receiveChatMsg();
					receiveRequest();
					DocumentManager.init();
					//receiveFileDocument();
					//receiveDocumentMessage();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void mainScreen_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result;
			result = MessageBox.Show(this, "Are You Sure", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				ProfileManager.saveProfile(ProfileManager.getCurrentProfile());
				e.Cancel = false;
			}
			else
			{
				e.Cancel = true;
				// Do Nothing
			}
		}

		private void mainScreen_FormClosed(object sender, FormClosedEventArgs e)
		{

		}

		private void controlFontButton_Click(object sender, EventArgs e)
		{
			DialogResult result;

			result = fontDialog.ShowDialog();

			if (result == DialogResult.OK)
			{
				documentRichTextBox.Font = fontDialog.Font;
				DocumentManager.getCurrentDocument().setFont(fontDialog.Font);
				if (DocumentManager.getCurrentDocument().getShared())
				{
					ConnectionManager.sendFontInfo();
				}
			}

			//if(DocumentManager.getCurrentDocument()

		}

		private void controlNewButton_Click(object sender, EventArgs e)
		{
			DocumentManager.init();
			this.Text = DocumentManager.getCurrentDocument().getDocName();
			documentRichTextBox.Text = "";

		}

		private void controlOpenButton_Click(object sender, EventArgs e)
		{
			DialogResult result;
			result = openFileDialog.ShowDialog();

			if (result == DialogResult.OK)
			{
				documentRichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
				this.Text = "" + openFileDialog.SafeFileName + " - Parallel Docs";
				DocumentManager.getCurrentDocument().setIsSaved(true);
				DocumentManager.getCurrentDocument().setPathName(openFileDialog.FileName);
				DocumentManager.getCurrentDocument().setDocName(openFileDialog.SafeFileName);
			}

		}

		private void controlSaveButton_Click(object sender, EventArgs e)
		{
			if (!DocumentManager.getCurrentDocument().getIsSaved())
			{
				if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName.Length > 0)
				{

					documentRichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
					string cur_path = saveFileDialog.FileName;
					Document cur = DocumentManager.getCurrentDocument();
					cur.setIsSaved(true);
					cur.setPathName(cur_path);
					this.Text = cur.getPathName() + " - Parallel Docs ";
					DocumentManager.setCurrentDocument(cur);

				}

			}

			else
			{

				documentRichTextBox.SaveFile(DocumentManager.getCurrentDocument().getPathName(), RichTextBoxStreamType.PlainText);
			}

		}

		private void controlSaveAsButton_Click(object sender, EventArgs e)
		{

			if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName.Length > 0)
			{
				documentRichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
			}
		}

		private void controlShareButton_Click(object sender, EventArgs e)
		{
			try
			{
				ConnectionManager.sendRequest();
				if (ConnectionManager.requestResultFirst)
				{					
					receiveRequestResult();
					controlEditCheckBox.Checked = false;						
					ConnectionManager.requestResultFirst = false;
					//DocumentManager.getCurrentDocument().shared = true;
					DocumentManager.getCurrentDocument().setDocContent(documentRichTextBox.Text);
															
					//ConnectionManager.sendDocumentFile(DocumentManager.getCurrentDocument());
				}
				MessageBox.Show("Share Request of the current document is sended to the all peers in network!", "Send", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void controlConnectButton_Click(object sender, EventArgs e)
		{

		}

		private void openFileDialog_FileOk(object sender, CancelEventArgs e)
		{

		}
		
		private void chatSendButton_Click(object sender, EventArgs e)
		{
			string message = chatSendTextBox.Text;

			ConnectionManager.sendChatMsg(message);

			chatSendTextBox.Text = "";
		}

		public void receiveChatMsgRun()
		{
			const int chatPort = 15014;

			Socket chatListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			IPEndPoint iepChat = new IPEndPoint(IPAddress.Any, chatPort);
			EndPoint epChat = (EndPoint)iepChat;
			chatListener.Bind(epChat);

			while (true)
			{

				try
				{
					string msg;
					Byte[] receiveBytes = new Byte[1024];
					int recv = chatListener.ReceiveFrom(receiveBytes, ref epChat);
					msg = Encoding.UTF8.GetString(receiveBytes, 0, receiveBytes.Length);
					updateChatBox(msg);
					iepChat = new IPEndPoint(IPAddress.Any, chatPort);
					epChat = (EndPoint)iepChat;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Exception : " + ex);
					break;
				}
			}

		}

		private void receiveChatMsg()
		{
			//after connection initiation,
			//listen the receive port in thread,
			//whenever a message received, return the received chat msg

			// this is the thread function
			try
			{

				Thread t = new Thread(new ThreadStart(receiveChatMsgRun));
				t.IsBackground = true;
				t.Start();


			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception : " + ex);

			}

		}

		private void receiveRequest()
		{
			try
			{
				Thread t = new Thread(new ThreadStart(receiveRequestRun));
				t.IsBackground = true;
				t.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception : " + ex);
			}
		
		}

		private void receiveDocumentMessage() 
		{
			try
			{
				Thread t = new Thread(new ThreadStart(receiveDocumentMessageRun));
				t.IsBackground = true;
				t.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception : " + ex);
			}
		}

		private void receiveDocumentMessageRun()
		{

			Socket fileDocumentListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			EndPoint epDocumentMessage = new IPEndPoint(IPAddress.Any, ConnectionManager.documentMessagePort);
			fileDocumentListener.Bind(epDocumentMessage);
			//IFormatter bf = new BinaryFormatter();

			while (true)
			{
				try
				{
					//string msg;
					Byte[] receiveBytes = new Byte[1024 * 50000];
					//MemoryStream ms = new MemoryStream();

					int recv = fileDocumentListener.ReceiveFrom(receiveBytes, ref epDocumentMessage);
					string doc_content = Encoding.UTF8.GetString(receiveBytes);					
					DocumentManager.getCurrentDocument().setDocContent(doc_content);
					string ip;
					string[] epArr = epDocumentMessage.ToString().Split(':');
					ip = epArr[0];
					// update document Rich box
					if(ip != ConnectionManager.getLocalIp())
						updateDocumentText();

				}
				catch (Exception ex)
				{
					MessageBox.Show("Exception : " + ex);
				}
			}
			//Socket documentMessageListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			//EndPoint epDocumentMessage = new IPEndPoint(IPAddress.Any, ConnectionManager.documentMessagePort);
			//documentMessageListener.Bind(epDocumentMessage);			

			//while (true)
			//{
			//    try
			//    {
			//        //string msg;
			//        Byte[] receiveBytes = new Byte[1024 * 50000];					

			//        int recv = documentMessageListener.ReceiveFrom(receiveBytes, ref epDocumentMessage);
			//        string doc_msg = Encoding.UTF8.GetString(receiveBytes);
			//        doc_msg = doc_msg.Replace("\0", "");
			//        string[] doc_msg_arr = doc_msg.Split(':');
			//        int start = int.Parse(doc_msg_arr[0]);
			//        string text = doc_msg_arr[1];
			//        //text = text.Replace("\0", "");
					
			//        // update document Rich box
			//        string ip;					
			//        string[] epArr = epDocumentMessage.ToString().Split(':');
			//        ip = epArr[0];
			//        if(ip != ConnectionManager.getLocalIp())
			//            updateDocumentText(start, text);

			//    }
			//    catch (Exception ex)
			//    {
			//        MessageBox.Show("Exception : " + ex);
			//    }
			//}
		
		}

		private void receiveRequestResult()
		{ 
			try
			{
				Thread t = new Thread(new ThreadStart(receiveRequestResultRun));
				t.IsBackground = true;
				t.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception : " + ex);
			}
				
		}

		private void receiveFileDocument()
		{
			try
			{
				Thread t = new Thread(new ThreadStart(receiveFileDocumentRun));				
				t.IsBackground = true;				
				t.Start();
				ConnectionManager.receiverStartedFlag = true;
				//t.Join();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception : " + ex);
			}
		
		}

		private void receiveFileDocumentRun()
		{
			const int fileDocumentPort = 10023;
								
			Socket fileDocumentListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			EndPoint epFileDocument = new IPEndPoint(IPAddress.Any, fileDocumentPort);
			fileDocumentListener.Bind(epFileDocument);
			IFormatter bf = new BinaryFormatter();		
			
			while (true)
			{
				try
				{
					//string msg;
					Byte[] receiveBytes = new Byte[1024 * 50000];
					MemoryStream ms = new MemoryStream();
					
					int recv = fileDocumentListener.ReceiveFrom(receiveBytes, ref epFileDocument);
					
					ms.Write(receiveBytes, 0, receiveBytes.Length);
					ms.Seek(0, SeekOrigin.Begin);
					DocumentManager.setCurrentDocument((Document)bf.Deserialize(ms));
					
					// update document Rich box 
					updateDocumentBox();

				}
				catch (Exception ex)
				{
					MessageBox.Show("Exception : " + ex);
				}
			}
						
		}

		private void receiveDocFont() {

			try
			{
				Thread t = new Thread(new ThreadStart(receiveDocFontRun));
				t.IsBackground = true;
				t.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception : " + ex);
			}
		}

		private void receiveDocFontRun() {

			const int docFontPort = 10024;

			Socket fontListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			IPEndPoint iepDocFont = new IPEndPoint(IPAddress.Any, docFontPort);
			EndPoint epDocFont = (EndPoint)iepDocFont;
			fontListener.Bind(epDocFont);

			while (true)
			{

				try
				{
					string font;
					Byte[] receiveBytes = new Byte[1024];
					int recv = fontListener.ReceiveFrom(receiveBytes, ref epDocFont);
					font = Encoding.UTF8.GetString(receiveBytes, 0, receiveBytes.Length);					
					updateFont(font);
					iepDocFont = new IPEndPoint(IPAddress.Any, docFontPort);
					epDocFont = (EndPoint)iepDocFont;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Exception : " + ex);
					break;
				}
			}
		
		}

		private void receiveRequestResultRun()
		{
			const int requestResultPort = 15041;
			Socket requestResultListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			//IPEndPoint iepRequest = new IPEndPoint(IPAddress.Any, shareRequestPort);
			//EndPoint epRequest = (EndPoint)iepRequest;
			EndPoint epRequestResult = new IPEndPoint(IPAddress.Any, requestResultPort);
			requestResultListener.Bind(epRequestResult);

			while (true)
			{
				try
				{
					string msg;
					Byte[] receiveBytes = new Byte[1024];
					int recv = requestResultListener.ReceiveFrom(receiveBytes, ref epRequestResult);
					msg = Encoding.UTF8.GetString(receiveBytes, 0, receiveBytes.Length);
					msg = msg.Replace("\0", "");
					string[] ipPort = epRequestResult.ToString().Split(':');
					string receivedIp = ipPort[0];
					string receivedPort = ipPort[1];
					string[] msgArr = msg.Split(':');
					string result = msgArr[0];
					string editorName = msgArr[1];

					if (result == "yes")
					{
						string editorIp = receivedIp;						
						Profile editor = new Profile(editorName,"",editorIp);
						DocumentManager.getCurrentDocument().addEditor(editor);						
						requestResultShow(editorName,editorIp,msg);
					}
					else
					{ 
						// Do nothing
					}										
				}
				catch (Exception ex)
				{
					MessageBox.Show("Exception : " + ex);
					break;
				}
			}		
		}

		private void receiveRequestRun()
		{
			const int shareRequestPort = 15003;

			Socket requestListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			//IPEndPoint iepRequest = new IPEndPoint(IPAddress.Any, shareRequestPort);
			//EndPoint epRequest = (EndPoint)iepRequest;
			EndPoint epRequest = new IPEndPoint(IPAddress.Any, shareRequestPort);
			requestListener.Bind(epRequest);

			while (true)
			{
				try
				{
					string msg;
					Byte[] receiveBytes = new Byte[1024];
					int recv = requestListener.ReceiveFrom(receiveBytes, ref epRequest);					
					msg = Encoding.UTF8.GetString(receiveBytes, 0, receiveBytes.Length);
					msg = msg.Replace("\0", "");
					string[] msgOwnerName = msg.Split(':');
					string owner = msgOwnerName[0];
					string doc_name = msgOwnerName[1];
					string[] ipPort = epRequest.ToString().Split(':');
					string receivedIp = ipPort[0];
					string receivedPort = ipPort[1];
					requestShow(receivedIp, receivedPort, msg, owner, doc_name);	
					
				}
				catch (Exception ex)
				{
					MessageBox.Show("Exception : " + ex);
					break;
				}
			}
		
		}	

		private void infoRefreshButton_Click(object sender, EventArgs e)
		{
			string editorsString = "";
			foreach (Profile editor in DocumentManager.getCurrentDocument().getEditors())
			{
				editorsString += editor.getFullName() + "," ;			
			}
			infoNoneLabel.Text = editorsString;

			infoIpAddressLabel.Text = "IP : " + ConnectionManager.getLocalIp();
		}

		private void documentRichTextBox_TextChanged(object sender, EventArgs e)
		{
			if (DocumentManager.getCurrentDocument().getShared() && (controlEditCheckBox.Checked))
			{
				DocumentManager.getCurrentDocument().setDocContent(documentRichTextBox.Text);
				ConnectionManager.sendDocumentMessage(DocumentManager.getCurrentDocument().getDocContent());
			}		
					
		}

		private void documentRichTextBox_SelectionChanged(object sender, EventArgs e)
		{
			
		}




	}
}
