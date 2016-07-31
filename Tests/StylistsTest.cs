using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test1_DatabaseEmptyAtFirst()
    {
      int result = Stylist.GetAll().Count;
      Assert.Equal(0, result);
    }


    [Fact]
    public void Test2_Equal_ReturnsTrueIfNameAreTheSame()
    {

      Stylist firstStylist = new Stylist("Bobby");
      Stylist secondStylist = new Stylist("Bobby");

      Assert.Equal(firstStylist, secondStylist);
    }



    [Fact]
    public void Test3_SavesToDatabase()
    {
      Stylist testStylist = new Stylist("Jimmy");

      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      Assert.Equal(testList, result);
    }


    [Fact]
    public void Test4_Save_AssignIdToObject()
    {

     Stylist testStylist = new Stylist("Anthony");

     testStylist.Save();
     Stylist savedStylist = Stylist.GetAll()[0];

     int result = savedStylist.GetId();
     int testId = testStylist.GetId();

     Assert.Equal(testId, result);
    }



    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
