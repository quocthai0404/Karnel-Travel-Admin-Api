using CloudinaryDotNet;
using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Services;

public class RoomServiceImpl : IRoomService
{
    private DatabaseContext db;
    public RoomServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }
    public bool create(Room room)
    {
        try
        {
            db.Rooms.Add(room);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool delete(int roomId)
    {
        try
        {
            var room = db.Rooms.Find(roomId);
            if (room == null)
            {
                return false;
            }
            room.IsHide = true;
            return update(room);
        }
        catch
        {
            return false;
        }
    }

    public List<RoomDTO> findAll()
    {
        return db.Rooms.Where(b => b.IsHide == false).Select(b => new RoomDTO 
        {   
            RoomId = b.RoomId,
            HotelId = b.HotelId,
            RoomName = b.RoomName,
            RoomDescription = b.RoomDescription,
            RoomPrice = b.RoomPrice,
            NumOfSingleBed = b.NumOfSingleBed,
            NumOfDoubleBed = b.NumOfDoubleBed, 
            IsHide = b.IsHide
        })
            .ToList();
    }

    public List<RoomDTO> findAllDeleted()
    {
        return db.Rooms.Where(b => b.IsHide == true).Select(b => new RoomDTO
        {
            RoomId = b.RoomId,
            HotelId = b.HotelId,
            RoomName = b.RoomName,
            RoomDescription = b.RoomDescription,
            RoomPrice = b.RoomPrice,
            NumOfSingleBed = b.NumOfSingleBed,
            NumOfDoubleBed = b.NumOfDoubleBed,
            IsHide = b.IsHide
        })
            .ToList();
    }

    public Room findById(int roomId)
    {
        return db.Rooms.SingleOrDefault(b => b.RoomId == roomId && b.IsHide == false);
    }

    public RoomDTO findByIdDTO(int roomId)
    {
        return findAll().SingleOrDefault(b => b.RoomId == roomId);
    }

    public bool Recover(int roomId)
    {
        var room = db.Rooms.SingleOrDefault(a => a.RoomId == roomId && a.IsHide == true);
        if (room == null)
        {
            return false;
        }
        room.IsHide = false;
        return update(room);
    }

    public bool update(Room room)
    {
        try
        {
            db.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
