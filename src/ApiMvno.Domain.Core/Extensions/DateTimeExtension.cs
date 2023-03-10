namespace ApiMvno.Domain.Core.Extensions;

public static class DateTimeExtension
{
    private static DateTime GetEasterDate(this DateTime easter)
    {
        int century, year, monthOfEaster, dayOfEaster, g, k, i, h, j, l;

        year = easter.Year;
        century = year / 100;
        g = (year % 19);
        k = (century - 17) / 25;
        i = (int)(century - Math.Truncate((decimal)century / 4) - Math.Truncate((((decimal)century - k) / 3)) + 19 * g + 15) % 30;
        h = (int)(i - Math.Truncate((decimal)i / 28) * (1 * -Math.Truncate((decimal)i / 28) * Math.Truncate(29 / ((decimal)i + 1))) * Math.Truncate(((21 - (decimal)g) / 11)));
        j = (int)(year + Math.Truncate((decimal)year / 4) + h + 2 - century + Math.Truncate((decimal)century / 4)) % 7;
        l = h - j;

        monthOfEaster = (int)(3 + Math.Truncate(((decimal)l + 40) / 44));
        dayOfEaster = (int)(l + 28 - 31 * Math.Truncate(((decimal)monthOfEaster / 4)));

        return new DateTime(year, monthOfEaster, dayOfEaster);
    }

    public static List<DateTime> GetAllHolidays(this DateTime date)
    {
        List<DateTime> holidays = new List<DateTime>();

        holidays.Add(new DateTime(date.Year, 01, 01)); //Confraternização Universal
        holidays.Add(new DateTime(date.Year, 04, 21)); //Tiradentes
        holidays.Add(new DateTime(date.Year, 05, 01)); //Dia do Trabalhador
        holidays.Add(new DateTime(date.Year, 09, 07)); //Dia da Independência
        holidays.Add(new DateTime(date.Year, 10, 12)); //N. S. Aparecida
        holidays.Add(new DateTime(date.Year, 11, 02)); //Todos os Santos
        holidays.Add(new DateTime(date.Year, 11, 15)); //Proclamação da Republica
        holidays.Add(new DateTime(date.Year, 12, 25)); //Natal

        var easter = date.GetEasterDate();

        holidays.Add(easter); //Pascoa
        holidays.Add(easter.AddDays(60)); //Corpus Christi
        holidays.Add(easter.AddDays(-2)); //6º feira Santa
        holidays.Add(easter.AddDays(-47)); //3º feria Carnaval
        holidays.Add(easter.AddDays(-48)); //2º feria Carnaval

        return holidays.OrderBy(f => f.Date).ToList();
    }

    public static List<DateTime> GetYearWeekendDates(this DateTime date)
    {
        var dayList = new List<DateTime>();

        for (DateTime currentDate = new DateTime(date.Year, 1, 1); currentDate <= new DateTime(date.Year, 12,31); currentDate = currentDate.AddDays(1))
        {
            if (currentDate.DayOfWeek == DayOfWeek.Sunday || currentDate.DayOfWeek == DayOfWeek.Saturday)
                dayList.Add(currentDate);
        }

        return dayList;
    }
    
    public static List<DateTime> GetYearWeekDates(this DateTime date)
    {
        var dayList = new List<DateTime>();

        for (DateTime currentDate = new DateTime(date.Year, 1, 1); currentDate <= new DateTime(date.Year, 12,31); currentDate = currentDate.AddDays(1))
        {
            if (!(currentDate.DayOfWeek == DayOfWeek.Sunday || currentDate.DayOfWeek == DayOfWeek.Saturday))
                dayList.Add(currentDate);
        }

        return dayList;
    }
    
