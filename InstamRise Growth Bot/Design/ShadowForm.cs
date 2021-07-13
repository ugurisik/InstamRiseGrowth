using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstamRise_Growth_Bot.Design
{
    public class ShadowForm
    {
        const int CS_DropSHADOW = 0x20000;
        const int GCL_STYLE = (-26);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);


        public void ApplyShadows(Form form)
        {
            SetClassLong(form.Handle, GCL_STYLE, GetClassLong(form.Handle, GCL_STYLE) | CS_DropSHADOW);
        }

    
    }
}
