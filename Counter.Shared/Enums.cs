using System;
using System.Collections.Generic;
using System.Text;

namespace Counter.Shared.Enums
{
    public enum RaporDurumu : byte
    {
        Hazirlaniyor = 0,
        Tamamlandi = 1,
        Hata = 2
    }

    public enum OlcumTipi : byte
    {
        Voltaj = 0,
        Akim = 1,
        Endeks= 2
    }
}