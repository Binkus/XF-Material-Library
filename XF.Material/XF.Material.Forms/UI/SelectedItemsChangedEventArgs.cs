using System;
using System.Collections;

namespace XF.Material.Forms.UI
{
    public class SelectedItemsChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Returns the indices of each selected choices.
        /// </summary>
        public IList Items { get; }

        public SelectedItemsChangedEventArgs(IList items)
        {
            this.Items = items;
        }
    }

    public class NamedGroupSelectedItemsChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Returns the indices of each selected choices.
        /// </summary>
        public IList Items { get; }

        public string GroupName { get; }

        public NamedGroupSelectedItemsChangedEventArgs(string groupName, IList items)
        {
            this.Items = items;
            this.GroupName = groupName;
        }
    }
}
