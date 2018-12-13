using System;
using osquery_csharp.osquery;
using System.Collections;
using System.Collections.Generic;

namespace osquery_csharp.plugins
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running C# binding for osquery...");
            try
            {
                BasePlugin plugin = new MyTablePlugin();
                PluginManager pm = PluginManager.getInstance();
                pm.addPlugin(plugin);
                pm.startExtension("MyTablePlugin", "0.0.1", "3.2.6", "3.2.6");
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message ?? "no error data found");
            }

        }
    }
}
