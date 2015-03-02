using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

using System.Windows.Forms;

namespace Windows.Forms.Controls.Forms.MyListBox
{

    internal class MyListBoxItemConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
            //MessageBox.Show("CanConvertTo:" + (destinationType == typeof(InstanceDescriptor)));
            return destinationType == typeof(InstanceDescriptor) ||
                base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
            object value, Type destinationType) {
            if (destinationType == null)
                throw new ArgumentNullException("DestinationType cannot be null");
            //MessageBox.Show("Convertto OK");
            if (destinationType == typeof(InstanceDescriptor) && (value is MyListBoxItem)) {
                ConstructorInfo constructor = null;
                MyListBoxItem item = (MyListBoxItem)value;
                MyListBoxSubItem[] subItems = null;
                //MessageBox.Show("Convertto Start Item:" + item.Text);
                //MessageBox.Show("Item.SubItems.Count:" + item.SubItems.Count);
                if (item.SubItems.Count > 0) {
                    subItems = new MyListBoxSubItem[item.SubItems.Count];
                    item.SubItems.CopyTo(subItems, 0);
                }
                //MessageBox.Show("Item.SubItems.Count:" + item.SubItems.Count);
                if (item.Text != null && subItems != null)
                    constructor = typeof(MyListBoxItem).GetConstructor(new Type[] { typeof(string), typeof(MyListBoxSubItem[]) });
                //MessageBox.Show("Constructor(Text,item[]):" + (constructor != null));
                if (constructor != null)
                    return new InstanceDescriptor(constructor, new object[] { item.Text, subItems }, false);
                
                if (subItems != null)
                    constructor = typeof(MyListBoxItem).GetConstructor(new Type[] { typeof(MyListBoxSubItem[]) });
                if (constructor != null)
                    return new InstanceDescriptor(constructor, new object[] { subItems }, false);
                if (item.Text != null) {
                    //MessageBox.Show("StartGetConstructor(text)");
                    constructor = typeof(MyListBoxItem).GetConstructor(new Type[] { typeof(string), typeof(bool) });
                }
                //MessageBox.Show("Constructor(Text):" + (constructor != null));
                if (constructor != null) {
                    //System.Windows.Forms.MessageBox.Show("text OK");
                    return new InstanceDescriptor(constructor, new object[] { item.Text, item.IsOpen });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
