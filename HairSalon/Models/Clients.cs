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

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetStylistId()
        {
            return _stylistId;
        }

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

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client>();
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients ORDER BY name;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                int clientStylist = rdr.GetInt32(2);
                Client newClient = new Client(clientName, clientStylist, clientId);
                allClients.Add(newClient);
            }
            conn.Close();
            return allClients;
        }

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
        }

        public static Client Find(int searchId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@searchId";
            clientId.Value = searchId;
            cmd.Parameters.Add(clientId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int foundId = 0;
            string foundName = "";
            int foundStylistId = 0;

            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
                foundStylistId = rdr.GetInt32(2);
            }

            Client foundClient = new Client(foundName, foundStylistId, foundId);
            conn.Close();
            return foundClient;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteThis()
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";

        MySqlParameter clientId = new MySqlParameter();
        clientId.ParameterName = "@thisId";
        clientId.Value = this._id;
        cmd.Parameters.Add(clientId);

        cmd.ExecuteNonQuery();
        conn.Close();
        }

        public void UpdateName(string newNameInput)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @thisId;";

            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@thisId";
            clientId.Value = this._id;
            cmd.Parameters.Add(clientId);

            MySqlParameter newName = new MySqlParameter();
            newName.ParameterName = "@newName";
            newName.Value = newNameInput;
            cmd.Parameters.Add(newName);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
