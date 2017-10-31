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
      University.ClearAllData();
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
    public void GetCourses_GetsCoursesFromStudent_CourseList()
    {
      Course epicodus = new Course("CSharp", "C#");
      epicodus.Save();
      Course life = new Course("Adulting for immature adults", "Adt101");
      life.Save();

      DateTime enrollmentDate = new DateTime(2017,3,26);
      Student adam = new Student("Adam", enrollmentDate);
      adam.Save();

      University.AddStudentToCourse(adam, epicodus);
      University.AddStudentToCourse(adam, life);

      List<Course> result = adam.GetCourses();
      List<Course> testList = new List<Course>{epicodus, life};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void RemoveCourse_RemoveCourseFromStudent_RemovesCourse()
    {
      Course epicodus = new Course("CSharp", "C#");
      epicodus.Save();
      Course life = new Course("Adulting for immature adults", "Adt101");
      life.Save();

      DateTime enrollmentDate = new DateTime(2017,3,26);
      Student adam = new Student("Adam", enrollmentDate);
      adam.Save();

      University.AddStudentToCourse(adam, epicodus);
      University.AddStudentToCourse(adam, life);
      Student.RemoveCourse(adam.Id, epicodus.Id);

      List<Course> result = adam.GetCourses();
      List<Course> testList = new List<Course>{life};

      CollectionAssert.AreEqual(result, testList);
    }
    [TestMethod]
    public void Update_UpdatesStudentInfo_InfoUpdated()
    {
      DateTime enrollmentDate = new DateTime(2017,3,26);
      Student adam = new Student("Adam", enrollmentDate);
      adam.Save();
      DateTime enrollmentDate2 = new DateTime(2017,3,14);
      Student.Update(adam.Id, "Rane", enrollmentDate2);

      Student result = Student.Find(adam.Id);
      Student test = new Student("Rane", enrollmentDate2);

      Assert.AreEqual(test, result);
    }
  }
}
