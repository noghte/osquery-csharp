using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Thrift;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;


namespace osquery_csharp.osquery
{
    public class PluginManager : ExtensionManager.Iface
    {
        //string someValue =  Configuration.GetValue<string>("extension.socke:SomeValue");
        // public static string EXTENSION_SOCKET = System.getProperty("extension.socket") != null
        //  	? System.getProperty("extension.socket") : ClientManager.DEFAULT_SOCKET_PATH;
        public static string EXTENSION_SOCKET = ClientManager.DEFAULT_SOCKET_PATH; //"/home/saber/.osquery/shell.em";

        public List<string> registryTypes = new List<string>(new string[] { "config", "logger", "table" });

        /**
         * Registered plugins.
         */
        private Dictionary<string, Dictionary<string, BasePlugin>> plugins = new Dictionary<string, Dictionary<string, BasePlugin>>();
        /**
         * Extension registry.
         */
        private Dictionary<string, Dictionary<string, List<Dictionary<string, string>>>> registry = new Dictionary<string, Dictionary<string, List<Dictionary<string, string>>>>();

        /**
         * UUID
         */
        private long? uuid = null;

        /**
         * Default private constructor.
         */
        private PluginManager()
        {
        }

        /**
         * Lazy on demand singleton holder.
         */
        private static class SingletonHolder
        {
            /**
             * Instance of ExtensionManager
             */
            public static PluginManager INSTANCE = new PluginManager();
        }

        /**
         * Get EtensionManager instance.
         * 
         * @return ExtensionManager
         */
        public static PluginManager getInstance()
        {
            return SingletonHolder.INSTANCE;
        }

        /**
         * OSQuery ping method.
         * 
         * @throws TException
         * @see osquery.extensions.Extension.Iface#ping()
         */
        public ExtensionStatus ping()
        {
            ExtensionStatus status = new ExtensionStatus
            {
                Code = 0,
                Message = "OK",
                Uuid = uuid != null ? uuid.Value : 0L
            };
            return status;
        }
        public ExtensionResponse call(string registry, string item, Dictionary<string, string> request)
        {
            if (!registryTypes.Contains(registry))
            {
                var res = new ExtensionResponse
                {
                    Status = new ExtensionStatus { Code = 1, Message = "A registry of an unknown type was called: " + registry, Uuid = uuid.Value },
                    Response = null// new ArrayList<Dictionary<string, string>>();
                };
                return res;
            }
            return plugins[registry][item].call(request);
        }

        public ExtensionStatus deregisterExtension(long uuid)
        {
            ExtensionManager.Client client = new ClientManager().getClient();
            if (uuid == -1)
            {

                throw new ExtensionException { Code = 1, Message = "Extension Manager does not have a valid UUID", Uuid = uuid };
            }
            try
            {
                ExtensionStatus status = client.deregisterExtension(uuid);
                if (status.Code != 0)
                {
                    throw new ExtensionException { Code = 1, Message = status.Message, Uuid = uuid };
                }
                return status;
            }
            catch (TException e)
            {
                throw new ExtensionException { Code = 1, Message = "Could not connect to socket", Uuid = uuid };
            }

        }

        public Dictionary<long, InternalExtensionInfo> extensions()
        {
            throw new NotImplementedException();
        }

        public ExtensionResponse getQueryColumns(string sql)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, InternalOptionInfo> options()
        {
            throw new NotImplementedException();
        }

        public ExtensionResponse query(string sql)
        {
            throw new NotImplementedException();
        }

        public ExtensionStatus registerExtension(InternalExtensionInfo info, Dictionary<string, Dictionary<string, List<Dictionary<string, string>>>> registry)
        {
             throw new NotImplementedException();
        }

        public void shutdown()
        {
            Environment.Exit(0);
        }
        public void addPlugin(BasePlugin plugin)
        {
            if (!registry.ContainsKey(plugin.registryName()))
            {
                registry.Add(plugin.registryName(), new Dictionary<string, List<Dictionary<string, string>>>());
            }
            if (!registry[plugin.registryName()].ContainsKey(plugin.name()))
            {
                registry[plugin.registryName()].Add(plugin.name(), new List<Dictionary<string, string>>());
                registry[plugin.registryName()][plugin.name()].AddRange(plugin.routes());
            }

            if (!plugins.ContainsKey(plugin.name()))
            {
                plugins.Add(plugin.registryName(), new Dictionary<string, BasePlugin>());
            }
            if (!plugins[plugin.registryName()].ContainsKey(plugin.name()))
            {
                plugins[plugin.registryName()].Add(plugin.name(), plugin);
            }

        }
        public void startExtension(string name, string version, string sdkVersion, string minSdkVersion)
        {
            ExtensionManager.Client client = new ClientManager(EXTENSION_SOCKET).getClient();
            InternalExtensionInfo info = new InternalExtensionInfo { Name = name, Version = version, Sdk_version = sdkVersion, Min_sdk_version = minSdkVersion };
            try
            {
                client.OutputProtocol.Transport.Open();
                ExtensionStatus status = client.registerExtension(info, registry);
                if (status.Code == 0)
                {
                    this.uuid = status.Uuid;
                    var processor = new ExtensionManager.Processor(this);
                    String serverSocketPath = EXTENSION_SOCKET + "." + uuid.ToString();

                    if (File.Exists(serverSocketPath))
                        File.Delete(serverSocketPath);
                    //TODO: uncomment below
                    
                    //var socket1 = new UnixEndPoint(EXTENSION_SOCKET);
                    var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
                                     
                    //var listener = new TcpListener(IPAddress.Any, 9090);
                    //TServerSocket transport = new TServerSocket(9090);
                    TTransportFactory transportFactory = new TTransportFactory();
                    TProtocolFactory protocolFactory = new TBinaryProtocol.Factory();
                   
                    TServerTransport serverTransport =  new TNamedPipeServerTransport(serverSocketPath); 
                    TTransport trans = client.InputProtocol.Transport;
                    TServer server = new TSimpleServer(processor,serverTransport,transportFactory,protocolFactory);
                    server.Serve(); //Starting the server

                }
                else
                {
                    throw new ExtensionException { Code = 1, Message = status.Message, Uuid = uuid ?? -1 };
                }
            }
            catch (TException e)
            {
                throw new ExtensionException { Code = 1, Message = "Could not connect to socket", Uuid = uuid ?? -1 };
            }
        }
    }
}