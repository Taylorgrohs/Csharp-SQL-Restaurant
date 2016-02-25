using Nancy;
using BestRestaurant;
using System.Collections.Generic;
using System;

namespace BestRestaurant
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };

      Get["/restaurants"] = _ =>
      {
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", allRestaurants];
      };

      Get["/cuisines"] = _ =>
      {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["cuisines.cshtml", allCuisines];
      };

      Get["/cuisines/new"] = _ =>
      {
        return View["cuisines_form.cshtml"];
      };

      Post["/cuisines/new"] = _ =>
      {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
        newCuisine.Save();
        return View["success.cshtml"];
      };

      Get["/review/new"] = _ =>
      {
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View["review_form.cshtml", allRestaurants];
      };

      Post["/review/new"] = _ =>
      {
        Review newReview = new Review(Request.Form["review-description"], Request.Form["restaurant-id"]);
        newReview.Save();
        return View["success.cshtml"];
      };

      Get["/restaurants/new"] = _ =>
      {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["restaurants_form.cshtml", allCuisines];
      };

      Post["/restaurants/new"] = _ =>
      {
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["restaurant-description"], Request.Form["cuisine-id"]);
        newRestaurant.Save();
        return View["success.cshtml"];
      };

      Post["/restaurants/delete"] = _ =>
      {
        Restaurant.DeleteAll();
        return View["cleared.cshtml"];
      };

      Get["/restaurant/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedRestaurant = Restaurant.Find(parameters.id);
        var RestaurantReviews = SelectedRestaurant.GetReviews();
        model.Add("restaurant", SelectedRestaurant);
        model.Add("review", RestaurantReviews);
        return View["restaurant.cshtml", model];
      };

      Get["/cuisines/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedCuisine = Cuisine.Find(parameters.id);
        var CuisineRestaurants = SelectedCuisine.GetRestaurants();
        model.Add("cuisine", SelectedCuisine);
        model.Add("restaurants", CuisineRestaurants);
        return View["cuisine.cshtml", model];
      };

      Post["/cuisines/delete"] = _ =>
      {
        Cuisine.DeleteAll();
        return View["cleraed.cshtml"];
      };

      Get["cuisine/edit/{id}"] = parameters =>
      {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_edit.cshtml", SelectedCuisine];
      };

      Patch["cuisine/edit/{id}"] = parameters =>
      {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        SelectedCuisine.Update(Request.Form["cuisine-name"]);
        return View["success.cshtml"];
      };

      Get["cuisine/delete/{id}"] = parameters =>
      {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_delete.cshtml", SelectedCuisine];
      };

      Delete["cuisine/delete/{id}"] = parameters =>
      {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        SelectedCuisine.Delete();
        return View["success.cshtml"];
      };

      Get["restaurant/edit/{id}"] = parameters =>
      {
        Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
        return View["restaurant_edit.cshtml", SelectedRestaurant];
      };

      Patch["restaurant/edit/{id}"] = parameters =>
      {
        Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
        SelectedRestaurant.Update(Request.Form["restaurant-name"], Request.Form["restaurant-description"]);
        return View["success.cshtml"];
      };

      Get["restaurant/delete/{id}"] = parameters =>
      {
        Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
        return View["restaurant_delete.cshtml", SelectedRestaurant];
      };

      Delete["restaurant/delete/{id}"] = parameters =>
      {
        Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
        SelectedRestaurant.Delete();
        return View["success.cshtml"];
      };

      Get["review/edit/{id}"] = parameters =>
      {
        Review selectedReview = Review.Find(parameters.id);
        return View["review_edit.cshtml", selectedReview];
      };

      Patch["review/edit/{id}"] = parameters =>
      {
        Review selectedReview = Review.Find(parameters.id);
        selectedReview.Update(Request.Form["review-description"]);
        return View["success.cshtml"];
      };

      Get["review/delete/{id}"] = parameters =>
      {
        Review selectedReview = Review.Find(parameters.id);
        return View["review_delete.cshtml", selectedReview];
      };

      Delete["review/delete/{id}"] = parameters =>
      {
        Review selectedReview = Review.Find(parameters.id);
        selectedReview.Delete();
        return View["success.cshtml"];
      };
    }
  }
}
