using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;

namespace TodoApp.Infrastructure.Services
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateOnly Today()
        {
            return DateOnly.FromDateTime(DateTime.Today);
        }
    }
}
