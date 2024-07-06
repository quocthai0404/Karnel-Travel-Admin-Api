using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Services;

public class BeachServiceImpl : IBeachService
{
    private DatabaseContext db;
    public BeachServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }

    public bool create(Beach beach)
    {
        try
        {
            db.Beaches.Add(beach);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool delete(int id)
    {
        try
        {
            var beach = db.Beaches.Find(id);
            if (beach == null)
            {
                return false;
            }
            beach.IsHide = true;
            return update(beach);
        }
        catch
        {
            return false;
        }
    }

    public List<BeachDTO> findAllDeleted()
    {
        return db.Beaches.Where(b => b.IsHide == true).Select(b => new BeachDTO { BeachId = b.BeachId, BeachName = b.BeachName, BeachLocation = b.BeachLocation, LocationId = b.LocationId }).ToList();
    }

    public List<BeachDTO> findAllDTO()
    {
        return db.Beaches.Where(b => b.IsHide == false).Select(b => new BeachDTO { BeachId = b.BeachId, BeachName = b.BeachName, BeachLocation = b.BeachLocation, LocationId = b.LocationId }).ToList();
    }

    public Beach findById(int id)
    {
        return db.Beaches.SingleOrDefault(b => b.BeachId == id && b.IsHide == false);
    }

    public BeachDTO findByIdDTO(int id)
    {
        return findAllDTO().Select(b => new BeachDTO { BeachId = b.BeachId, BeachName = b.BeachName, BeachLocation = b.BeachLocation, LocationId = b.LocationId }).SingleOrDefault(b => b.BeachId == id);
    }



    public bool Recover(int id)
    {

        var beach = db.Beaches.SingleOrDefault(a => a.BeachId == id && a.IsHide == true);
        if (beach == null)
        {
            return false;
        }
        beach.IsHide = false;
        return update(beach);

    }

    public bool update(Beach beach)
    {
        try
        {
            db.Entry(beach).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }


}
