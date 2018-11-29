using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace osquery_csharp.osquery
{
public abstract class ConfigPlugin: BasePlugin {
	
	/**
	 * Registry name.
	 * 
	 * @see net.melastmohican.osquery.BasePlugin#registryName()
	 */
	
	public override string registryName() {
		return "config";
	}
	
	/**
	 * Internal routing for this plugin type
	 * 
	 * @see net.melastmohican.osquery.BasePlugin#call(java.util.Map)
	 */
	
	public override sealed ExtensionResponse call(Dictionary<string, string> request) {
		var res = new ExtensionResponse();
        if (!request.ContainsKey("action")) {
            res.Status = new ExtensionStatus{
                Code = 1,
                Message = "Config plugins must include a request action",
                Uuid = 0L
            }; 
            res.Response = Enumerable.Empty<Dictionary<string, string>>().ToList<Dictionary<string, string>>();
			return res;
		}

		if (request["action"] == "genConfig") {
            res.Status = new ExtensionStatus{
                Code = 0,
                Message = "OK",
                Uuid = 0L
            };
            res.Response = content();
			return res;
		}
        res.Status = new ExtensionStatus{
            Code = 1,
            Message = "Config plugin request action undefined",
            Uuid = 0L
        };
        res.Response = Enumerable.Empty<Dictionary<string, string>>().ToList<Dictionary<string, string>>();
        return res;
	}
	
	/**
	 * Routes that should be broadcasted by plugin
	 * @return List<Map<string, string>>
	 */
	
	public override sealed List<Dictionary<string, string>> routes() {
        return Enumerable.Empty<Dictionary<string, string>>().ToList<Dictionary<string, string>>();
	}
	
	/**
	 * Implementation of config plugin.
	 * 
	 * @return Returns a list of dictionaries, such that each dictionary has a
	 *         key corresponding to each of table's columns.
	 */
	public abstract List<Dictionary<string, string>> content();

}
}