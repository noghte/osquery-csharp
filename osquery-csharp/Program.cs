using System;
using osquery_csharp.osquery;
using System.Collections;
using System.Collections.Generic;

namespace osquery_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running C# binding for osquery...");
            try
            {
                // BasePlugin plugin = new MyTablePlugin();
                // PluginManager pm = PluginManager.getInstance();
                // pm.addPlugin(plugin);
                // pm.startExtension("MyTablePlugin", "0.0.1", "2.2.1", "2.2.1");

                ClientManager cm = new ClientManager();
                cm.open();
                ExtensionManager.Client client = cm.getClient();
                string query = "SELECT name,description,value FROM osquery_flags;";
                Console.WriteLine($"Result of '{query}'");
                ExtensionResponse res = client.query(query);
                foreach (var listItem in res.Response)
                {
                    foreach (var item in listItem)
                        Console.WriteLine(item.Key + ":" + item.Value);
                    Console.WriteLine("-------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message ?? "no error data found");
            }

        }
    }
}
