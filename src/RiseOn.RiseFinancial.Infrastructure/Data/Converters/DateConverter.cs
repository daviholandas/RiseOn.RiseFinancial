using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace RiseOn.RiseFinancial.Infrastructure.Data.Converters;

public class DateConverter : ValueConverter<DateOnly, DateTime>
{

    public DateConverter()
        : base(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime)){}
}

public class DateComparer : ValueComparer<DateOnly>
{
    public DateComparer() : base(
        (d1, d2) => d1.DayNumber == d2.DayNumber,
        d => d.GetHashCode()){}
}