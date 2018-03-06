using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private string _name;

    public Client(string name, int id = 0)
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
    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId()) == newClient.GetId();
        bool nameEquality = (this.GetName()) == newClient.GetName();

        return (idEquality && nameEquality);
      }
    }
    //does something
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
    //Method to get all clients from the database
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>();
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      //This Query is telling the database to select all clients in the table called clients & sort the clients alphabetically
      cmd.CommandText = @"SELECT * FROM clients ORDER BY name;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        //The clients that are retrieved from the database will be added to the list which will be displayed on the webpage
        Client newClient = new Client(clientName, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }
    //Mathod to save new entries to the database
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name) VALUES (@name);";

      MySqlParameter clientName = new MySqlParameter();
      clientName.ParameterName = "@name";
      clientName.Value = this._name;
      cmd.Parameters.Add(clientName);

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@id";
      id.Value = this._id;
      cmd.Parameters.Add(id);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    //Find clients by id in the database
    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      //This Query will select all clients from the id where the id matches the id of the client that is clicked on on the webpage
      cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

      MySqlParameter clientId = new MySqlParameter();
      clientId.ParameterName = "@searchId";
      clientId.Value = id;
      cmd.Parameters.Add(clientId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      //The variables are created with default values so that the client retrieved from the database has somewhere to be stored. The found client info will change the default values of the variables.
      int foundId = 0;
      string foundName = "";

      while(rdr.Read())
      {
        //The values in the parens are just telling the database that the id, name, and stylist id of the client are at these particular indexes on the array(table).
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
      }
      //The found clients are put in the list.
      Client foundClient = new Client(foundName, foundId);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundClient;
    }
    //Method to update the name of the client
    public void UpdateName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _name = newName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients_stylists (stylist_id, client_id) VALUES (@StylistId, @ClientId);";

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@StylistId";
      stylist_id.Value = stylist_id;
      cmd.Parameters.Add(stylist_id);

      MySqlParameter clientId = new MySqlParameter();
      clientId.ParameterName = "@ClientId";
      clientId.Value = _id;
      cmd.Parameters.Add(clientId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Stylist> GetStylists()
    {
      List<Stylist> newStylistList = new List<Stylist>{};

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM stylists
                JOIN clients_stylists ON (stylists.id = clients_stylists.stylist_id)
                JOIN clients ON (clients_stylists.client_id = clients.id)
                WHERE clients.id = @ClientId;";

      MySqlParameter ClientId = new MySqlParameter();
      ClientId.ParameterName = "@ClientId";
      ClientId.Value = this._id;
      cmd.Parameters.Add(ClientId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);


        Stylist newStylist = new Stylist(name, id);
        newStylistList.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStylistList;
    }
    //Method to delete specific client by id.
    public void DeleteOne()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      //Method to delete client info from the table based on id
      cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";

      MySqlParameter clientId = new MySqlParameter();
      clientId.ParameterName = "@thisId";
      clientId.Value = _id;
      cmd.Parameters.Add(clientId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    // //Method to clear all clients from the database.
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients; DELETE FROM clients_stylists";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
