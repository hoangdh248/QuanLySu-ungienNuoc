using LogicDataModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicDataModels
{
    public class User_RoleLDM : BaseLDM<Users_Roles>
    {
        public override List<Users_Roles> GetElements(int UserID)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Users_Roles.Where(x => x.UserID == UserID && x.isDelete == false).ToList();
            return result;
        }
        public override Users_Roles Insert(Users_Roles obj)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            obj.isDelete = false;
            var result = db.Users_Roles.Add(obj);
            if (result != null)
            {
                if (db.SaveChanges() > 0)
                {
                    return result;
                }
            }
            return null;
        }
        public bool Delete(int UserID, int RoleID)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Users_Roles.Where(x => x.UserID == UserID && x.RoleID == RoleID).FirstOrDefault();
            if (result != default(Users_Roles))
            {
                result.isDelete = true;
            }
            if (db.SaveChanges() <= 0)
            {
                return false;
            }
            return true;
        }
        public override bool Update(Users_Roles obj)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Users_Roles.Where(x => x.UserID == obj.UserID && x.RoleID == obj.RoleID).FirstOrDefault();
            if (result != default(Users_Roles))
            {
                result.isAdd = obj.isAdd;
                result.isEdit = obj.isEdit;
                result.isRemove = obj.isRemove;
                result.isView = obj.isView;
                result.ModifiedByID = obj.ModifiedByID;
                result.ModifiedDate = DateTime.Now.Date;
                if (db.SaveChanges() >= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
