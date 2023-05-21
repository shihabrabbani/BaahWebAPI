using System;

namespace BaahWebAPI
{
    public class clsUtility
    {

        public string GetDateString(int type, string format)
        {
            DateTime givenDate = DateTime.Now;
            DateTime result = DateTime.Now;
            if (type == 1)
            {
                result = new DateTime(givenDate.Year, givenDate.Month, 1);
            }
            else if (type == 2)
            {
                // Get the first day of the next month
                DateTime nextMonth = givenDate.AddMonths(1);
                DateTime firstDayOfNextMonth = new DateTime(nextMonth.Year, nextMonth.Month, 1);

                // Subtract one day to get the last day of the current month
                result = firstDayOfNextMonth.AddDays(-1);
            }

            return result.ToString(format);
        }
    }
}