using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using BL;

namespace PL
{
    public class Utils
    {
        public static IEnumerable<ComboBoxItem> NullableComboBoxItems(Type type, string nullTitle)
        {
            return Enum.GetValues(type).Cast<object>()
                .Select(s => new ComboBoxItem {Content = s.ToString(), Tag = s})
                .Prepend(new ComboBoxItem {Content = nullTitle, Tag = null});
        }
    }
}