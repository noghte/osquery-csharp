using System;
using Thrift.Protocol;
using Thrift.Transport;

using System.Net.Sockets;

namespace osquery_csharp.osquery
{
public sealed class ClientManager {
	/**
	 * Default Unix domain socket.
	 */
	public static sealed string DEFAULT_SOCKET_PATH = "/var/osquery/osquery.em";
	public static sealed string SHELL_SOCKET_PATH = System.getProperty("user.home") + "/.osquery/shell.em";
	
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
		//AFUNIXSocket socket = AFUNIXSocket.connectTo(new AFUNIXSocketAddress(new File(socketPath)));
		//this.transport = new TIOStreamTransport(socket.getInputStream(), socket.getOutputStream());
        //TODO: set inputStream and outputStream
        this.transport = new TStreamTransport(null,null);
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