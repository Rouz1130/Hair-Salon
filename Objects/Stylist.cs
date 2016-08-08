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

    public List<Client> GetClients()
  {
    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @StylistId;", conn);
    SqlParameter stylistIdParameter = new SqlParameter();
    stylistIdParameter.ParameterName = "@StylistId";
    stylistIdParameter.Value = this.GetId();
    cmd.Parameters.Add(stylistIdParameter);
    SqlDataReader rdr = cmd.ExecuteReader();

    List<Client> clients = new List<Client> {};
    while(rdr.Read())
    {
      int clientId = rdr.GetInt32(0);
      string clientName = rdr.GetString(1);
      int clientStylistId = rdr.GetInt32(2);
      Client newClient = new Client(clientName, clientStylistId, clientId);
      clients.Add(newClient);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return clients;
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
      //(setp 1) 3 steps in when we use parameters in our queries
      SqlConnection conn = DB.Connection();
      conn.Open();

      // placeholder @sytlistName (step 2)
      SqlCommand cmd = new SqlCommand("INSERT INTO stylists(name) OUTPUT INSERTED.id VALUES (@StylistName);", conn);
      // declare an SqlParameter object and assign values (step 3)
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


    public static Stylist Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);
      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = id.ToString();
      cmd.Parameters.Add(stylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundStylistId = 0;
      string foundStylistName = null;
      while(rdr.Read())
      {
        foundStylistId = rdr.GetInt32(0);
        foundStylistName = rdr.GetString(1);
      }
      Stylist foundStylist = new Stylist(foundStylistName, foundStylistId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundStylist;
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @NewName OUTPUT INSERTED.name WHERE id = @StylistId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
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
    //not a delete all method/function its a single delete run a test to see if it works
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @StylistId; DELETE FROM clients WHERE stylist_id = @StylistId;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();

      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();

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
