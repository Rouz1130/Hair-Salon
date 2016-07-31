using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Stylist
  {
    // properties

    private int _id;
    private string _name;


    // constructor
    public Stylist(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }


    // getters and setters
    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};

      SqlConnection conn = DB.Connection();
      conn.Open();
      //using parameters: creating sql command
      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
      // no placeholder in the above command : place holder example is @stylists
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, stylistId);
        allStylists.Add(newStylist);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allStylists;
    }
    // like the kitten example, allows us too use two same names without it getting confused
    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool nameEquality = (this.GetName() == newStylist.GetName());
        return (nameEquality);
      }
    }


    public void Save()
    {
      // 3 steps in when we use parameters in our queries
      SqlConnection conn = DB.Connection();
      conn.Open();

      // placeholder @sytlistName
      SqlCommand cmd = new SqlCommand("INSERT INTO stylists(name) OUTPUT INSERTED.id VALUES (@stylistName);", conn);
      // declare an SqlParameter object and assign values
      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@StylistName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);//adding the sqlparameter object to the sqlcommand command properties
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
