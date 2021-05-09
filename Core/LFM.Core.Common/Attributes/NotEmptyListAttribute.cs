using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace LFM.Core.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmptyListAttribute : ValidationAttribute
    {
        private const string defaultError = "'{0}' must have at least one element.";
        public NotEmptyListAttribute ( ) 
            : base(defaultError) 
        {
        }

        public override bool IsValid (object value)
        {
            IList list = value as IList;
            
            return (list != null && list.Count > 0);
        }

        public override string FormatErrorMessage ( string name )
        {
            return String.Format(this.ErrorMessageString, name);
        }
    }
}