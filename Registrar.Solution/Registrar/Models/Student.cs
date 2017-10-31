using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Registrar.Models
{
  public class Student
  {
    public int Id{get; private set;}
    public string Name{get;}
    public DateTime EnrollmentDate{get;}

    public Student(string name, DateTime enrollmentDate, int id = 0)
    {
      Name = name;
      EnrollmentDate = enrollmentDate;
      Id = id;
    }

    public override bool Equals(object otherStudent)
    {
      if(!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        bool nameEquality = this.Name.Equals(newStudent.Name);
        bool enrollEquality = this.EnrollmentDate.Equals(newStudent.EnrollmentDate);
        return(nameEquality && enrollEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public static List<Student> GetAll()
    {
      List<Student> allStudents = new List<Student> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        DateTime studentDate = rdr.GetDateTime(2);
        Student newStudent = new Student(studentName, studentDate, studentId);
        allStudents.Add(newStudent);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allStudents;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"INSERT INTO students (name, enrollment_date) VALUES (@name, @enrollmentDate)";

      cmd.Parameters.Add(new MySqlParameter("@name", this.Name));
      cmd.Parameters.Add(new MySqlParameter("@enrollmentDate", this.EnrollmentDate.ToString("yyyy-MM-dd")));

      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Student Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText =@"SELECT * FROM students WHERE id = (@searchId);";

      cmd.Parameters.Add(new MySqlParameter("@searchId", id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int studentId = 0;
      string studentName = "";
      DateTime studentDate = new DateTime();;

      while(rdr.Read())
      {
        studentId = rdr.GetInt32(0);
        studentName = rdr.GetString(1);
        studentDate = rdr.GetDateTime(2);
      }
      Student newStudent = new Student(studentName, studentDate, studentId);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return newStudent;
    }

    public static void ClearAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM students;";
        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }
  }
}
