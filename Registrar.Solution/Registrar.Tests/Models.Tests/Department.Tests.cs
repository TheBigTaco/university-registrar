using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registrar.Models;

namespace Registrar.Models.Tests
{
  [TestClass]
  public class DepartmentTests : IDisposable
  {
    public void Dispose()
    {
      University.ClearAllData();
    }
    public DepartmentTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_registrar_tests;";
    }
    [TestMethod]
    public void Equals_EqualsOverrideSuccessful_True()
    {
      Department code = new Department("Code");
      Department codeS = new Department("Code");
      Assert.AreEqual(code, codeS);
    }
    [TestMethod]
    public void GetAll_CheckDatabaseEmpty_0()
    {
      int result = Department.GetAll().Count;

      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Save_SaveDepartmentToDatabase_DepartmentList()
    {
      Department code = new Department("Code");
      code.Save();

      List<Department> result = Department.GetAll();
      List<Department> testList = new List<Department>{code};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_FindsDepartmentInDatabase_Department()
    {
      Department code = new Department("Code");
      code.Save();

      Assert.AreEqual(code, Department.Find(code.Id));
    }
    [TestMethod]
    public void GetCourses_GetsCoursesFromDepartment_CourseList()
    {
      Department code = new Department("Code");
      code.Save();

      Course epicodus = new Course("CSharp", "C#");
      Course life = new Course("Adulting for immature adults", "Adt101");
      life.Save();
      epicodus.Save();
      University.AddCourseToDepartment(epicodus, code);
      University.AddCourseToDepartment(life, code);

      List<Course> result = code.GetCourses();
      List<Course> testList = new List<Course>{life, epicodus};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void GetStudents_GetsStudentsFromDepartment_StudentList()
    {
      Department code = new Department("Code");
      code.Save();

      Student adam = new Student("Adam", new DateTime(2017,3,26));
      adam.Save();
      Student rane = new Student("Adam", new DateTime(2016,3,26));
      rane.Save();

      University.AddStudentToDepartment(adam, code);
      University.AddStudentToDepartment(rane, code);

      List<Student> result = code.GetStudents();
      List<Student> testList = new List<Student>{adam, rane};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Update_UpdatesDepartmentInfo_InfoUpdated()
    {
      Department code = new Department("Code");
      code.Save();
      Department.Update(code.Id, "Cod");

      Department result = Department.Find(code.Id);
      Department test = new Department("Cod");

      Assert.AreEqual(test, result);
    }
  }
}
