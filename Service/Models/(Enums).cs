using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public enum GameState
    {
        Pregame=1,
        RoundStart=2,
        PlayTime=3,
        RoundEnd=4,
        PostGame=5
    }
}