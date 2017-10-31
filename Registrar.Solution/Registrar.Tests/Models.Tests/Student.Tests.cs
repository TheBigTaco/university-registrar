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
      Course.ClearAll();
      Student.ClearAll();
    }
    public StudentTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_registrar_tests;";
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
    [TestMethod]
    public void Find_FindsStudentInDatabase_Student()
    {
      DateTime enrollmentDate = new DateTime(2017,3,26);
      Student adam = new Student("Adam", enrollmentDate);
      adam.Save();

      Assert.AreEqual(adam, Student.Find(adam.Id));
    }
    [TestMethod]
    public void AddCourse_AddsCourseToStudent_CourseList()
    {
      Course epicodus = new Course("CSharp", "C#");
      epicodus.Save();

      DateTime enrollmentDate = new DateTime(2017,3,26);
      Student adam = new Student("Adam", enrollmentDate);
      adam.Save();

      adam.AddCourse(epicodus);

      List<Course> result = adam.GetCourses();
      List<Course> testList = new List<Course>{epicodus};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void GetCourses_GetsCoursesFromStudent_CourseList()
    {
      Course epicodus = new Course("CSharp", "C#");
      epicodus.Save();
      Course life = new Course("Adulting for immature adults", "Adt101");
      life.Save();

      DateTime enrollmentDate = new DateTime(2017,3,26);
      Student adam = new Student("Adam", enrollmentDate);
      adam.Save();

      adam.AddCourse(epicodus);
      adam.AddCourse(life);

      List<Course> result = adam.GetCourses();
      List<Course> testList = new List<Course>{epicodus, life};

      CollectionAssert.AreEqual(testList, result);
    }
  }
}
