using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Registrar.Models
{
  public class Course
  {
    public int Id {get;private set;}
    public string Name {get;}
    public string Number {get;}

    public Course(string name, string number, int id = 0)
    {
      Name = name;
      Number = number;
      Id = id;
    }

    public override bool Equals(object otherCourse)
    {
      if(!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        bool nameEquality = this.Name.Equals(newCourse.Name);
        bool numberEquality = this.Number.Equals(newCourse.Number);
        return(nameEquality && numberEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"INSERT INTO courses (name, number) VALUES (@name, @number)";

      cmd.Parameters.Add(new MySqlParameter("@name", this.Name));
      cmd.Parameters.Add(new MySqlParameter("@number", this.Number));

      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Course> GetAll()
    {
      List<Course> allCourses = new List<Course> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM courses;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNumber, courseId);
        allCourses.Add(newCourse);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allCourses;
    }

    public static Course Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText =@"SELECT * FROM courses WHERE id = (@searchId);";

      cmd.Parameters.Add(new MySqlParameter("@searchId", id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int courseId = 0;
      string courseName = "";
      string courseNumber = "";

      while(rdr.Read())
      {
        courseId = rdr.GetInt32(0);
        courseName = rdr.GetString(1);
        courseNumber = rdr.GetString(2);
      }
      Course newCourse = new Course(courseName, courseNumber, courseId);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return newCourse;
    }

    public static void Update(int id, string newName, string newNumber)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE courses SET name = @NewName, number = @NewNumber WHERE id = @Id;";
      cmd.Parameters.Add(new MySqlParameter("@NewName", newName));
      cmd.Parameters.Add(new MySqlParameter("@NewNumber", newNumber));
      cmd.Parameters.Add(new MySqlParameter("@Id", id));
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Student> GetStudents()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT students.* FROM courses JOIN students_courses ON (courses.id = students_courses.course_id) JOIN students ON (students_courses.student_id = students.id) WHERE courses.id = @courseId;";

      cmd.Parameters.Add(new MySqlParameter("@courseId", Id));
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Student> allStudentsInCourse = new List<Student>{};

      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        DateTime studentDate = rdr.GetDateTime(2);
        Student newStudent = new Student(studentName, studentDate, studentId);
        allStudentsInCourse.Add(newStudent);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStudentsInCourse;
    }
  }
}
