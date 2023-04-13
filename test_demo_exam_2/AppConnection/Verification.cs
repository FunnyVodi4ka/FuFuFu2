using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_demo_exam_2.AppConnection
{
    class Verification
    {
        public bool CheckAccess()
        {
            if(SelectedUser.user != null)
            {
                return true;
            }
            else { return false; }
        }
    }
}
