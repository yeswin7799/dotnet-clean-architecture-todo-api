using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;

namespace TodoApp.Infrastructure.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly HashSet<DateOnly> _holidays;
        public HolidayService()
        {
            _holidays = new HashSet<DateOnly>
            {
                new DateOnly(2026, 1, 1),
                new DateOnly(2026, 7, 4),
                new DateOnly(2026, 12, 25)
            };
        }

        public bool IsHoliday(DateOnly date)
        {
            return _holidays.Contains(date);
        }
    }
}
