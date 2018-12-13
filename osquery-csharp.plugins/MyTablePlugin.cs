
using System;
using System.Collections.Generic;
using osquery_csharp.osquery;

namespace osquery_csharp.plugins
{
    public class MyTablePlugin : TablePlugin
    {

        public override List<TableColumn<string, string>> columns()
        {
            var colList = new List<TableColumn<string, string>>();
            var col1 = new TableColumn<string, string>()
        {
            {"foo", "TEXT"}
        };
            colList.Add(col1);

            var col2 = new TableColumn<string, string>()
        {
            {"baz", "TEXT"}
        };
            colList.Add(col2);
            return colList;
        }

        public override string name()
        {
            return "mytable";
        }

        public override List<Dictionary<string, string>> generate()
        {
            var result = new List<Dictionary<string, string>>();

            result.Add(new Dictionary<string, string> {
            {"foo", "bar"},
            {"baz", "baz"}
        });

            result.Add(new Dictionary<string, string> {
            {"foo", "bar"},
            {"baz", "baz"}
        });

            return result;
        }
    }
}