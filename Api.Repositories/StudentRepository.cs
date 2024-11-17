using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models;
using Api.Utilities;

namespace Api.Repositories
{
  public class StudentRepository : IStudentRepository
  {
    private readonly DatabaseHelper _databaseHelper;

    public StudentRepository(DatabaseHelper databaseHelper)
    {
      _databaseHelper = databaseHelper;
    }

    /// <summary>
    /// Gets all students from the database.
    /// </summary>
    public async Task<IEnumerable<Student>> GetStudentsAsync()
    {
      var query = "SELECT * FROM Students";
      var dataTable = _databaseHelper.ExecuteQuery(query, CommandType.Text);

      return from DataRow row in dataTable.Rows
             select new Student
             {
               StudentID = (int)row["StudentID"],
               FullName = (string)row["FullName"],
               DateOfBirth = (DateTime)row["DateOfBirth"],
               RollNumber = (string)row["RollNumber"],
               Address = (string)row["Address"]
             };
    }

    /// <summary>
    /// Gets a single student by ID.
    /// </summary>
    public async Task<Student> GetStudentByIdAsync(int id)
    {
      var query = "SELECT * FROM Students WHERE StudentID = @StudentID";
      var parameters = new Dictionary<string, object>
            {
                { "@StudentID", id }
            };

      var dataTable = _databaseHelper.ExecuteQuery(query, CommandType.Text, parameters);
      if (dataTable.Rows.Count == 0)
        return null;

      var row = dataTable.Rows[0];
      return new Student
      {
        StudentID = (int)row["StudentID"],
        FullName = (string)row["FullName"],
        DateOfBirth = (DateTime)row["DateOfBirth"],
        RollNumber = (string)row["RollNumber"],
        Address = (string)row["Address"]
      };
    }

    /// <summary>
    /// Adds a new student to the database.
    /// </summary>
    public async Task AddStudentAsync(Student student)
    {
      var query = @"INSERT INTO Students (FullName, DateOfBirth, RollNumber, Address)
                          VALUES (@FullName, @DateOfBirth, @RollNumber, @Address)";
      var parameters = new Dictionary<string, object>
            {
                { "@FullName", student.FullName },
                { "@DateOfBirth", student.DateOfBirth },
                { "@RollNumber", student.RollNumber },
                { "@Address", student.Address }
            };

      _databaseHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
    }

    /// <summary>
    /// Updates an existing student in the database.
    /// </summary>
    public async Task UpdateStudentAsync(Student student)
    {
      var query = @"UPDATE Students 
                          SET FullName = @FullName, DateOfBirth = @DateOfBirth, 
                              RollNumber = @RollNumber, Address = @Address
                          WHERE StudentID = @StudentID";
      var parameters = new Dictionary<string, object>
            {
                { "@StudentID", student.StudentID },
                { "@FullName", student.FullName },
                { "@DateOfBirth", student.DateOfBirth },
                { "@RollNumber", student.RollNumber },
                { "@Address", student.Address }
            };

      _databaseHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
    }

    /// <summary>
    /// Deletes a student by ID.
    /// </summary>
    public async Task DeleteStudentAsync(int id)
    {
      var query = "DELETE FROM Students WHERE StudentID = @StudentID";
      var parameters = new Dictionary<string, object>
            {
                { "@StudentID", id }
            };

      _databaseHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
    }
  }
}
