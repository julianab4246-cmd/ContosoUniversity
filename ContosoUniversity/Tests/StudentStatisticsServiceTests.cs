using Xunit;
using ContosoUniversity.Services;
using ContosoUniversity.Models;
using System.Collections.Generic;

public class StudentStatisticsServiceTests
{
    [Fact]
    public void CountByLastName_ReturnsCorrectCount()
    {
        var service = new StudentStatisticsService();
        var students = new List<Student>
        {
            new Student { LastName = "Smith" },
            new Student { LastName = "Johnson" },
            new Student { LastName = "Smith" }
        };

        var result = service.CountByLastName(students, "Smith");

        Assert.Equal(2, result);
    }

    [Fact]
    public void AverageCredits_ReturnsExpectedAverage()
    {
        var service = new StudentStatisticsService();
        var enrollments = new List<Enrollment>
        {
            new Enrollment { Course = new Course { Credits = 3 }},
            new Enrollment { Course = new Course { Credits = 4 }},
        };

        var result = service.AverageCredits(enrollments);

        Assert.Equal(3.5, result, precision: 1);
    }
}
