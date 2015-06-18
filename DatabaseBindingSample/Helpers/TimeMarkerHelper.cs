using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;

namespace DatabaseBindingSample.Helpers
{
    public static class TimeMarkerHelper
    {
        // static ctor
        static TimeMarkerHelper()
        {
            timeMarkerCollection = new ITimeMarker[] 
            {
                TimeMarker.Busy, 
                TimeMarker.Free,
                TimeMarker.OutOfOffice,
                TimeMarker.Tentative,
                new TimeMarker("Custom",System.Windows.Media.Brushes.LightGray)
            };
        }

        private static readonly ITimeMarker[] timeMarkerCollection;

        public static ITimeMarker[] TimeMarkerCollection
        {
            get { return timeMarkerCollection; }
        }

        public static ITimeMarker ResolveTimeMarkerByName(string timeMarkerName)
        {
            ITimeMarker timeMarker = timeMarkerCollection.FirstOrDefault(tm => tm.TimeMarkerName.Equals(timeMarkerName, StringComparison.CurrentCultureIgnoreCase));
            return timeMarker;
        }
    }
}
