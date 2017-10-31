using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registrar.Models;

namespace Registrar.Models.Tests
{
  [TestClass]
  public class StudentTests : IDisposable
  {
    public void Dispose()
    {
      Student.ClearAll();
    }
    public StudentTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_registrar;";
    }
    [TestMethod]
    public void Equals_EqualsOverrideSuccessful_True()
    {
      DateTime enrollmentDate = new DateTime(2017,3,26);
      Student adam = new Student("Adam", enrollmentDate);
      Student adamT = new Student("Adam", enrollmentDate);
      Assert.AreEqual(adam, adamT);
    }
    [TestMethod]
    public void GetAll_CheckDatabaseEmpty_0()
    {
      int result = Student.GetAll().Count;

      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Save_SaveStudentToDatabase_StudentList()
    {
      DateTime enrollmentDate = new DateTime(2017,3,26);
      Student adam = new Student("Adam", enrollmentDate);
      adam.Save();

      List<Student> result = Student.GetAll();
      List<Student> testList = new List<Student>{adam};

      CollectionAssert.AreEqual(testList, result);
    }
  }
}
