using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registrar.Models;

namespace Registrar.Models.Tests
{
  [TestClass]
  public class UniversityTests : IDisposable
  {
    public void Dispose()
    {
      University.ClearAllData();
    }
    public UniversityTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_registrar_tests;";
    }
    [TestMethod]
    public void RemoveStudent_RemoveStudentEntirelyFromEverythingEver_RemovesStudent()
    {
      Student adam = new Student("Adam", new DateTime(2017,3,26));
      Student rane = new Student("Rane", new DateTime(2017,3,14));
      adam.Save();
      rane.Save();
      Course epicodus = new Course("CSharp", "C#");
      epicodus.Save();
      University.AddStudentToCourse(adam, epicodus);
      University.AddStudentToCourse(rane, epicodus);

      University.RemoveStudent(adam.Id);
      List<Student> result1 = Student.GetAll();
      List<Student> result2 = epicodus.GetStudents();
      List<Student> testList = new List<Student> {rane};

      CollectionAssert.AreEqual(result1, testList);
      CollectionAssert.AreEqual(result2, testList);
    }
    [TestMethod]
    public void RemoveCourse_RemoveCourseEntirelyFromEverythingEver_RemovesCourse()
    {
      Course epicodus = new Course("CSharp", "C#");
      Course life = new Course("Adulting for immature adults", "Adt101");
      epicodus.Save();
      life.Save();
      Student adam = new Student("Adam", new DateTime(2017,3,26));
      adam.Save();
      University.AddStudentToCourse(adam, epicodus);
      University.AddStudentToCourse(adam, life);

      University.RemoveCourse(life.Id);
      List<Course> result1 = Course.GetAll();
      List<Course> result2 = adam.GetCourses();
      List<Course> testList = new List<Course> {epicodus};

      CollectionAssert.AreEqual(result1, testList);
      CollectionAssert.AreEqual(result2, testList);
    }
    [TestMethod]
    public void RemoveDepartment_RemoveDepartmentEntirelyFromEverythingEver_RemovesDepartment()
    {
      Department code = new Department("Code");
      code.Save();
      University.RemoveDepartment(code.Id);

      List<Department> result = Department.GetAll();
      List<Department> testList = new List<Department> {};

      CollectionAssert.AreEqual(result, testList);
    }
    [TestMethod]
    public void AddStudentToCourse_AddsStudentToCourseAndViceVersa_Add()
    {
      Course epicodus = new Course("CSharp", "C#");
      epicodus.Save();
      Student adam = new Student("Adam", new DateTime(2017,3,26));
      adam.Save();
      University.AddStudentToCourse(adam, epicodus);

      List<Course> resultCourses = adam.GetCourses();
      List<Course> testCourses = new List<Course>{epicodus};
      List<Student> resultStudents = epicodus.GetStudents();
      List<Student> testStudents = new List<Student> {adam};

      CollectionAssert.AreEqual(testCourses, resultCourses);
      CollectionAssert.AreEqual(testStudents, resultStudents);
    }
    [TestMethod]
    public void AddCourseToDepartment_AddsCourseToDepartment_CourseList()
    {
      Department code = new Department("Code");
      code.Save();
      Course epicodus = new Course("CSharp", "C#");
      epicodus.Save();

      University.AddCourseToDepartment(epicodus, code);

      List<Course> result = code.GetCourses();
      List<Course> testList = new List<Course>{epicodus};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void AddStudentToDepartment_AddsCourseToDepartment_CourseList()
    {
      Department code = new Department("Code");
      code.Save();
      Student adam = new Student("Adam", new DateTime(2017,3,26));
      adam.Save();

      University.AddStudentToDepartment(adam, code);

      List<Student> result = code.GetStudents();
      List<Student> testList = new List<Student>{adam};

      CollectionAssert.AreEqual(testList, result);
    }
  }
}
