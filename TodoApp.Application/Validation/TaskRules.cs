using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Validation
{
    public class TaskRules
    {
        private readonly IHolidayService _holidayService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public TaskRules(
            IHolidayService holidayService,
            IDateTimeProvider dateTimeProvider)
        {
            _holidayService = holidayService;
            _dateTimeProvider = dateTimeProvider;
        }

        public void ValidateDueDate(DateOnly dueDate)
        {
            if (dueDate < _dateTimeProvider.Today())
            {
                throw new DomainRuleException(
                    "Due date cannot be in the past.");
            }

            if (dueDate.DayOfWeek == DayOfWeek.Saturday  ||
                dueDate.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new DomainRuleException(
                    "Due date cannot be on a weekend.");
            }
            if (_holidayService.IsHoliday(dueDate))
            {
                throw new DomainRuleException(
                    "Due date cannot be on a holiday.");
            }

        }

    }
}
