using TodoApp.Application.Interfaces;

namespace TodoApp.Tests.Fakes
{
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        private readonly DateOnly _today;

        public FakeDateTimeProvider(DateOnly today)
        {
            _today = today;
        }

        public DateOnly Today()
        {
            return _today;
        }
    }
}
