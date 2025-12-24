using TodoApp.Application.Interfaces;

namespace TodoApp.Tests.Fakes
{
    public class FakeHolidayService : IHolidayService
    {
        private readonly HashSet<DateOnly> _holidays;

        public FakeHolidayService(params DateOnly[] holidays)
        {
            _holidays = new HashSet<DateOnly>(holidays);
        }

        public bool IsHoliday(DateOnly date)
        {
            return _holidays.Contains(date);
        }
    }
}
