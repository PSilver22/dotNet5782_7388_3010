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
        /// <summary>
        /// Creates ComboBoxItem's for a nullable enum field.
        /// </summary>
        /// <param name="type">the enum type</param>
        /// <param name="nullTitle">the title for the num option</param>
        /// <returns>An IEnumerable of ComboBoxItem's for each enum variant as well as for null</returns>
        public static IEnumerable<ComboBoxItem> NullableComboBoxItems(Type type, string nullTitle)
        {
            return Enum.GetValues(type).Cast<object>()
                .Select(s => new ComboBoxItem {Content = s.ToString(), Tag = s})
                .Prepend(new ComboBoxItem {Content = nullTitle, Tag = null});
        }
    }
}