using GalaSoft.MvvmLight;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Observations.ViewModel
{
    public class SharedViewModelBase : ViewModelBase
    {
        public async Task<object> GetParseObject(ParseObject parseObject, string Type, string ColumnName)
        {
            if (parseObject.ContainsKey(ColumnName))
            {
                switch (Type)
                {
                    case "String":
                        return parseObject.Get<String>(ColumnName);
                    case "ParseFile":
                        return parseObject.Get<ParseFile>(ColumnName);
                    case "DateTime":
                        return parseObject.Get<DateTime>(ColumnName); 
                    default:
                        break;
                }
            }
            return null;
        }
    }
}
