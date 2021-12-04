using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonDonLibrary.Fumen
{
    public enum NoteType : int
    {
        Don = 0x01,
        Do  = 0x02,
        Ko  = 0x03,
        Kat = 0x04,
        Ka  = 0x05,
        Drumroll      = 0x06,
        LargeDon      = 0x07,
        LargeKat      = 0x08,
        LargeDrumroll = 0x09,
        Balloon       = 0x0A,
        Bell          = 0x0C
    }
}
