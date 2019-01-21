using LogicDataModels.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicDataModels
{
    public class KhachHangLDM : BaseLDM<Customer>
    {
        public override List<Customer> GetElements()
        {
            List<Customer> result = new List<Customer>();
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            result = db.Customers.Where(e => e.isDelete == false).ToList();
            return result;
        }
        public List<Customer> SearchElements(string key)
        {
            List<Customer> result = new List<Customer>();
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            result = db.Customers.Where(e => e.isDelete == false && e.User.DisplayName.Contains(key)).ToList();
            return result;
        }
        public override Customer GetElement(int id)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            return db.Customers.Where(x => x.isDelete == false && x.ID == id).FirstOrDefault();
        }
        public Customer GetElementByUserID(int id)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            return db.Customers.Where(x => x.isDelete == false && x.UserID == id).FirstOrDefault();
        }
        public override bool Delete(int id)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Customers.Where(x => x.ID == id).FirstOrDefault();
            if (result != default(Customer))
            {
                result.isDelete = true;
            }
            if (db.SaveChanges() <= 0)
            {
                return false;
            }
            return true;
        }
        public override Customer Insert(Customer obj)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            obj.isDelete = false;
            var result = db.Customers.Add(obj);
            if (result != null)
            {
                if (db.SaveChanges() > 0)
                {
                    return result;
                }
            }
            return null;
        }
        public override bool Update(Customer obj)
        {

            if (new UserLDM().Update(obj.User))
            {
                QLSDDienNuocModel db = new QLSDDienNuocModel();
                var result = db.Customers.Where(x => x.ID == obj.ID).FirstOrDefault();
                if (result != default(Customer))
                {
                    result.PriceID = obj.PriceID;
                    result.ModifiedByID = obj.ModifiedByID;
                    result.ModifiedDate = DateTime.Now.Date;
                    result.PassportID = obj.PassportID;
                    if (db.SaveChanges() >= 0)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}
