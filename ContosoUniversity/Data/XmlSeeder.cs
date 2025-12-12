using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public static class XmlSeeder
    {
        public static void SeedFromXml(SchoolContext context, string xmlPath)
        {
            if (!File.Exists(xmlPath))
                return;

            var doc = XDocument.Load(xmlPath);

            var students = doc.Root.Element("Students").Elements("Student");
            foreach (var s in students)
            {
                int id = (int)s.Attribute("ID");
                if (!context.Students.Any(x => x.ID == id))
                {
                    context.Students.Add(new Student
                    {
                        ID = id,
                        FirstMidName = (string)s.Attribute("FirstMidName"),
                        LastName = (string)s.Attribute("LastName"),
                        EnrollmentDate = DateTime.Parse((string)s.Attribute("EnrollmentDate"))
                    });
                }
            }

            var courses = doc.Root.Element("Courses").Elements("Course");
            foreach (var c in courses)
            {
                int id = (int)c.Attribute("CourseID");
                if (!context.Courses.Any(x => x.CourseID == id))
                {
                    context.Courses.Add(new Course
                    {
                        CourseID = id,
                        Title = (string)c.Attribute("Title"),
                        Credits = (int)c.Attribute("Credits"),
                        DepartmentID = 1  
                    });
                }
            }

            var enrollments = doc.Root.Element("Enrollments").Elements("Enrollment");
            foreach (var e in enrollments)
            {
                int studentId = (int)e.Attribute("StudentID");
                int courseId = (int)e.Attribute("CourseID");

                if (!context.Enrollments.Any(x =>
                    x.StudentID == studentId && x.CourseID == courseId))
                {
                    context.Enrollments.Add(new Enrollment
                    {
                        StudentID = studentId,
                        CourseID = courseId,
                        Grade = Enum.TryParse<Grade>(
                            (string)e.Attribute("Grade"), out var g) ? g : null
                    });
                }
            }

            context.SaveChanges();
        }

        public static void ExportToXml(SchoolContext context, string xmlPath)
        {
            var doc = new XDocument(
                new XElement("ContosoData",
                    new XElement("Students",
                        context.Students.Select(s =>
                            new XElement("Student",
                                new XAttribute("ID", s.ID),
                                new XAttribute("FirstMidName", s.FirstMidName),
                                new XAttribute("LastName", s.LastName),
                                new XAttribute("EnrollmentDate", s.EnrollmentDate.ToString("yyyy-MM-dd"))
                            )
                        )
                    ),
                    new XElement("Courses",
                        context.Courses.Select(c =>
                            new XElement("Course",
                                new XAttribute("CourseID", c.CourseID),
                                new XAttribute("Title", c.Title),
                                new XAttribute("Credits", c.Credits)
                            )
                        )
                    ),
                    new XElement("Enrollments",
                        context.Enrollments.Select(e =>
                            new XElement("Enrollment",
                                new XAttribute("StudentID", e.StudentID),
                                new XAttribute("CourseID", e.CourseID),
                                new XAttribute("Grade", e.Grade?.ToString() ?? "")
                            )
                        )
                    )
                )
            );

            doc.Save(xmlPath);
        }
    }
}
