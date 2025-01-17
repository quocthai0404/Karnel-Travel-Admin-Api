﻿

using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Services;


namespace Karnel_Travel_Admin_Api.Helpers;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {

        services.AddScoped<IAccountService, AccountServiceImpl>();
        services.AddScoped<IAirportService, AirportServiceImpl>();
        services.AddScoped<IPhotoService, PhotoServiceImpl>();
        services.AddScoped<IBeachService, BeachServiceImpl>();
        services.AddScoped<IDiscountService, DiscountServiceImpl>();
        services.AddScoped<IFacilityService, FacilityServiceImpl>();
        services.AddScoped<IFlightService, FlightServiceImpl>();
        services.AddScoped<IHotelService, HotelServiceImpl>();
        services.AddScoped<ILocationService, LocationServiceImpl>();
        services.AddScoped<IRestaurantService, RestaurantServiceImpl>();
        services.AddScoped<IReviewService, ReviewServiceImpl>();
        services.AddScoped<IRoomService, RoomServiceImpl>();
        services.AddScoped<ITourService, TourServiceImpl>();
    }
}
