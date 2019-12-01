using System.Collections;

namespace XF.Material.Forms.UI
{
    public class NamedGroupList
    {
        public string GroupName { get; }
        public IList Items { get; }

        public NamedGroupList(string groupName, IList items)
        {
            this.GroupName = groupName;
            this.Items = items;
        }
    }
}