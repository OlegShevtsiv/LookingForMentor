using Lfm.Core.Common.Web.Types;

namespace Lfm.Core.Common.Web.Models.DataModels
{
    public sealed class AlertDataModel
    {
        public readonly string Message;

        public readonly AlertTypes Type;
        
        public AlertDataModel(string message, AlertTypes type)
        {
            Message = message;
            Type = type;
        }
    }
}