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

     Stylist firstStylist = new Stylist("Amy");
     Stylist secondStylist = new Stylist("Amy");

     Assert.Equal(firstStylist, secondStylist);
   }


   public void Dispose()
   {
     Client.DeleteAll();
   }

    }
  }
