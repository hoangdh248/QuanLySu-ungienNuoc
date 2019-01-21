using Commons;
using LogicDataModels.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicDataModels
{
    public class UserLDM : BaseLDM<User>
    {
        public override List<User> GetElements()
        {
            List<User> result = new List<User>();
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            result = db.Users.Where(e => e.isDelete == false && e.isAdmin == 2).ToList();
            return result;
        }
        public override User GetElement(int id)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            return db.Users.Where(x => x.ID == id && x.isDelete == false).FirstOrDefault();
        }
        public User Login(string UserName, string Password)
        {
            string pass = En_Decrypt.Encrypt(Password);
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            User result = db.Users.Where(e => e.UserName == UserName && e.Password == pass && e.isDelete == false).FirstOrDefault();
            return result;
        }
        public List<User> SearchElements(string key)
        {
            List<User> result = new List<User>();
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            result = db.Users.Where(x => x.isDelete == false && x.DisplayName.Contains(key) && x.isAdmin != 1).ToList();
            return result;
        }
        public override User Insert(User obj)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            obj.isDelete = false;
            var result = db.Users.Add(obj);
            if (result != null)
            {
                if (db.SaveChanges() > 0)
                {
                    foreach (var item in new QuyenLDM().GetElements())
                    {
                        var mUR = new Users_Roles
                        {
                            CreatedByID = obj.CreatedByID,
                            CreatedDate = DateTime.Now.Date,
                            isAdd = false,
                            isDelete = false,
                            isEdit = false,
                            isRemove = false,
                            isView = false,
                            RoleID = item.ID,
                            UserID = result.ID
                        };
                        new User_RoleLDM().Insert(mUR);
                    }
                    return result;
                }
            }
            return null;
        }
        public override bool Delete(int id)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Users.Where(x => x.ID == id).FirstOrDefault();
            if (result != default(User))
            {
                result.isDelete = true;
            }
            if (db.SaveChanges() <= 0)
            {
                return false;
            }
            return true;
        }
        public override bool Update(User obj)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Users.Where(x => x.ID == obj.ID).FirstOrDefault();
            if (result != default(User))
            {
                result.Address = obj.Address;
                result.DateOfBirth = obj.DateOfBirth;
                result.DisplayName = obj.DisplayName;
                result.Email = obj.Email;
                result.Gender = obj.Gender;
                result.isAdmin = obj.isAdmin;
                result.ModifiedByID = obj.ModifiedByID;
                result.ModifiedDate = DateTime.Now.Date;
                result.Phone = obj.Phone;
                result.isPay = obj.isPay;
                if (db.SaveChanges() >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        public bool DoiMatKhau(User obj)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            var result = db.Users.Where(x => x.ID == obj.ID).FirstOrDefault();
            if (result != default(User))
            {
                result.Password = obj.Password;
                if (db.SaveChanges() >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        public bool isDuplicateUserName(string userName)
        {
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            if (db.Users.Where(x => x.isDelete == false && x.UserName == userName).FirstOrDefault() == default(User))
                return false;
            return true;
        }
    }
}
