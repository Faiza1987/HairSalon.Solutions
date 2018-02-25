using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace HairSalon.Models
{
    public class Client
    {
        private int _id;
        private string _name;
        private int _stylistId;

        public Client(string name, int stylistId, int id = 0)
        {
            _name = name;
            _stylistId = stylistId;
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
        public int GetStylistId()
        {
            return _stylistId;
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
                int clientStylist = rdr.GetInt32(2);
                //The clients that are retrieved from the database will be added to the list which will be displayed on the webpage
                Client newClient = new Client(clientName, clientStylist, clientId);
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
            cmd.CommandText = @"INSERT INTO clients (name, stylistId) VALUES (@name, @stylist_id);";

            MySqlParameter clientName = new MySqlParameter();
            clientName.ParameterName = "@name";
            clientName.Value = this._name;
            cmd.Parameters.Add(clientName);

            MySqlParameter clientStylist = new MySqlParameter();
            clientStylist.ParameterName = "@stylist_id";
            clientStylist.Value = this._stylistId;
            cmd.Parameters.Add(clientStylist);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if(conn != null)
            {
              conn.Dispose();
            }
        }
        //Find clients by id in the database
        public static Client Find(int searchId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            //This Query will select all clients from the id where the id matches the id of the client that is clicked on on the webpage
            cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@searchId";
            clientId.Value = searchId;
            cmd.Parameters.Add(clientId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            //The variables are created with default values so that the client retrieved from the database has somewhere to be stored. The found client info will change the default values of the variables.
            int foundId = 0;
            string foundName = "";
            int foundStylistId = 0;

            while(rdr.Read())
            {
              //The values in the parens are just telling the database that the id, name, and stylist id of the client are at these particular indexes on the array(table).
                foundId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
                foundStylistId = rdr.GetInt32(2);
            }
            //The found clients are put in the list.
            Client foundClient = new Client(foundName, foundStylistId, foundId);
            conn.Close();
            if(conn != null)
            {
              conn.Dispose();
            }
            return foundClient;
        }
        //Method to update the name of the client
        public void UpdateName(string newNameInput)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            //Query to update client info in the table called Clients based on id.
            cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @thisId;";

            //This will look for the cliend by id
            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@thisId";
            clientId.Value = this._id;
            cmd.Parameters.Add(clientId);
            //And then the new name will overwrite the name that is already in the table
            MySqlParameter newName = new MySqlParameter();
            newName.ParameterName = "@newName";
            newName.Value = newNameInput;
            cmd.Parameters.Add(newName);

            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
              conn.Dispose();
            }
        }
        //Mathod to delete specific client by id.
        public void DeleteThis()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            //Method to delete client info from the table based on id
            cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";

            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@thisId";
            clientId.Value = this._id;
            cmd.Parameters.Add(clientId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
              conn.Dispose();
            }
        }
        //Method to clear all clients from the database.
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
              conn.Dispose();
            }
        }
    }
}
