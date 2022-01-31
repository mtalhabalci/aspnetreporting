using SDIKit.Common.Attributes;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SDIKit.Common.Types
{
    public partial class ApplicationTypeDefinition
    {
        public class PropertyInfo
        {
            public bool IsNullable { get; set; }
            public string Label { get; set; }
            public string Name { get; set; }
            public bool Sortable { get; set; }
            public string SortableAlias { get; set; }
            public string Type { get; set; }
            public int Order { get; set; }
            public bool IsHidden { get; set; }
            public int StepNumber { get; set; }
            public bool IsEmail { get; set; }
            public int MaxLength { get; set; }
            public bool IsRequired { get; set; }
            public bool IsUrl { get; set; }
            public int MinLength { get; set; }
            public object Value { get; set; }

            public PropertyInfo AddValidations(IEnumerable<Attribute> attributes)
            {
                foreach (var attr in attributes)
                {
                    if (attr is DisplayAttribute)
                    {
                        //throw new Exception("DisplayName attrbute'u kullanılmalıdır");
                        Label = (attr as DisplayAttribute).Name;
                    }
                    if (attr is DisplayNameAttribute)
                    {
                        Label = (attr as DisplayNameAttribute).DisplayName;
                    }
                    else if (attr is SortableAttribute)
                    {
                        Sortable = true;
                        SortableAlias = (attr as SortableAttribute).Alias;
                    }
                    else if (attr is OrderAttribute)
                    {
                        Order = (attr as OrderAttribute).ColumnIndex;
                    }
                    else if (attr is HiddenAttribute)
                    {
                        IsHidden = true;
                    }
                    else if (attr is StepAttribute)
                    {
                        StepNumber = (attr as StepAttribute).Step;
                    }
                    else if (attr is EmailAddressAttribute)
                    {
                        IsEmail = true;
                    }
                    else if (attr is MaxLengthAttribute)
                    {
                        MaxLength = (attr as MaxLengthAttribute).Length;
                    }
                    else if (attr is MinLengthAttribute)
                    {
                        MinLength = (attr as MinLengthAttribute).Length;
                    }
                    else if (attr is RequiredAttribute)
                    {
                        IsRequired = true;
                    }
                    else if (attr is UrlAttribute)
                    {
                        IsUrl = true;
                    }
                }
                return this;
            }

            public PropertyInfo MarkAsNullable()
            {
                IsNullable = true;
                return this;
            }

            public PropertyInfo SetType(string type)
            {
                Type = type;
                return this;
            }
        }
    }
}