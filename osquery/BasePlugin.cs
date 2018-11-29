using System;
using System.Collections;
using System.Collections.Generic;

namespace osquery_csharp.osquery
{
public abstract class BasePlugin {	
	public abstract string name();
	
    public abstract string registryName();
	public abstract ExtensionResponse call(Dictionary<string, string> request);
	/**
	 * Routes that should be broadcasted by plugin
	 * @return List<Map<String, String>>
	 */
	public abstract List<Dictionary<string, string>> routes();

}
}