using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMtoKOPS
{
    public interface ITournament
    {
        void ReadResults();
        String PrintResults();
        String PrintProtocols();
        String PrintAllHistories();
        String PrintListHistories(List<int> nums);
    }
}
