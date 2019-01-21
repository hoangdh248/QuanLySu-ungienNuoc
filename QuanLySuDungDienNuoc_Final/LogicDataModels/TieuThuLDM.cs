using LogicDataModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicDataModels
{
    public class TieuThuLDM : BaseLDM<Consume>
    {
        public override List<Consume> GetElements(int id)
        {
            List<Consume> result = new List<Consume>();
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            result = db.Consumes.Where(e => e.isDelete == false && e.CustomerID == id).ToList();
            return result;
        }
        public override Consume GetElement(int id)
        {
            return new QLSDDienNuocModel().Consumes.Where(x => x.ID == id).First();
        }
        public List<Consume> SearchElements(DateTime fromDate, DateTime toDate, int cusID)
        {
            List<Consume> result = new List<Consume>();
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            result = db.Consumes.Where(e => e.isDelete == false && e.Time.Value >= fromDate && e.Time.Value <= toDate && e.CustomerID == cusID).ToList();
            return result;
        }
        public override bool Delete(int id)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Consumes.Where(x => x.ID == id).FirstOrDefault();
            if (result != default(Consume))
            {
                result.isDelete = true;
            }
            if (db.SaveChanges() <= 0)
            {
                return false;
            }
            return true;
        }
        public override Consume Insert(Consume obj)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            obj.isDelete = false;
            obj.isPay = false;
            var result = db.Consumes.Add(obj);
            if (result != null)
            {
                if (db.SaveChanges() > 0)
                {
                    return result;
                }
            }
            return null;
        }
        public override bool Update(Consume obj)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Consumes.Where(x => x.ID == obj.ID).FirstOrDefault();
            if (result != default(Consume))
            {
                result.WaterConsume = obj.WaterConsume;
                result.ElectricConsume = obj.ElectricConsume;
                result.ModifiedByID = obj.ModifiedByID;
                result.ModifiedDate = DateTime.Now.Date;
                result.Time = obj.Time.Value.Date;
                result.NewWaterIndex = obj.NewWaterIndex;
                result.NewElectricIndex = obj.NewElectricIndex;
                if (db.SaveChanges() >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        public bool ThanhToan(Consume obj)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Consumes.Where(x => x.ID == obj.ID).FirstOrDefault();
            if (result != default(Consume))
            {
                result.isPay = true;
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
