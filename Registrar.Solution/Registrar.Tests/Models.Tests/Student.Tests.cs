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
      Student.ClearAll
    }
    [TestMethod]
    public void Method_Description_ExpectedValue()
    {
      Assert.AreEqual(var1, method(input));
    }
  }
}
