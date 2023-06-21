using System.Collections.Generic;

namespace TableStorage.View
{
    public class ColumnWithPropertiesView
    {
        public string Name { get; set; }
        public IEnumerable<ColumnPropertyView> Properties { get; set; }
    }
}
