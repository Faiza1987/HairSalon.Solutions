using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _id;
    private string _name;

    public Stylist(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }
    //GETTERS AND SETTERS
    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }
    public void SetName(string name)
    {
      _name = name;
    }
    //Will overrirde the new entry into a database if its exact match already exists in the database
    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId()) == newStylist.GetId();
        bool nameEquality = (this.GetName()) == newStylist.GetName();

        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
    //Method to get all stylists from the database
    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      //Query to get all stylists from stylists table and order it alphabetically
      cmd.CommandText = @"SELECT * FROM stylists ORDER BY name;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        //The stylists that are retrieved from the database will be added to the list which will be displayed on the webpage
        Stylist newStylist = new Stylist(stylistName, stylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }
    //Method to get all clients from the database that are attached to a specific stylist's id
    public List<Client> GetAllClients()
    {
      List<Client> allClients = new List<Client>();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      //This Query tells the database to select clients from the clients table that all have the same stylist id
      cmd.CommandText = @"SELECT * FROM clients WHERE stylistId = @thisId;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@thisId";
      stylistId.Value = this._id;
      cmd.Parameters.Add(stylistId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylist = rdr.GetInt32(2);
        //The clients that are retrieved from the database will be added to the list which will be displayed on the webpage
        Client newClient = new Client(clientName, clientStylist, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }
    //Method to save new entries to the database
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      //Query to tell the database to insert these new entries into the stylists table by name
      cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@name);";

      MySqlParameter stylistName = new MySqlParameter();
      stylistName.ParameterName = "@name";
      stylistName.Value = this._name;
      cmd.Parameters.Add(stylistName);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    //Method to find stylists from the database based on the id
    public static Stylist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      //This Query will select all stylists from the id where the id matches the id of the stylist that is clicked on on the webpage
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@searchId";
      stylistId.Value = id;
      cmd.Parameters.Add(stylistId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      //The variables are created with default values so that the client retrieved from the database has somewhere to be stored. The found client info will change the default values of the variables.
      int foundId = 0;
      string foundName = "";

      while(rdr.Read())
      {
        //The values in the parens are just telling the database that the id, name are at these particular indexes on the array(table).
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
      }
      //The found clients are put in the list.
      Stylist foundStylist = new Stylist(foundName, foundId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStylist;
    }
    //Method to update the name of the client
    public void UpdateName(string newNameInput)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      //Query to update stylist info in the table called stylists based on id.
      cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @thisId;";
      //This will look for the stylist by id
      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@thisId";
      stylistId.Value = this._id;
      cmd.Parameters.Add(stylistId);
      //And then the new name will overwrite the name that is already in the table
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@newName";
      newName.Value = newNameInput;
      cmd.Parameters.Add(newName);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    //Method to delete specific stylists from the list based on the id
    public void DeleteThis()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      //Query to delete client info in the table called Clients based on id.
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@thisId";
      stylistId.Value = this._id;
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    //Method to delete all stylists
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
