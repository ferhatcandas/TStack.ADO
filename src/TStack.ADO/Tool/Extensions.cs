using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace TStack.ADO.Tool
{
    internal static class Extensions
    {
        internal static void Add(this List<Parameter> parameters,string key, object value)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            parameters.Add(new Parameter(key, value));
        }
        internal static void AddParameters(this SqlCommand command, List<Parameter> parameters)
        {
            if (parameters != null)
                foreach (Parameter param in parameters)
                    command.Parameters.AddWithValue(param.Key, param.Value);
        }
        internal static void ParseCommandType(this SqlCommand command, Tool.CommandType commandType)
        {
            command.CommandType = (System.Data.CommandType)Enum.Parse(typeof(System.Data.CommandType), commandType.ToString());
        }
    }
}
