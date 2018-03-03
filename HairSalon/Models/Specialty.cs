using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _id;
    private string _description;

    public Specialty(string description, int id = 0)
    {
      _id = id;
      _description = description;
    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = (this.GetId()) == newSpecialty.GetId();

        return (idEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }
    public int GetId()
    {
      return _id;
    }
    public string GetDescription()
    {
      return _description;
    }
    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialtys = new List<Specialty>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties ORDER BY name;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        string specialtyName = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
        allSpecialtys.Add(newSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialtys;
    }
    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients_stylists(stylist_id, specialty_id) VALUES (@StylistId, SpecialtyId);";

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@StylistId";
      stylist_id.Value = newStylist.GetId();
      cmd.Parameters.Add(stylist_id);

      MySqlParameter specialty_id = new MySqlParameter();
      specialty_id.ParameterName = "@SpecialtyId";
      specialty_id.Value = _id;
      cmd.Parameters.Add(specialty_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Stylist> GetStylists()
{
    MySqlConnection conn = DB.Connection();
    conn.Open();
    MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT stylists.* FROM specialties
        JOIN clients_stylists ON (specialties.id = specialties_stylists.specialty_id)
        JOIN stylists ON (specialties_stylists.stylist_id = stylists.id)
        WHERE specialties.id = @SpecialtyId;";

    MySqlParameter specialtyIdParameter = new MySqlParameter();
    specialtyIdParameter.ParameterName = "@SpecialtyId";
    specialtyIdParameter.Value = _id;
    cmd.Parameters.Add(specialtyIdParameter);
    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

    List<Stylist> stylists = new List<Stylist>{};
    while(rdr.Read())
    {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);

        Stylist newStylist = new Stylist(stylistName, stylistId);
        stylists.Add(newStylist);
    }

    conn.Close();
    if (conn != null)
    {
        conn.Dispose();
    }
    return stylists;
}
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialties (description) VALUES (@description);";

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@description";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Specialty Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int specialtyId = 0;
      string specialtyName = "";

      while(rdr.Read())
      {
        specialtyId = rdr.GetInt32(0);
        specialtyName = rdr.GetString(1);
      }

      Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return newSpecialty;
    }
    public void UpdateName(string newDescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE specialties SET description = @newDescription WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@newDescription";
      description.Value = newDescription;
      cmd.Parameters.Add(description);

      cmd.ExecuteNonQuery();
      _description = newDescription;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void DeleteOne()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties WHERE id = @specialtyId; DELETE FROM clients_stylists WHERE specialty_id = @specialtyId;";

      MySqlParameter specialtyIdParameter = new MySqlParameter();
      specialtyIdParameter.ParameterName = "@specialtyId";
      specialtyIdParameter.Value = this.GetId();
      cmd.Parameters.Add(specialtyIdParameter);

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
