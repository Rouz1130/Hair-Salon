using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Client.DeleteAll();
    }

    [Fact]
    public void Test1_DatabaseEmptyAtFirst()
    {
      int result = Client.GetAll().Count;

      Assert.Equal(0, result);
    }



    [Fact]
    public void Test2_Equal_ReturnsTrueIfNameAreTheSame()
    {

      Client firstClient = new Client("Amy",1);
      Client secondClient = new Client("Amy",1);

      Assert.Equal(firstClient, secondClient);
    }


    [Fact]
    public void Test3_Save_AssignsIdToObject()
    {


      Client testClient = new Client("Alex",1);

      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      Assert.Equal(testId, result);
    }


    [Fact]
    public void Test5_Find_FindClientInDatabase()
    {

      Client testClient = new Client("Robert",1);
      testClient.Save();

      Client foundClient = Client.Find(testClient.GetId());


      Assert.Equal(testClient, foundClient);
    }


  }
}
