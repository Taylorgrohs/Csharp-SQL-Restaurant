using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BestRestaurant
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private string _description;
    private int _cuisineId;

    public Restaurant(string Name, string Description, int CuisineId, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _description = Description;
      _cuisineId = CuisineId;
    }
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
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurant;", conn);
      cmd.ExecuteNonQuery();
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = this.GetId() == newRestaurant.GetId();
        bool nameEquality = this.GetName() == newRestaurant.GetName();
        bool descriptionEquality = this.GetDescription() == newRestaurant.GetDescription();
        bool cuisineEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
        return (idEquality && nameEquality && descriptionEquality && cuisineEquality);
      }
    }
    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public void SetCuisineId(int newCuisineId)
    {
      _cuisineId = newCuisineId;
    }
    public static List<Restaurant> GetAll()
    {
      List<Restaurant> AllRestaurants = new List<Restaurant>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantDescription = rdr.GetString(2);
        int restaurantCuisineId = rdr.GetInt32(3);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantDescription, restaurantCuisineId, restaurantId);
        AllRestaurants.Add(newRestaurant);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllRestaurants;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurant (name, description, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantDescription, @RestaurantCuisineId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@RestaurantName";
      nameParameter.Value = this.GetName();

      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@RestaurantDescription";
      descriptionParameter.Value = this.GetDescription();

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@restaurantCuisineId";
      cuisineIdParameter.Value = this.GetCuisineId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(descriptionParameter);
      cmd.Parameters.Add(cuisineIdParameter);

      rdr = cmd.ExecuteReader();

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

    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant WHERE id = @RestaurantId;", conn);
      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = id.ToString();
      cmd.Parameters.Add(restaurantIdParameter);
      rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantName = null;
      string foundRestaurantDescription = null;
      int foundRestaurantCuisineId = 0;

      while(rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantName = rdr.GetString(1);
        foundRestaurantDescription = rdr.GetString(2);
        foundRestaurantCuisineId = rdr.GetInt32(3);
      }
      Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundRestaurantDescription, foundRestaurantCuisineId, foundRestaurantId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundRestaurant;
    }

    public void Update(string newName, string newDescription)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE restaurant SET name = @NewName OUTPUT INSERTED.name WHERE id = @RestaurantId; UPDATE restaurant SET description = @NewDescription OUTPUT INSERTED.description WHERE id = @RestaurantId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter newDescriptionParameter = new SqlParameter();
      newDescriptionParameter.ParameterName = "@NewDescription";
      newDescriptionParameter.Value = newDescription;
      cmd.Parameters.Add(newDescriptionParameter);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = this.GetId();
      cmd.Parameters.Add(restaurantIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
        this._description = rdr.GetString(0);
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

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurant WHERE id = @RestaurantId;", conn);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = this.GetId();

      cmd.Parameters.Add(restaurantIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
    public override int GetHashCode()
    {
      return 0;
    }
  }
}
