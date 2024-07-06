using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Karnel_Travel_Admin_Api.Services;

public class LocationServiceImpl : ILocationService
{
    private DatabaseContext db;
    public LocationServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }

    public bool AddLocation(Location location)
    {
        try
        {
            db.Locations.Add(location);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool AddStreet(Street street)
    {
        try {
            db.Streets.Add(street);
            return db.SaveChanges() > 0;
        }
        catch {
            return false;
        }
    }

    public bool DeleteLocation(int id)
    {
        try
        {
            var location = findById(id);
            if (location == null)
            {
                return false;
            }
            location.IsHide = true;
            return update(location);
        }
        catch
        {
            return false;
        }
    }

    public List<DistrictDTO> findAllDistrictDTO(int provinceId)
    {
        return db.Districts.Where(d => d.ProvinceId == provinceId).Select(d => new DistrictDTO
        {
            DistrictId = d.DistrictId,
            DistrictName = d.DistrictName,
            ProvinceId = d.ProvinceId
        }).ToList();
    }

    public List<ProvinceDTO> findAllProvinceDTO()
    {
        return db.Provinces.Select(p => new ProvinceDTO { ProvinceId = p.ProvinceId, ProvinceName = p.ProvinceName }).ToList();
    }

    public List<StreetDTO> findAllStreetDTO(int wardId)
    {
        return db.Streets.Where(s => s.WardId == wardId).Select(s => new StreetDTO
        {
            StreetId = s.StreetId,
            StreetName = s.StreetName,
            WardId = s.WardId,
        }).ToList();
    }

    public List<WardDTO> findAllWardDTO(int districtId)
    {
        return db.Wards.Where(w => w.DistrictId == districtId).Select(w => new WardDTO
        {
            WardId = w.WardId,
            WardName = w.WardName,
            DistrictId = w.DistrictId
        }).ToList();
    }

    public Location findById(int id)
    {
        return db.Locations.SingleOrDefault(a => a.LocationId == id && a.IsHide == false);
    }

    public LocationDTO findByIdDTO(int id)
    {
        return db.Locations.Select(l => new LocationDTO
        {
            LocationId = l.LocationId,
            LocationNumber = l.LocationNumber,
            ProvinceId = l.ProvinceId,
            DistrictId = l.DistrictId,
            WardId = l.WardId,
            StreetId = l.StreetId,
            IsHide = l.IsHide
        }).SingleOrDefault(l => l.LocationId == id && l.IsHide == false);
    }

    public StringLocationDTO findByIdLocation(int id)
    {
        var query = @"
        SELECT l.location_id AS LocationId, l.location_number AS LocationNumber, 
               p.province_name AS Province, d.district_name AS District, 
               w.ward_name AS Ward, s.street_name AS Street
        FROM Location l
        JOIN Province p ON l.province_id = p.province_id
        JOIN District d ON l.district_id = d.district_id
        JOIN Ward w ON l.ward_id = w.ward_id
        JOIN Street s ON l.street_id = s.street_id
        WHERE l.location_id = @id";

        using (var connection = db.Database.GetDbConnection())
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("@id", id));

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new StringLocationDTO
                        {
                            LocationId = (int)reader["LocationId"],
                            LocationNumber = (string)reader["LocationNumber"],
                            Province = (string)reader["Province"],
                            District = (string)reader["District"],
                            Ward = (string)reader["Ward"],
                            Street = (string)reader["Street"]
                        };
                    }
                }
            }
        }

        return new StringLocationDTO();


    }

    public Street findStreetById(int id)
    {
        return db.Streets.Find(id);
    }

    public bool RecoverLocation(int id)
    {
        var location = db.Locations.SingleOrDefault(a => a.LocationId == id && a.IsHide == true);
        if (location == null)
        {
            return false;
        }
        location.IsHide = false;
        return update(location);
    }

    public bool update<T>(T entity)
    {
        try
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
