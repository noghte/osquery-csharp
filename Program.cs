using System;
using osquery_csharp.osquery;

namespace osquery_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running C# binding for osquery...");
            BasePlugin plugin = new MyTablePlugin();
            PluginManager pm = PluginManager.getInstance();
            pm.addPlugin(plugin);
            pm.startExtension("MyTablePlugin","0.0.1","2.2.1","2.2.1");
        }
    }
}
