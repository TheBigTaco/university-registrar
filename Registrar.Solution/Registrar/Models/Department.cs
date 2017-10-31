using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Registrar.Models
{
  public class Department
  {
    public int Id {get; private set;}
    public string Name {get;}

    public Department(string name, int id = 0)
    {
      Name = name;
      Id = id;
    }

    public override bool Equals(object otherDepartment)
    {
      if(!(otherDepartment is Department))
      {
        return false;
      }
      else
      {
        Department newDepartment = (Department) otherDepartment;
        bool nameEquality = this.Name.Equals(newDepartment.Name);
        return(nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public static List<Department> GetAll()
    {
      List<Department> allDepartments = new List<Department> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM departments;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int departmentId = rdr.GetInt32(0);
        string departmentName = rdr.GetString(1);
        Department newDepartment = new Department(departmentName, departmentId);
        allDepartments.Add(newDepartment);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allDepartments;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"INSERT INTO departments (name) VALUES (@name)";

      cmd.Parameters.Add(new MySqlParameter("@name", this.Name));

      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Department Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText =@"SELECT * FROM departments WHERE id = (@searchId);";

      cmd.Parameters.Add(new MySqlParameter("@searchId", id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int departmentId = 0;
      string departmentName = "";
      while(rdr.Read())
      {
        departmentId = rdr.GetInt32(0);
        departmentName = rdr.GetString(1);
      }
      Department newDepartment = new Department(departmentName, departmentId);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return newDepartment;
    }

    public static void Update(int id, string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE departments SET name = @NewName WHERE id = @Id;";
      cmd.Parameters.Add(new MySqlParameter("@NewName", newName));
      cmd.Parameters.Add(new MySqlParameter("@Id", id));
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddCourse(Course newCourse)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO courses_departments (course_id, department_id) VALUES (@courseId, @departmentId);";

      cmd.Parameters.Add(new MySqlParameter("@courseId", newCourse.Id));
      cmd.Parameters.Add(new MySqlParameter("@departmentId", Id));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void RemoveCourse(int departmentId, int courseId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses_departments WHERE department_id = @DepartmentId AND course_id = @CourseId;";
      cmd.Parameters.Add(new MySqlParameter("@DepartmentId", departmentId));
      cmd.Parameters.Add(new MySqlParameter("@CourseId", courseId));
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void Remove(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses_departments WHERE department_id = @DepartmentId; DELETE FROM departments WHERE id = @DepartmentId;";
      cmd.Parameters.Add(new MySqlParameter("@DepartmentId", id));
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Course> GetCourses()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT courses.* FROM courses JOIN courses_departments ON (courses.id = courses_departments.course_id) JOIN departments ON (courses_departments.department_id = departments.id) WHERE departments.id = @departmentId;";

      cmd.Parameters.Add(new MySqlParameter("@departmentId", Id));
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Course> allCoursesInDepartment = new List<Course>{};

      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNumber, courseId);
        allCoursesInDepartment.Add(newCourse);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCoursesInDepartment;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses_departments; DELETE FROM students_departments; DELETE FROM departments;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
