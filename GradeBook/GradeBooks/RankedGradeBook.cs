using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            this.Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");

            var orderedGrades = Students.
                                    OrderByDescending(s => s.AverageGrade).
                                    Select(s => s.AverageGrade).
                                    ToList();

            var threshold20 = (int)Math.Ceiling(Students.Count * 0.2);

            if (orderedGrades[threshold20 - 1] <= averageGrade)
                return 'A';
            if (orderedGrades[threshold20 * 2 - 1] <= averageGrade)
                return 'B';
            if (orderedGrades[threshold20 * 3 - 1] <= averageGrade)
                return 'C';
            if (orderedGrades[threshold20 * 4 - 1] <= averageGrade)
                return 'D';

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
