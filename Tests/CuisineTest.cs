using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurant
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=bestrestaurant_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_CuisinesEmptyAtFirst()
    {
      int result = Cuisine.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Cuisine firstCuisine = new Cuisine("Chinese Food");
      Cuisine secondCuisine = new Cuisine("Chinese Food");

      Assert.Equal(firstCuisine, secondCuisine);
    }

    [Fact]
    public void Test_Save_SavesCuisineToDatabase()
    {
      Cuisine testCuisine = new Cuisine("Chinese Food");
      testCuisine.Save();

      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToCuisineObject()
    {
      Cuisine testCuisine = new Cuisine("Chinese Food");
      testCuisine.Save();

      Cuisine savedCuisine = Cuisine.GetAll()[0];

      int result = savedCuisine.GetId();
      int testId = testCuisine.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsCuisineInDatabase()
    {
      Cuisine testCuisine = new Cuisine("Chinese Food");
      testCuisine.Save();

      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

      Assert.Equal(testCuisine, foundCuisine);
    }

    [Fact]
    public void Test_GetRestaurants_RetrievesAllRestaurantsWithCuisine()
    {
      Cuisine testCuisine = new Cuisine("Chinese Food");
      testCuisine.Save();

      Restaurant firstRestaurant = new Restaurant("PF Changs", "A chinese food chain.", testCuisine.GetId());
      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("Panda Express", "Chinese fast-food.", testCuisine.GetId());
      secondRestaurant.Save();

      List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
      List<Restaurant> resultRestaurantList = testCuisine.GetRestaurants();

      Assert.Equal(testRestaurantList, resultRestaurantList);
    }

    [Fact]
    public void Test_Update_CuisineName()
    {
      Cuisine testCuisine = new Cuisine("Chinese Food");
      testCuisine.Save();
      testCuisine.Update("Italian Food");

      Cuisine newCuisine = new Cuisine("Italian Food");

      Assert.Equal(testCuisine.GetName(), newCuisine.GetName());
    }

    [Fact]
    public void Test_Delete_CuisineName()
    {
      Cuisine testCuisine1 = new Cuisine("Chinese Food");
      testCuisine1.Save();
      Cuisine testCuisine2 = new Cuisine("Indian Food");
      testCuisine2.Save();
      testCuisine2.Delete();

      List<Cuisine> testCousineList = new List<Cuisine> {testCuisine1};
      List<Cuisine> testCousineList2 = Cuisine.GetAll();

      Assert.Equal(testCousineList, testCousineList2);
    }
    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }
  }
}
