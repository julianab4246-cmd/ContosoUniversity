This is my first part of the Contoso University app 
Student — basic student info; one-to-many with Enrollment

Course — identified by non–identity CourseID; belongs to one Department

Enrollment — join table for the many-to-many relationship between Student and Course, 

Department — academic units; each may have an Instructor as Administrator

Instructor — faculty members; can teach many courses

CourseAssignment — explicit many-to-many join table linking Instructor and Course

OfficeAssignment — one-to-one record giving an Instructor a physical office
