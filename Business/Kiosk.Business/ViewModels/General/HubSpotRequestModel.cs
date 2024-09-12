using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.ViewModels.General
{
    public class HubSpotRequestModel
    {
        public int limit { get; set; }
        public List<FilterGroup> filterGroups { get; set; }
        public List<Sort> sorts { get; set; }
        public List<string> properties { get; set; }
    }

    public class FilterGroup
    {
        public List<Filter> filters { get; set; }
    }
    public class SearchContactRequestModel
    {
        public List<FilterGroup> filterGroups { get; set; }
    }

    public class Filter
    {
        public string propertyName { get; set; }
        public string @operator { get; set; }
        public string value { get; set; }
    }
    public class Sort
    {
        public string propertyName { get; set; }
        public string direction { get; set; }
    }
}
