using System;
using HairSalon;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
    public class Client
    {
        private string _fullName;
        private int _phoneNumber;
        private int _stylistId;
        private int _clientId;

        public Client(string fullName, int phoneNumber, int stylistId, int clientId = 0)
        {
            _fullName = fullName;
            _phoneNumber = phoneNumber;
            _stylistId = stylistId;
            _clientId = clientId;
        }
        //OVERRIDE CODE -> If 2 clients are identical, a duplicate will not be created.
        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool clientIdEquality = (this.GetClientId() == newClient.GetClientId());
                bool fullNameEquality = (this.GetFullName() == newClient.GetFullName());
                bool stylistEquality = (this.GetStylistId() == newClient.GetStylistId());

                return (clientIdEquality && fullNameEquality && stylistEquality);
            }
        }
        public override int GetHashCode()
        {
            return this.GetClientId().GetHashCode();
        }
        //GETTERS AND SETTERS
        public int GetClientId()
        {
            return _clientId;
        }
        public void SetClientId(int clientId)
        {
            _clientId = clientId;
        }
        public string GetFullName()
        {
            return _fullName;
        }
        public void SetFullName(string fullName)
        {
            _fullName = fullName;
        }
        public int GetPhoneNumber()
        {
            return _phoneNumber;
        }
        public void SetPhoneNumber(int phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }
        public int GetStylistId()
        {
            return _stylistId;
        }
        public void SetStylistId(int stylistId)
        {
            _stylistId = stylistId;
        }
        public static List<Client> GetAllClients()
        {
            //code here
            return null;
        }

        public void Save()
        {
            //code here

        }
        //Method to search for clients in the database via id
        public static Client Find(int id)
        {
            //code here
            return null;
        }
        //Method to update specific client info in the database. This is void because it will be affecting specific clients in the database and will not return anything - hence the term void.
        public void UpdateClientInfo(string newClientName, int newClientPhoneNumber)
        {
            //code here

        }
        //Method to delete specific clients in the database. This is void because it will be affecting specific clients in the database and will not return anything - hence the term void.
        public void DeleteClient()
        {
            //code here

        }
        //Method to delete all the clients in the database. This is static because it will be affecting the class and not specific clients.
        public static void DeleteAll()
        {
            //code here
        }
    }
}
