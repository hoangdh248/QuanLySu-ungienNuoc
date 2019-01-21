using LogicDataModels.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicDataModels
{
    public class QuyenLDM : BaseLDM<Role>
    {
        public override List<Role> GetElements()
        {
            List<Role> result = new List<Role>();
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            result = db.Roles.Where(x => x.isDelete == false).ToList();
            return result; 
        }
        public override Role GetElement(int id)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Roles.Where(x => x.isDelete == false && x.ID == id).FirstOrDefault();
            return result;
        }
        public override Role Insert(Role obj)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            obj.isDelete = false;
            var result = db.Roles.Add(obj);
            if (result != null)
            {
                if(db.SaveChanges() > 0 )
                {
                    return result;
                }
            }
            return null;
        }
        public override bool Delete(int id)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Roles.Where(x => x.ID == id).FirstOrDefault();
            if(result != default(Role))
            {
                result.isDelete = true;
                if (db.SaveChanges() > 0)
                    return true;
            }
            return false;
        }
        public override bool Update(Role obj)
        {
                QLSDDienNuocModel db = new QLSDDienNuocModel();
                var result = db.Roles.Where(x => x.ID == obj.ID).FirstOrDefault();
                if (result != default(Role))
                {
                    result.RoleName = obj.RoleName;
                    result.Description = obj.Description;
                    result.ModifiedByID = obj.ModifiedByID;
                    result.ModifiedDate = DateTime.Now.Date;
                    if (db.SaveChanges() >= 0)
                    {
                        return true;
                    }
                }
            return false;
        }
        public List<Role> SearchElements(string key)
        {
            List<Role> result = new List<Role>();
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            result = db.Roles.Where(x => x.isDelete == false && x.RoleName.Contains(key)).ToList();
            return result;
        }

        public bool isDuplicateRoleName(string roleName,int id)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            if(id != -1)
            {
                var CurrentRole = this.GetElement(id);
                if (db.Roles.Where(x => x.RoleName == roleName && x.isDelete == false && x.ID != CurrentRole.ID).FirstOrDefault() == default(Role))
                    return false;
            }
            else
            {
                if (db.Roles.Where(x => x.RoleName == roleName && x.isDelete == false).FirstOrDefault() == default(Role))
                    return false;
            }
            return true;
        }
    }
}
