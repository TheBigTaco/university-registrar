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
      Course.ClearAll();
      Student.ClearAll();
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
    [TestMethod]
    public void AddStudent_AddsStudentToCourse_StudentList()
    {
      Course epicodus = new Course("CSharp", "C#");
      epicodus.Save();

      DateTime enrollmentDate = new DateTime(2017,3,26);
      Student adam = new Student("Adam", enrollmentDate);
      adam.Save();

      epicodus.AddStudent(adam);

      List<Student> result = epicodus.GetStudents();
      List<Student> testList = new List<Student>{adam};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void GetStudents_GetsStudentsFromCourse_StudentList()
    {
      Course epicodus = new Course("CSharp", "C#");
      epicodus.Save();

      DateTime enrollmentDate = new DateTime(2017,3,26);
      Student adam = new Student("Adam", enrollmentDate);
      DateTime enrollmentDate2 = new DateTime(2017,3,14);
      Student rane = new Student("Rane", enrollmentDate2);
      adam.Save();
      rane.Save();

      epicodus.AddStudent(adam);
      epicodus.AddStudent(rane);

      List<Student> result = epicodus.GetStudents();
      List<Student> testList = new List<Student>{adam, rane};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Remove_RemoveStudentEntirelyFromEverythingEver_RemovesStudent()
    {
      DateTime enrollmentDate = new DateTime(2017,3,26);
      Student adam = new Student("Adam", enrollmentDate);
      adam.Save();
      Course epicodus = new Course("CSharp", "C#");
      epicodus.Save();
      Course life = new Course("Adulting for immature adults", "Adt101");
      life.Save();
      adam.AddCourse(epicodus);
      adam.AddCourse(life);

      Course.Remove(life.Id);
      List<Course> result1 = Course.GetAll();
      List<Course> result2 = adam.GetCourses();
      List<Course> testList = new List<Course> {epicodus};

      CollectionAssert.AreEqual(result1, testList);
      CollectionAssert.AreEqual(result2, testList);
    }
  }
}
