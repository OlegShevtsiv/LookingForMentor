using LFM.DataAccess.DB.Core.Types;

namespace Lfm.Web.Mvc.App.UIRenderers.Models
{
    public class CheckBox<T>
    {
        public T Value { get; set; }
        
        public string Name { get; set; }

        public string Checked { get; set; }
    }
}