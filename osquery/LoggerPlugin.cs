using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace osquery_csharp.osquery
{
public abstract class LoggerPlugin: BasePlugin {
	
	
	public override string registryName() {
		return "logger";
	}
	
	/**
	 * Internal routing for this plugin type
	 * 
	 * @see net.melastmohican.osquery.BasePlugin#call(java.util.Map)
	 */
	public override sealed ExtensionResponse call(Dictionary<string, string> request) {
        var res = new ExtensionResponse();
        res.Response = Enumerable.Empty<Dictionary<string, string>>().ToList<Dictionary<string, string>>();
		if (request.ContainsKey("string")) {
            res.Status = logstring(request["string"]); 
            return res;
		} else if (request.ContainsKey("snapshot")) {
            res.Status = logstring(request["snapshot"]); 
			return res;
		} else if (request.ContainsKey("health")) {
            res.Status = logstring(request["health"]); 
			return res;
		} else if (request.ContainsKey("init")) {
            res.Status = new ExtensionStatus {
                Code = 1,
                Message ="Use Glog for status logging",
                Uuid = 0L
            };
            return res;
		} else if (request.ContainsKey("status")) {
            res.Status = new ExtensionStatus {
                Code = 1,
                Message ="Use Glog for status logging",
                Uuid = 0L
            };
            return res;
		}
        res.Status = new ExtensionStatus {
                Code = 1,
                Message ="Logger plugin request action undefined",
                Uuid = 0L
            };
		
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
	 * The implementation of your logger plugin
	 * 
	 * @param value the string to log
	 * @return ExtensionStatus
	 */
	public abstract ExtensionStatus logstring(string value);

	/**
	 * If you'd like the log health statistics about osquery's performance, override this method in your logger plugin.
	 * By default, this action is just hands off the string to logstring.
	 * @param value
	 * @return ExtensionStatus
	 */
	public ExtensionStatus logHealth(string value) {
		return new ExtensionStatus{Code=0,Message= "OK",Uuid= 0L};
	}
 
	/**
	 * If you'd like to log snapshot queries in a special way, override this method.
	 * @param value
	 * @return ExtensionStatus
	 */
	public ExtensionStatus logSnapshot(string value) {
		return this.logstring(value);
	}
}
}