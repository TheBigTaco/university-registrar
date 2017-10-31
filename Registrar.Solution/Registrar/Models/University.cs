using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Registrar.Models
{
  public static class University
  {
    public static void AddStudentToCourse(Student student, Course course)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students_courses (course_id, student_id) VALUES (@courseId, @studentId);";

      cmd.Parameters.Add(new MySqlParameter("@courseId", course.Id));
      cmd.Parameters.Add(new MySqlParameter("@studentId", student.Id));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void AddCourseToDepartment(Course newCourse, Department newDepartment)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO courses_departments (course_id, department_id) VALUES (@courseId, @departmentId);";

      cmd.Parameters.Add(new MySqlParameter("@courseId", newCourse.Id));
      cmd.Parameters.Add(new MySqlParameter("@departmentId", newDepartment.Id));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void AddStudentToDepartment(Student newStudent, Department newDepartment)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students_departments (student_id, department_id) VALUES (@studentId, @departmentId);";

      cmd.Parameters.Add(new MySqlParameter("@studentId", newStudent.Id));
      cmd.Parameters.Add(new MySqlParameter("@departmentId", newDepartment.Id));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void RemoveStudentFromCourse(int studentId, int courseId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students_courses WHERE student_id = @StudentId AND course_id = @CourseId;";
      cmd.Parameters.Add(new MySqlParameter("@StudentId", studentId));
      cmd.Parameters.Add(new MySqlParameter("@CourseId", courseId));
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void RemoveCourseFromDepartment(int courseId, int departmentId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses_departments WHERE department_id = @DepartmentId AND course_id = @CourseId;";
      cmd.Parameters.Add(new MySqlParameter("@CourseId", courseId));
      cmd.Parameters.Add(new MySqlParameter("@DepartmentId", departmentId));
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void RemoveStudentFromDepartment(int studentId, int departmentId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students_departments WHERE student_id = @StudentId AND department_id = @DepartmentId;";
      cmd.Parameters.Add(new MySqlParameter("@StudentId", studentId));
      cmd.Parameters.Add(new MySqlParameter("@DepartmentId", departmentId));
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void RemoveStudent(int studentId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students_courses WHERE student_id = @StudentId; DELETE FROM students_departments WHERE student_id = @StudentId; DELETE FROM students WHERE id = @StudentId;";
      cmd.Parameters.Add(new MySqlParameter("@StudentId", studentId));
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void RemoveCourse(int courseId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students_courses WHERE course_id = @CourseId; DELETE FROM courses_departments WHERE course_id = @CourseId; DELETE FROM courses WHERE id = @CourseId;";
      cmd.Parameters.Add(new MySqlParameter("@CourseId", courseId));
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void RemoveDepartment(int departmentId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses_departments WHERE department_id = @DepartmentId; DELETE FROM students_departments WHERE department_id = @DepartmentId; DELETE FROM departments WHERE id = @DepartmentId;";
      cmd.Parameters.Add(new MySqlParameter("@DepartmentId", departmentId));
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void ClearAllData()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses_departments; DELETE FROM students_departments; DELETE FROM students_courses; DELETE FROM students; DELETE FROM courses; DELETE FROM departments;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
