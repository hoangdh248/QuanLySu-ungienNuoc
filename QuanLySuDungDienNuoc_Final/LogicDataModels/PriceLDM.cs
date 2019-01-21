using LogicDataModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicDataModels
{
    public class PriceLDM : BaseLDM<Price>
    {
        public override List<Price> GetElements()
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            return db.Prices.Where(x => x.isDelete == false).ToList();
        }
        public override Price GetElement(int id)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            return db.Prices.Where(x => x.isDelete == false && x.ID == id).FirstOrDefault();
        }
        public override Price Insert(Price obj)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            obj.isDelete = false;
            var result = db.Prices.Add(obj);
            if (result != null)
            {
                if (db.SaveChanges() > 0)
                {
                    return result;
                }
            }
            return null;
        }
        public override bool Update(Price obj)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Prices.Where(x => x.ID == obj.ID).First();
            if (result != null)
            {
                result.ModifiedByID = obj.ModifiedByID;
                result.ModifiedDate = DateTime.Now.Date;
                result.PriceName = obj.PriceName;
                result.WaterPrice = obj.WaterPrice;
                result.ElectricPrice = obj.ElectricPrice;
                if (db.SaveChanges() >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Price> SearchElements(string key)
        {
            List<Price> result = new List<Price>();
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            result = db.Prices.Where(e => e.isDelete == false && e.PriceName.Contains(key)).ToList();
            return result;
        }
        public override bool Delete(int id)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Prices.Where(x => x.ID == id).FirstOrDefault();
            if (result != default(Price))
            {
                result.isDelete = true;
            }
            if (db.SaveChanges() <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
