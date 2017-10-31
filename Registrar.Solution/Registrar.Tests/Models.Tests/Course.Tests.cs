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
  }
}
