using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurant
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=bestrestaurant_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameDescription()
    {
      Restaurant firstRestaurant = new Restaurant("GreatWall", "Buffet", 1);
      Restaurant secondRestaurant = new Restaurant("GreatWall", "Buffet", 1);

      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_Save()
    {
      Restaurant testRestaurant = new Restaurant("GreatWall", "Buffet", 1);
      testRestaurant.Save();

      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdtoObject()
    {
      Restaurant testRestaurant = new Restaurant("GreatWall", "Buffet", 1);
      testRestaurant.Save();

      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testId = testRestaurant.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsRestaurantDatabase()
    {
      Restaurant testRestaurant = new Restaurant("GreatWall", "Buffet", 1);
      testRestaurant.Save();

      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      Assert.Equal(testRestaurant, foundRestaurant);
    }

    [Fact]
    public void Test_Update_Restaurant()
    {
      Restaurant testRestaurant = new Restaurant("PF Changs", "A chain", 0);
      testRestaurant.Save();
      testRestaurant.Update("Panda Express", "A bad chain");

      Restaurant newRestaurant = new Restaurant("Panda Express", "A bad chain", 0);
      Assert.Equal(testRestaurant.GetName(), newRestaurant.GetName());
    }

    [Fact]
    public void Test_Delete_RestaurantName()
    {
      Restaurant testRestaurant1 = new Restaurant("Chinese Food", "buffet", 1);
      testRestaurant1.Save();
      Restaurant testRestaurant2 = new Restaurant("Indian Food", "buffet", 1);
      testRestaurant2.Save();
      testRestaurant2.Delete();

      List<Restaurant> testCousineList = new List<Restaurant> {testRestaurant1};
      List<Restaurant> testCousineList2 = Restaurant.GetAll();

      Assert.Equal(testCousineList, testCousineList2);
    }
    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
