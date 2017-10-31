using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registrar.Models;

namespace Registrar.Models.Tests
{
  [TestClass]
  public class CourseTests : IDisposable
  {
    public void Dispose()
    {
      Student.ClearAll();
      Course.ClearAll();
    }
    public CourseTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_registrar_tests;";
    }
    [TestMethod]
    public void Equals_EqualsOverrideSuccessful_True()
    {
      Course epicodus = new Course("CSharp", "C#");
      Course epicodusS = new Course("CSharp", "C#");
      Assert.AreEqual(epicodus, epicodusS);
    }
    [TestMethod]
    public void GetAll_CheckDatabaseEmpty_0()
    {
      int result = Course.GetAll().Count;

      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Save_SaveCourseToDatabase_CourseList()
    {
      Course epicodus = new Course("CSharp", "C#");
      epicodus.Save();

      List<Course> result = Course.GetAll();
      List<Course> testList = new List<Course>{epicodus};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_FindsCourseInDatabase_Course()
    {
      Course epicodus = new Course("CSharp", "C#");
      epicodus.Save();

      Assert.AreEqual(epicodus, Course.Find(epicodus.Id));
    }
  }
}
