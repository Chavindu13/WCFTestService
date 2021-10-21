using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFTestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string Insert(InsertUser user)
        {
            string msg;
            SqlConnection con = new SqlConnection("data source=DESKTOP-M5PVNLO\\SQLEXPRESS;initial catalog=wavenet;integrated security=True;");
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO UserTab(Name, Email) VALUES (@Name, @Email)", con);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            int g = cmd.ExecuteNonQuery();
            if(g == 1)
            {
                msg = "Successfully Inserted!";
            }
            else
            {
                msg = "Failed to Insert!";
            }
            return msg;
        }
    }
}
