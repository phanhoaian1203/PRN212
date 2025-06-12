using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DelegatesLinQ.Homework
{
    // Data models for the homework
    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; } // Added required modifier
        public int Age { get; set; }
        public required string Major { get; set; } // Added required modifier
        public double GPA { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public DateTime EnrollmentDate { get; set; }
        public required string Email { get; set; } // Added required modifier
        public required Address Address { get; set; } // Added required modifier
    }

    public class Course
    {
        public required string Code { get; set; } // Added required modifier
        public required string Name { get; set; } // Added required modifier
        public int Credits { get; set; }
        public double Grade { get; set; } // 0-4.0 scale
        public required string Semester { get; set; } // Added required modifier
        public required string Instructor { get; set; } // Added required modifier
    }

    public class Address
    {
        public required string Street { get; set; } // Added required modifier
        public required string City { get; set; } // Added required modifier
        public required string State { get; set; } // Added required modifier
        public required string ZipCode { get; set; } // Added required modifier
    }

    /// <summary>
    /// Homework 3: LINQ Data Processor
    /// 
    /// This is the most challenging homework requiring students to:
    /// 1. Use complex LINQ operations with multiple data sources
    /// 2. Implement custom extension methods
    /// 3. Create generic delegates and expressions
    /// 4. Handle advanced scenarios like pivot operations, statistical analysis
    /// 5. Implement caching and performance optimization
    /// 
    /// Advanced Requirements:
    /// - Custom LINQ extension methods
    /// - Expression trees and dynamic queries
    /// - Performance comparison between different approaches
    /// - Statistical calculations and reporting
    /// - Data transformation and pivot operations
    /// </summary>
    public class LinqDataProcessor
    {
        private List<Student> _students;

        public LinqDataProcessor()
        {
            _students = GenerateSampleData();
        }

        // BASIC REQUIREMENTS (using techniques from sample codes)
        
        public void BasicQueries()
        {
            // TODO: Implement basic LINQ queries similar to 6_8_LinQObject
            
            // 1. Find all students with GPA > 3.5
            // 2. Group students by major
            // 3. Calculate average GPA per major
            // 4. Find students enrolled in specific courses
            // 5. Sort students by enrollment date
            
            Console.WriteLine("=== BASIC LINQ QUERIES ===");
            // Implementation needed by students

            // 1. Students with GPA > 3.5
            var highGPAStudents = _students.Where(s => s.GPA > 3.5).Select(s => s.Name);
            Console.WriteLine("\nStudents with GPA > 3.5:");
            Console.WriteLine(string.Join(", ", highGPAStudents));

            // 2. Group students by major
            var studentsByMajor = _students.GroupBy(s => s.Major)
                .Select(g => new { Major = g.Key, Count = g.Count() });
            Console.WriteLine("\nStudents grouped by major:");
            foreach (var group in studentsByMajor)
            {
                Console.WriteLine($"{group.Major}: {group.Count} students");
            }

            // 3. Average GPA per major
            var avgGPAByMajor = _students.GroupBy(s => s.Major)
                .Select(g => new { Major = g.Key, AverageGPA = g.Average(s => s.GPA) });
            Console.WriteLine("\nAverage GPA by major:");
            foreach (var group in avgGPAByMajor)
            {
                Console.WriteLine($"{group.Major}: {group.AverageGPA:F2}");
            }

            // 4. Students enrolled in specific courses (e.g., CS101 or CS102)
            var courseStudents = _students
                .Where(s => s.Courses.Any(c => c.Code == "CS101" || c.Code == "CS102"))
                .Select(s => s.Name);
            Console.WriteLine("\nStudents enrolled in CS101 or CS102:");
            Console.WriteLine(string.Join(", ", courseStudents));

            // 5. Sort students by enrollment date
            var sortedByEnrollment = _students.OrderBy(s => s.EnrollmentDate)
                .Select(s => $"{s.Name} ({s.EnrollmentDate:yyyy-MM-dd})");
            Console.WriteLine("\nStudents sorted by enrollment date:");
            Console.WriteLine(string.Join(", ", sortedByEnrollment));
        }

        // Challenge 1: Create custom extension methods
        public void CustomExtensionMethods()
        {
            Console.WriteLine("=== CUSTOM EXTENSION METHODS ===");
            
            // TODO: Implement extension methods
            // 1. CreateAverageGPAByMajor() extension method
            // 2. FilterByAgeRange(int min, int max) extension method  
            // 3. ToGradeReport() extension method that creates formatted output
            // 4. CalculateStatistics() extension method
            
            // Example usage (students need to implement the extensions):
            // var highPerformers = _students.FilterByAgeRange(20, 25).Where(s => s.GPA > 3.5);
            // var gradeReport = _students.ToGradeReport();
            // var stats = _students.CalculateStatistics();

            var highPerformers = _students.FilterByAgeRange(20, 25).Where(s => s.GPA > 3.5);
            Console.WriteLine("\nHigh performers (Age 20-25, GPA > 3.5):");
            Console.WriteLine(string.Join(", ", highPerformers.Select(s => s.Name)));

            var avgGPAByMajor = _students.AverageGPAByMajor();
            Console.WriteLine("\nAverage GPA by Major (Extension Method):");
            foreach (var kvp in avgGPAByMajor)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value:F2}");
            }

            Console.WriteLine("\nGrade Report for First Student:");
            Console.WriteLine(_students.First().ToGradeReport());

            var stats = _students.CalculateStatistics();
            Console.WriteLine("\nStatistics:");
            Console.WriteLine($"Mean GPA: {stats.MeanGPA:F2}");
            Console.WriteLine($"Median GPA: {stats.MedianGPA:F2}");
            Console.WriteLine($"Standard Deviation: {stats.StandardDeviation:F2}");
        }

        // Challenge 2: Dynamic queries using Expression Trees
        public void DynamicQueries()
        {
            Console.WriteLine("=== DYNAMIC QUERIES ===");
            
            // TODO: Research Expression Trees
            // Implement a method that builds LINQ queries dynamically based on user input
            // Example: BuildDynamicFilter("GPA", ">", "3.5")
            // This requires understanding of Expression<Func<T, bool>>
            
            // Students should implement:
            // 1. Dynamic filtering based on property name and value
            // 2. Dynamic sorting by any property
            // 3. Dynamic grouping operations

            // Dynamic filter: GPA > 3.5
            var gpaFilter = BuildDynamicFilter<Student>("GPA", ">", 3.5);
            var filteredStudents = _students.AsQueryable().Where(gpaFilter).Select(s => s.Name);
            Console.WriteLine("\nDynamic Filter (GPA > 3.5):");
            Console.WriteLine(string.Join(", ", filteredStudents));

            // Dynamic sort: by Name
            var sortedStudents = _students.AsQueryable().OrderByProperty("Name").Select(s => s.Name);
            Console.WriteLine("\nDynamic Sort by Name:");
            Console.WriteLine(string.Join(", ", sortedStudents));

            // Dynamic grouping: by Major
            var groupedStudents = _students.AsQueryable().GroupByProperty("Major")
                .Select(g => new { Major = g.Key, Count = g.Count() });
            Console.WriteLine("\nDynamic Grouping by Major:");
            foreach (var group in groupedStudents)
            {
                Console.WriteLine($"{group.Major}: {group.Count} students");
            }
        }

        // Helper method for dynamic filtering
        private Expression<Func<T, bool>> BuildDynamicFilter<T>(string propertyName, string op, double value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(value);
            Expression comparison = op switch
            {
                ">" => Expression.GreaterThan(property, constant),
                "<" => Expression.LessThan(property, constant),
                "=" => Expression.Equal(property, constant),
                _ => throw new ArgumentException("Invalid operator")
            };
            return Expression.Lambda<Func<T, bool>>(comparison, parameter);
        }

        // Challenge 3: Statistical Analysis with Complex Aggregations
        public void StatisticalAnalysis()
        {
            Console.WriteLine("=== STATISTICAL ANALYSIS ===");
            
            // TODO: Implement complex statistical calculations
            // 1. Calculate median GPA (requires custom logic)
            // 2. Calculate standard deviation of GPAs
            // 3. Find correlation between age and GPA
            // 4. Identify outliers using statistical methods
            // 5. Create percentile rankings

            var gpas = _students.Select(s => s.GPA).OrderBy(g => g).ToList();
            double medianGPA = gpas.Count % 2 == 0
                ? (gpas[gpas.Count / 2 - 1] + gpas[gpas.Count / 2]) / 2.0
                : gpas[gpas.Count / 2];
            Console.WriteLine($"\nMedian GPA: {medianGPA:F2}");

            double meanGPA = gpas.Average();
            double stdDev = Math.Sqrt(gpas.Average(g => Math.Pow(g - meanGPA, 2)));
            Console.WriteLine($"Standard Deviation of GPA: {stdDev:F2}");

            var ageGPA = _students.Select(s => (Age: s.Age, GPA: s.GPA)).ToList();
            double meanAge = ageGPA.Average(x => x.Age);
            double correlation = ageGPA.Sum(x => (x.Age - meanAge) * (x.GPA - meanGPA)) /
                Math.Sqrt(ageGPA.Sum(x => Math.Pow(x.Age - meanAge, 2)) * ageGPA.Sum(x => Math.Pow(x.GPA - meanGPA, 2)));
            Console.WriteLine($"Correlation between Age and GPA: {correlation:F2}");

            var outliers = _students.Where(s => Math.Abs(s.GPA - meanGPA) > 2 * stdDev).Select(s => s.Name);
            Console.WriteLine("\nOutliers (GPA > 2 std dev from mean):");
            Console.WriteLine(string.Join(", ", outliers.Any() ? outliers : new[] { "None" }));

            var percentiles = _students.OrderBy(s => s.GPA)
                .Select((s, i) => new { s.Name, Percentile = (i + 1.0) / _students.Count * 100 });
            Console.WriteLine("\nPercentile Rankings:");
            foreach (var p in percentiles)
            {
                Console.WriteLine($"{p.Name}: {p.Percentile:F2}%");
            }
        }

        // Challenge 4: Data Pivot Operations
        public void PivotOperations()
        {
            Console.WriteLine("=== PIVOT OPERATIONS ===");
            
            // TODO: Research pivot table concepts
            // Create pivot tables showing:
            // 1. Students by Major vs GPA ranges (3.0-3.5, 3.5-4.0, etc.)
            // 2. Course enrollment by semester and major
            // 3. Grade distribution across instructors

            // 1. Students by Major vs GPA ranges
            var gpaRanges = new[] { (0.0, 3.0), (3.0, 3.5), (3.5, 4.0) };
            var pivotByMajorGPA = _students.GroupBy(s => s.Major)
                .SelectMany(g => gpaRanges.Select(r => new
                {
                    Major = g.Key,
                    GPARange = $"{r.Item1}-{r.Item2}",
                    Count = g.Count(s => s.GPA >= r.Item1 && s.GPA < r.Item2)
                }));
            Console.WriteLine("\nStudents by Major and GPA Range:");
            foreach (var item in pivotByMajorGPA)
            {
                Console.WriteLine($"{item.Major}, GPA {item.GPARange}: {item.Count} students");
            }

            // 2. Course enrollment by semester and major
            var enrollmentBySemesterMajor = _students
                .SelectMany(s => s.Courses.Select(c => new { s.Major, c.Semester }))
                .GroupBy(x => new { x.Major, x.Semester })
                .Select(g => new { g.Key.Major, g.Key.Semester, Count = g.Count() });
            Console.WriteLine("\nCourse Enrollment by Semester and Major:");
            foreach (var item in enrollmentBySemesterMajor)
            {
                Console.WriteLine($"{item.Major}, {item.Semester}: {item.Count} courses");
            }

            // 3. Grade distribution across instructors
            var gradeDistribution = _students
                .SelectMany(s => s.Courses)
                .GroupBy(c => c.Instructor)
                .Select(g => new
                {
                    Instructor = g.Key,
                    AverageGrade = g.Average(c => c.Grade),
                    CourseCount = g.Count()
                });
            Console.WriteLine("\nGrade Distribution by Instructor:");
            foreach (var item in gradeDistribution)
            {
                Console.WriteLine($"{item.Instructor}: Avg Grade = {item.AverageGrade:F2}, Courses = {item.CourseCount}");
            }
        }

        // Sample data generator
        private List<Student> GenerateSampleData()
        {
            return new List<Student>
            {
                new Student
                {
                    Id = 1, Name = "Alice Johnson", Age = 20, Major = "Computer Science", 
                    GPA = 3.8, EnrollmentDate = new DateTime(2022, 9, 1),
                    Email = "alice.j@university.edu",
                    Address = new Address { Street = "123 Main St", City = "Seattle", State = "WA", ZipCode = "98101" },
                    Courses = new List<Course>
                    {
                        new Course { Code = "CS101", Name = "Intro to Programming", Credits = 3, Grade = 3.7, Semester = "Fall 2022", Instructor = "Dr. Smith" },
                        new Course { Code = "MATH201", Name = "Calculus II", Credits = 4, Grade = 3.9, Semester = "Fall 2022", Instructor = "Prof. Johnson" }
                    }
                },
                new Student
                {
                    Id = 2, Name = "Bob Wilson", Age = 22, Major = "Mathematics", 
                    GPA = 3.2, EnrollmentDate = new DateTime(2021, 9, 1),
                    Email = "bob.w@university.edu",
                    Address = new Address { Street = "456 Oak St", City = "Portland", State = "OR", ZipCode = "97201" },
                    Courses = new List<Course>
                    {
                        new Course { Code = "MATH301", Name = "Linear Algebra", Credits = 3, Grade = 3.3, Semester = "Spring 2023", Instructor = "Dr. Brown" },
                        new Course { Code = "STAT101", Name = "Statistics", Credits = 3, Grade = 3.1, Semester = "Spring 2023", Instructor = "Prof. Davis" }
                    }
                },
                // Add more sample students...
                new Student
                {
                    Id = 3, Name = "Carol Davis", Age = 19, Major = "Computer Science", 
                    GPA = 3.9, EnrollmentDate = new DateTime(2023, 9, 1),
                    Email = "carol.d@university.edu",
                    Address = new Address { Street = "789 Pine St", City = "San Francisco", State = "CA", ZipCode = "94101" },
                    Courses = new List<Course>
                    {
                        new Course { Code = "CS102", Name = "Data Structures", Credits = 4, Grade = 4.0, Semester = "Fall 2023", Instructor = "Dr. Smith" },
                        new Course { Code = "CS201", Name = "Algorithms", Credits = 3, Grade = 3.8, Semester = "Fall 2023", Instructor = "Prof. Lee" }
                    }
                }
            };
        }
    }

    // TODO: Implement these extension methods
    public static class StudentExtensions
    {
        // Challenge: Implement custom extension methods
        // public static IEnumerable<Student> FilterByAgeRange(this IEnumerable<Student> students, int minAge, int maxAge)
        // public static Dictionary<string, double> AverageGPAByMajor(this IEnumerable<Student> students)
        // public static string ToGradeReport(this Student student)
        // public static StudentStatistics CalculateStatistics(this IEnumerable<Student> students)

        public static IEnumerable<Student> FilterByAgeRange(this IEnumerable<Student> students, int minAge, int maxAge)
        {
            return students.Where(s => s.Age >= minAge && s.Age <= maxAge);
        }

        public static Dictionary<string, double> AverageGPAByMajor(this IEnumerable<Student> students)
        {
            return students.GroupBy(s => s.Major)
                .ToDictionary(g => g.Key, g => g.Average(s => s.GPA));
        }

        public static string ToGradeReport(this Student student)
        {
            return $"Student: {student.Name}\n" +
                   $"GPA: {student.GPA:F2}\n" +
                   $"Courses:\n" +
                   string.Join("\n", student.Courses.Select(c => $"  {c.Code}: {c.Name} ({c.Grade:F2})"));
        }

        public static StudentStatistics CalculateStatistics(this IEnumerable<Student> students)
        {
            var gpas = students.Select(s => s.GPA).OrderBy(g => g).ToList();
            double mean = gpas.Average();
            double median = gpas.Count % 2 == 0
                ? (gpas[gpas.Count / 2 - 1] + gpas[gpas.Count / 2]) / 2.0
                : gpas[gpas.Count / 2];
            double stdDev = Math.Sqrt(gpas.Average(g => Math.Pow(g - mean, 2)));

            return new StudentStatistics
            {
                MeanGPA = mean,
                MedianGPA = median,
                StandardDeviation = stdDev
            };
        }

        // Helper for dynamic sorting
        public static IQueryable<Student> OrderByProperty(this IQueryable<Student> students, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(Student), "s");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);
            return students.Provider.CreateQuery<Student>(
                Expression.Call(
                    typeof(Queryable),
                    "OrderBy",
                    new[] { typeof(Student), property.Type },
                    students.Expression,
                    lambda
                )
            );
        }

        // Helper for dynamic grouping
        public static IQueryable<IGrouping<object, Student>> GroupByProperty(this IQueryable<Student> students, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(Student), "s");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);
            return students.Provider.CreateQuery<IGrouping<object, Student>>(
                Expression.Call(
                    typeof(Queryable),
                    "GroupBy",
                    new[] { typeof(Student), property.Type },
                    students.Expression,
                    lambda
                )
            );
        }
    }

    // TODO: Define this class for statistical operations
    public class StudentStatistics
    {
        // Properties for mean, median, mode, standard deviation, etc.
        public double MeanGPA { get; set; }
        public double MedianGPA { get; set; }
        public double StandardDeviation { get; set; }
    }

    public class HW3_LinqDataProcessor
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== HOMEWORK 3: LINQ DATA PROCESSOR ===");
            Console.WriteLine();
            
            Console.WriteLine("BASIC REQUIREMENTS:");
            Console.WriteLine("1. Implement basic LINQ queries (filtering, grouping, sorting)");
            Console.WriteLine("2. Use SelectMany for course data");
            Console.WriteLine("3. Calculate averages and aggregations");
            Console.WriteLine();
            
            Console.WriteLine("ADVANCED REQUIREMENTS:");
            Console.WriteLine("1. Create custom LINQ extension methods");
            Console.WriteLine("2. Implement dynamic queries using Expression Trees");
            Console.WriteLine("3. Perform statistical analysis (median, std dev, correlation)");
            Console.WriteLine("4. Create pivot table operations");
            Console.WriteLine();

            LinqDataProcessor processor = new LinqDataProcessor();
            
            // Students should implement all these methods
            processor.BasicQueries();
            processor.CustomExtensionMethods();
            processor.DynamicQueries();
            processor.StatisticalAnalysis();
            processor.PivotOperations();

            Console.ReadKey();
        }
    }
}