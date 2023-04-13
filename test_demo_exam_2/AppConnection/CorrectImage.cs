using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_demo_exam_2.AppConnection
{
    public partial class Users
    {
        public string CorrectImage
        {
            get
            {
                if(File.Exists(System.AppDomain.CurrentDomain.BaseDirectory+"..\\..\\Resources\\UserImages\\"+pImage))
                {
                    return System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\UserImages\\" + pImage;
                }
                else 
                {
                    return "/Resources/SystemImages/DefaultPicture.png";
                }
            }
        }
    }
}
