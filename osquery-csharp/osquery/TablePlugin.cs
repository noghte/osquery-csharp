using System;
using System.Collections.Generic;
using System.Linq;
using Thrift;


namespace osquery_csharp.osquery
{
public abstract class TablePlugin: BasePlugin {
        public class TableColumn<T,V> : Dictionary<string, string>
        {
            /**
             * Default constructor
             * 
             * @param key
             * @param value
             */
            public TableColumn(string key, string value) {
                base.Add(key,value);

            }
            public TableColumn():base()
            {
            }
        }

        /**
         * Registry name.
         * 
         * @see net.melastmohican.osquery.BasePlugin#registryName()
         */

        public override string registryName() {
		return "table";
	}

	/**
	 * Internal routing for this plugin type
	 * 
	 * @see net.melastmohican.osquery.BasePlugin#call(java.util.Map)
	 */
	
	public override sealed ExtensionResponse call(Dictionary<string, string> request) {
		ExtensionResponse res = new ExtensionResponse();
        if (!request.ContainsKey("action")) {
            res.Status = new ExtensionStatus { Code = 1, Message = "Table plugins must include a request action", Uuid = 0L};
            res.Response = Enumerable.Empty<Dictionary<string, string>>().ToList<Dictionary<string, string>>();
            return res;
		}

		if (request["action"] == "generate") {
            res.Status = new ExtensionStatus { Code = 0, Message = "OK", Uuid = 0L};
            res.Response = generate();
            return res;
		} else if (request["action"] == "columns") {
            res.Status = new ExtensionStatus { Code = 0, Message = "OK", Uuid = 0L};
            res.Response = routes();
			return res;
		}

        res.Status = new ExtensionStatus { Code = 1, Message = "Table plugin request action undefined", Uuid = 0L};
		res.Response = Enumerable.Empty<Dictionary<string, string>>().ToList<Dictionary<string, string>>();
        return res;
	}

	/**
	 * Plugin routes.
	 * 
	 * @see net.melastmohican.osquery.BasePlugin#routes()
	 */
		public override List<Dictionary<string, string>> routes() {
		var result = new List<Dictionary<string, string>>();
		foreach (var column in columns()) {
			result.Add(new Dictionary<string, string>() {
					{"id", "column"},
					{"name", (string) column.Keys.Single()}, //it has only one item
					{"type", (string) column.Values.Single()},
					{"op", "0"}
			});
		}
		return result;
	}

        /**
         * Table column definitions.
         * 
         * @return
         */
        public abstract List<TableColumn<string, string>> columns();

        /**
         * Implementation of table plugin.
         * 
         * @return Returns a list of dictionaries, such that each dictionary has a
         *         key corresponding to each of table's columns.
         */
        public abstract List<Dictionary<string, string>> generate();

}
}