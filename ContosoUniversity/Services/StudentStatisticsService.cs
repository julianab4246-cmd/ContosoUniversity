using ContosoUniversity.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Services
{
    public class StudentStatisticsService
    {
        public int CountByLastName(IEnumerable<Student> students, string lastName)
        {
            if (students == null || lastName == null)
                return 0;

            return students.Count(s => s.LastName == lastName);
        }

        public double AverageCredits(IEnumerable<Enrollment> enrollments)
        {
            if (enrollments == null || !enrollments.Any())
                return 0;

            return enrollments.Average(e => e.Course.Credits);
        }
    }
}
