using System;
using System.Linq;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Controls.ScheduleView.ICalendar;

namespace DatabaseBindingSample.Helpers
{
    public static class RecurrenceHelper
    {
        public static bool IsAnyOccurrenceInRange(string valueToParse, DateTime start, DateTime end)
        {
            RecurrencePattern pattern = new RecurrencePattern();
            if (RecurrencePatternHelper.TryParseRecurrencePattern(valueToParse, out pattern))
            {
                return IsAnyOccurrenceInRange(pattern, start, end);
            }

            return false;
        }

        public static bool IsAnyOccurrenceInRange(RecurrencePattern pattern, DateTime start, DateTime end)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException("pattern");
            }

            bool any = pattern.GetOccurrences(start, start, end).Any();
            return any;
        }
    }
}
