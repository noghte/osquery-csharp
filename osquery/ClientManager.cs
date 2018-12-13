using System;
using Thrift.Protocol;
using Thrift.Transport;

using System.Net.Sockets;
using System.Net;

namespace osquery_csharp.osquery
{
public sealed class ClientManager {
	/**
	 * Default Unix domain socket.
	 */
	public static string DEFAULT_SOCKET_PATH = "/var/osquery/osquery.em";
	public static string SHELL_SOCKET_PATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/.osquery/shell.em";
	
	private string socketPath = null;
	private TTransport transport;
	private TProtocol protocol;
	
	/**
	 * Default  constructor.
	 * @throws IOException 
	 */
	public ClientManager():this(ClientManager.DEFAULT_SOCKET_PATH)  {
        
	}
	
	/**
	 * Constructor with given socket path
	 * @param socketPath
	 * @throws IOException
	 */
	public ClientManager(string socketPath) {
		this.socketPath = socketPath;
		//lines below taken from https://stackoverflow.com/a/40203940/87088
		var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
		
		var unixEp = new UnixEndPoint(socketPath);
		socket.Connect(unixEp);
		NetworkStream oss = new NetworkStream(socket);
		NetworkStream iss = new NetworkStream(socket); 
		
		//below lines translated from:
		//AFUNIXSocket socket = AFUNIXSocket.connectTo(new AFUNIXSocketAddress(new File(socketPath)));
		//this.transport = new TIOStreamTransport(socket.getInputStream(), socket.getOutputStream());
        this.transport = new TStreamTransport(iss,oss);
		this.protocol = new TBinaryProtocol(transport);
	}
	
	/**
	 * Opens transpot for reading and writing.
	 * @throws TTransportException
	 */
	public void open() {
		this.transport.Open();
	}
	
	/**
	 * Closes transport.
	 */
	public void close() {
		if(this.transport != null) {
			this.transport.Close();
		}
	}

	/**
	 * Client factory method.
	 * @return ExtensionManager.Client
	 * @throws IOException if I/O exception occurs
	 */
	public ExtensionManager.Client getClient() {
		return new ExtensionManager.Client(protocol);
	}

}
}