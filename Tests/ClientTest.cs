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
   public void Test3_Save_AssignIdToObject()
   {

     Client testClient = new Client("Alex");

     testClient.Save();
     Client savedClient = Client.GetAll()[0];

     int result = savedClient.GetId();
     int testId = testClient.GetId();

     Assert.Equal(testId, result);
   }



   public void Dispose()
   {
     Client.DeleteAll();
   }

    }
  }