    public static IEnumerable<KeyValuePair<DateTime, DateTime>> GetAllPortabilityWindows(this DateTime date)
    {

        if (date < DateTime.UtcNow.FromUtcToBrTimeZone().AddHours(72))
        {
            date = date.Date.Add(DateTime.UtcNow.FromUtcToBrTimeZone().TimeOfDay).AddHours(72);
        }

        var portabilityWindows = new List<KeyValuePair<DateTime, DateTime>>();

        #region Holiday time portability window

        var holidays = date.GetAllHolidays().Where(holidayDate => holidayDate.Date >= date.Date);

        var holidayPortabilityTimeWindows = new List<KeyValuePair<TimeSpan, TimeSpan>>()
        {
            new (TimeSpan.FromHours(10), TimeSpan.FromHours(12)),
            new (TimeSpan.FromHours(14), TimeSpan.FromHours(16)),
            new (TimeSpan.FromHours(18), TimeSpan.FromHours(20))
        };

        foreach (var holiday in holidays)
        {
            foreach (var windows in holidayPortabilityTimeWindows)
            {
                portabilityWindows.Add(new(holiday.Add(windows.Key), holiday.Add(windows.Value)));
            }
        }

        #endregion

        #region Weekend time portability window

        var weekendDates = date.GetYearWeekendDates().Where(weekendDate => weekendDate.Date >= date.Date);

        var weekendPortabilityTimeWindows = new List<KeyValuePair<TimeSpan, TimeSpan>>()
        {
            new (TimeSpan.FromHours(10), TimeSpan.FromHours(12)),
            new (TimeSpan.FromHours(14), TimeSpan.FromHours(16)),
            new (TimeSpan.FromHours(18), TimeSpan.FromHours(20))
        };

        foreach (var weekend in weekendDates)
        {
            if (portabilityWindows.All(portabilityWindow => portabilityWindow.Key.Date != weekend.Date))
            {
                foreach (var windows in weekendPortabilityTimeWindows)
                {
                    portabilityWindows.Add(new(weekend.Add(windows.Key), weekend.Add(windows.Value)));
                }
            }
        }

        #endregion


        #region Week time portability window

        var weekDates = date.GetYearWeekDates().Where(weekDate => weekDate.Date >= date.Date);

        var weekPortabilityTimeWindows = new List<KeyValuePair<TimeSpan, TimeSpan>>()
        {
            new (TimeSpan.FromHours(8), TimeSpan.FromHours(10)),
            new (TimeSpan.FromHours(12), TimeSpan.FromHours(14)),
            new (TimeSpan.FromHours(16), TimeSpan.FromHours(18)),
            new (TimeSpan.FromHours(20), TimeSpan.FromHours(22)),
            new (TimeSpan.FromHours(22), TimeSpan.FromHours(23.9999))
        };

        foreach (var week in weekDates)
        {
            if (portabilityWindows.All(portabilityWindow => portabilityWindow.Key.Date != week.Date))
            {
                foreach (var windows in weekPortabilityTimeWindows)
                {
                    portabilityWindows.Add(new(week.Add(windows.Key), week.Add(windows.Value)));
                }
            }
        }

        #endregion
        
        return portabilityWindows
            .Where(pw => pw.Key >= date)
            .OrderBy(pw => pw.Key);
    }
    
    public static bool IsValidPortabilityScheduledDate(this DateTime date)
    {
        return date.GetAllPortabilityWindows().Any(portabilityWindow =>
            date.Date == portabilityWindow.Key.Date 
            && date.TimeOfDay >= portabilityWindow.Key.TimeOfDay
            && date.TimeOfDay <= portabilityWindow.Value.TimeOfDay);
    }

    public static DateTime FromUtcToBrTimeZone(this DateTime utcDate)
    {
        var timeLista = TimeZoneInfo.GetSystemTimeZones();

        var brTimeZone = timeLista.FirstOrDefault(tz => tz.BaseUtcOffset == new TimeSpan(-3, 0, 0));
        
        return TimeZoneInfo.ConvertTimeFromUtc(utcDate, brTimeZone);
    }

    public static TimeSpan Diff(this DateTime date1, DateTime date2)
    {
        var startDate = date1 < date2 ? date1 : date2;
        var endDate = date1 < date2 ? date2 : date1;
        return endDate - startDate;
    }
}