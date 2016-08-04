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

      Client firstClient = new Client("Amy");
      Client secondClient = new Client("Amy");

      Assert.Equal(firstClient, secondClient);
    }


    [Fact]
    public void Test3_Save_AssignsIdToObject()
    {


      Client testClient = new Client("Alex");

      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      Assert.Equal(testId, result);
    }


    [Fact]
    public void Test4_SavesToDatabase()
    {
      Client testClient = new Client("Paul");

      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      Assert.Equal(testList, result);
    }


    [Fact]
    public void Test5_Find_FindClientInDatabase()
    {

      Client testClient = new Client("Robert");
      testClient.Save();

      Client foundClient = Client.Find(testClient.GetId());


      Assert.Equal(testClient, foundClient);
    }


      [Fact]
      public void Test6_Update_UpdatesClientInDatabase()
      {
        string name = "Denzel";
        Client testClient = new Client(name);
        testClient.Save();

        string newName = "Kyle";
        testClient.Update(newName);
        string result = testClient.GetName();

        Assert.Equal(newName, result);
      }



  }
}
