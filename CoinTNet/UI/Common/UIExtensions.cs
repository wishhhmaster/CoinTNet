using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Linq;
using CoinTNet.Common;

namespace CoinTNet.UI.Common
{
    /// <summary>
    /// Contains UI extensions
    /// </summary>
    static class UIExtensions
    {
        public static bool IsValueEmpty(this TextBox textbox)
        {
            return string.IsNullOrEmpty(textbox.Text.Trim());
        }

        public static string TrimmedText(this TextBox textbox)
        {
            return textbox.Text.Trim();
        }


        public static void SelectValue<T>(this ComboBox cbb, T value)
        {
            foreach (var item in cbb.Items)
            {
                var cbbItem = item as ListControlItem<T>;
                if ((cbbItem == null && value == null) || (cbbItem != null && cbbItem.Item.Equals(value)))
                {
                    cbb.SelectedItem = item;
                    break;
                }
            }
        }

        public static void SelectValue<T>(this ListBox cbb, T value) //where T : class
        {
            foreach (var item in cbb.Items)
            {
                var cbbItem = item as ListControlItem<T>;
                if (cbbItem != null && cbbItem.Item.Equals(value))
                {
                    cbb.SelectedItem = item;
                    break;
                }
            }
        }

        public static T GetSelectedValue<T>(this ComboBox cbb)
        {
            var item = cbb.SelectedItem as ListControlItem<T>;
            return item != null ? item.Item : default(T);
        }

        public static T GetSelectedValue<T>(this ListBox cbb) where T : class
        {
            var item = cbb.SelectedItem as ListControlItem<T>;
            return item != null ? item.Item : null;
        }

        public static List<T> GetSelectedValues<T>(this ListBox cbb) where T : class
        {
            List<T> list = new List<T>();
            foreach (var o in cbb.SelectedItems)
            {
                var item = o as ListControlItem<T>;
                if (item != null)
                    list.Add(item.Item);
            }
            return list;
        }

        public static T FindParentControl<T>(this Control c) where T : Control
        {
            while (c.Parent != null)
            {
                var p = c.Parent as T;
                if (p != null)
                    return p;
                c = c.Parent;
            }
            return null;
        }

        public static void PopulateListboxFromList<T>(this ListBox listbox, IEnumerable<T> list, Func<T, string> func, T valueToSelect, bool addBlank)
        {
            listbox.BeginUpdate();
            listbox.Items.Clear();
            if (addBlank)
                listbox.Items.Add(string.Empty);
            
            list.ForEachExt(item => listbox.Items.Add(new ListControlItem<T>(item, func(item))));

            if (valueToSelect != null)
                listbox.SelectValue(valueToSelect);
            /*else if (listbox.Items.Count > 0)
                listbox.SelectedIndex = 0;*/

            listbox.EndUpdate();
        }

        /// <summary>
        /// Populates a combobox with a list of objets
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="comboBox">The combobox to populate</param>
        /// <param name="list">The list of objects used to fill the cbb</param>
        /// <param name="funcDescription">A function used to get a textual representation of each item</param>
        /// <param name="valueToSelect">The value to select</param>
        /// <param name="addBlank">Whether to add a blank value to the list</param>
        public static void PopulateCbbFromList<T>(this ComboBox comboBox, IEnumerable<T> list, Func<T, string> funcDescription, T valueToSelect, bool addBlank = false)
        {
            comboBox.BeginUpdate();
            comboBox.Items.Clear();
            if (addBlank)
                comboBox.Items.Add(string.Empty);

            list.ForEachExt(item => comboBox.Items.Add(new ListControlItem<T>(item, funcDescription(item))));

            if (valueToSelect != null)
                comboBox.SelectValue(valueToSelect);
            else if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;

            comboBox.EndUpdate();
        }

        public static void PopulateCbbFromSimpleList<T>(this ComboBox comboBox, IEnumerable<Tuple<string, T>> list, Tuple<string,T> valueToSelect = null, bool addBlank = false)
        {
            comboBox.BeginUpdate();
            comboBox.Items.Clear();
            
            comboBox.DataSource = list;
            comboBox.DisplayMember = "Item1";
            comboBox.ValueMember = "Item2";
            
            if (valueToSelect != null)
                comboBox.SelectValue(valueToSelect);
            else if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;

            if (addBlank)
                comboBox.Items.Add(string.Empty);

            comboBox.EndUpdate();
        }


        public static List<T> ToListExt<T>(this ICollection col)
        {
            List<T> list = new List<T>();
            foreach (var elt in col)
            {
                list.Add((T)elt);
            }
            return list;
        }
    }


}
