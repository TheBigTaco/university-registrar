using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Registrar.Models
{
  public class Student
  {
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
